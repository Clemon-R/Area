﻿using Area.Graphs.Spotify;
using Area.Models;
using Area.ViewModels;
using Area.Wrappers.Spotify.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Area.Services.App
{
    public class SpotifyService : IService
    {
        private readonly SpotifyWrapper _spotifyWrapper;
        private readonly AccountService _accountService;
        private readonly ApplicationDbContext _context;

        public SpotifyService(SpotifyWrapper spotifyWrapper, ApplicationDbContext context, AccountService accountService)
        {
            _spotifyWrapper = spotifyWrapper;
            _accountService = accountService;
            _context = context;
        }

        public IViewModel IsSpotifyTokenAvailable(Account owner)
        {
            return _context.Tokens.Where(t => t.Owner.Id == owner.Id && t.Type == Enums.ServiceTypeEnum.Spotify).Any() 
                ? (IViewModel)new ErrorViewModel() 
                : (IViewModel)new SuccessViewModel();
        }

        public IViewModel GenerateSpotifyToken(Account owner, string code)
        {
            Console.WriteLine($"SpotifyService(GetSpotifyToken): The user code is {code}");
            var result = _spotifyWrapper.RefreshSpotifyToken(code);
            if (!result.Success)
            {
                Console.WriteLine("SpotifyService(GetSpotifyToken): Failed to get token");
                return new ErrorViewModel() { Error = (result as SpotifyFailedModel).Error};
            }
            _context.Tokens.RemoveRange(_context.Tokens.Where(t => t.Owner.Id == owner.Id && t.Type == Enums.ServiceTypeEnum.Spotify));
            SpotifyTokenModel tokenModel = result as SpotifyTokenModel;
            var token = new Token()
            {
                AccessToken = tokenModel.Access_Token,
                RefreshToken = tokenModel.Refresh_Token,
                ExpireIn = tokenModel.Expires_In,
                Owner = owner,
                Type = Enums.ServiceTypeEnum.Spotify
            };
            _context.Tokens.Add(token);
            _context.SaveChanges();
            Console.WriteLine("SpotifyService(GetSpotifyToken): Token successfully saved");
            return new SuccessViewModel();
        }

        public IViewModel ConnectToAccount(string code)
        {
            Console.WriteLine($"SpotifyService(ConnectToAccount): The user code is {code}");
            var result = _spotifyWrapper.GenerateSpotifyToken(code, "http://127.0.0.1:8081/spotify/login");
            if (!result.Success)
            {
                Console.WriteLine("SpotifyService(GetSpotifyToken): Failed to get token");
                return new ErrorViewModel() { Error = (result as SpotifyFailedModel).Error };
            }
            result = _spotifyWrapper.GetSpotifyProfile(result as SpotifyTokenModel);
            if (!result.Success)
            {
                Console.WriteLine("SpotifyService(GetSpotifyToken): Failed to get profile");
                return new ErrorViewModel() { Error = (result as SpotifyFailedModel).Error };
            }
            var profile = result as SpotifyProfileModel;
            byte[] encodedPassword = new UTF8Encoding().GetBytes($"{profile.Id}");
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            var account = _context.Accounts.FirstOrDefault(a => a.UserName.Equals(profile.Id) && a.Password.Equals(encoded));
            if (account == null)
            {
                account = new Account()
                {
                    UserName = profile.Id,
                    Password = encoded
                };
                _context.Accounts.Add(account);
                _context.SaveChanges();
                Console.WriteLine($"SpotifyService(GetSpotifyToken): Crated Account({account.Id})");
            }
            Console.WriteLine($"SpotifyService(GetSpotifyToken): Account({account.Id})");
            return _accountService.Login(account);
        }
    }
}
