using Area.Enums;
using Area.Graphs.Spotify;
using Area.Models;
using Area.Services.Actions.Spotify;
using Area.Services.Reactions.Spotify;
using Area.Services.Triggers;
using Area.ViewModels;
using Area.Wrappers.Models;
using Area.Wrappers.Spotify.Models;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Area.Services.App
{
    public class SpotifyService : ApiService, IService
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly SpotifyWrapper _spotifyWrapper;
        private readonly AccountService _accountService;
        private readonly ApplicationDbContext _context;

        public SpotifyService(SpotifyWrapper spotifyWrapper, 
            ApplicationDbContext context, 
            AccountService accountService,
            IServiceProvider serviceProvider) : base(context, ServiceTypeEnum.Spotify)
        {
            _spotifyWrapper = spotifyWrapper;
            _accountService = accountService;
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public override IViewModel GenerateToken(Account owner, string code)
        {
            Console.WriteLine($"SpotifyService(GenerateSpotifyToken): The user code is {code}");
            var result = _spotifyWrapper.GenerateSpotifyToken(code);
            if (!result.Success)
            {
                Console.WriteLine("SpotifyService(GenerateSpotifyToken): Failed to get token");
                return new ErrorViewModel() { Error = (result as RequestFailedModel).Error};
            }
            SpotifyTokenModel tokenModel = result as SpotifyTokenModel;
            var token = new Models.Token()
            {
                AccessToken = tokenModel.Access_Token,
                RefreshToken = tokenModel.Refresh_Token,
                ExpireIn = tokenModel.Expires_In,
                Type = ServiceTypeEnum.Spotify
            };
            _context.Tokens.Add(token);
            owner.Tokens.Add(token);
            _context.Update(owner);
            _context.SaveChanges();
            Console.WriteLine("SpotifyService(GenerateSpotifyToken): Token successfully saved");
            return new SuccessViewModel();
        }

        public IViewModel ConnectToAccount(string code)
        {
            Console.WriteLine($"SpotifyService(ConnectToAccount): The user code is {code}");
            var result = _spotifyWrapper.GenerateSpotifyToken(code, "http://127.0.0.1:8081/spotify/login");
            if (!result.Success)
            {
                Console.WriteLine("SpotifyService(GetSpotifyToken): Failed to get token");
                return new ErrorViewModel() { Error = (result as RequestFailedModel).Error };
            }
            result = _spotifyWrapper.GetSpotifyProfile(result as SpotifyTokenModel);
            if (!result.Success)
            {
                Console.WriteLine("SpotifyService(GetSpotifyToken): Failed to get profile");
                return new ErrorViewModel() { Error = (result as RequestFailedModel).Error };
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

        public SpotifyWebAPI GetSpotifyWebApi(SpotifyTokenModel model)
        {
            SpotifyWebAPI api = new SpotifyWebAPI()
            {
                AccessToken = model.Access_Token,
                TokenType = model.Token_Type,
                UseAuth = true
            };
            return api;
        }

        public IViewModel TestApi(Account owner)
        {
            var tokenModel = GetSpotifyToken(owner);
            if (tokenModel == null)
                return new ErrorViewModel() { Error = "Problem avec token" };
            var token = GetSpotifyWebApi(tokenModel);
            foreach (var trigger in owner.Triggers)
            {
                if (trigger.Template == null)
                {
                    Type action = null;
                    Type reaction;
                    var type = ActionTypeEnum.FollowedArtistNewReleaseSpotify;
                    switch (type)
                    {
                        case ActionTypeEnum.FollowedArtistNewReleaseSpotify:
                            action = typeof(FollowedArtistNewReleaseSpotifyAction);
                            break;
                    }

                    switch (1)
                    {
                        case 1:
                            reaction = typeof(AddToPlaylistSpotifyReaction);
                            break;
                    }
                    trigger.Template = new TriggerTemplate(action, reaction, _serviceProvider);
                }
                trigger.Template.TryActivate(owner, null);
            }
            return new SuccessViewModel();
        }

        public SpotifyTokenModel GetSpotifyToken(Account owner)
        {
            var tokenModel = owner.Tokens.Where(t => t.Type == ServiceTypeEnum.Spotify).FirstOrDefault();
            if (tokenModel == null)
                return null;
            var token = _spotifyWrapper.RefreshSpotifyToken(tokenModel.RefreshToken);
            if (!token.Success)
                return null;
            return token as SpotifyTokenModel;
        }

        public FollowedArtists GetFollowedArtists(SpotifyWebAPI api)
        {
            return api.GetFollowedArtists(SpotifyAPI.Web.Enums.FollowType.Artist, 50);
        }

        public NewAlbumReleases GetNewReleases(SpotifyWebAPI api)
        {
            return api.GetNewAlbumReleases("", 50);
        }

        public Paging<SimplePlaylist> GetUserPlaylists(SpotifyWebAPI api, Account user)
        {
            string id = (_spotifyWrapper.GetSpotifyProfile(GetSpotifyToken(user)) as SpotifyProfileModel).Id;

            return api.GetUserPlaylists(id, 50);
        }

        public Paging<PlaylistTrack> GetPlaylistTracks(SpotifyWebAPI api, string playlistId, Account user)
        {
            string id = (_spotifyWrapper.GetSpotifyProfile(GetSpotifyToken(user)) as SpotifyProfileModel).Id;

            return api.GetPlaylistTracks(id, playlistId);
        }

        public FullPlaylist CreatePlaylist(SpotifyWebAPI api, string name, Account user)
        {
            string id = (_spotifyWrapper.GetSpotifyProfile(GetSpotifyToken(user)) as SpotifyProfileModel).Id;
            return api.CreatePlaylist(id, name);
        }

        public void AddTracksToPlaylist(SpotifyWebAPI api, List<SimpleTrack> tracks, string playlistId, Account user)
        {
            List<string> trackUris = new List<string>();
            for (int i = 0; i < tracks.Count; i++)
            {
                trackUris.Add(tracks[i].Uri);
            }
            string id = (_spotifyWrapper.GetSpotifyProfile(GetSpotifyToken(user)) as SpotifyProfileModel).Id;
            api.AddPlaylistTracks(id, playlistId, trackUris);
        }

        public List<SimpleTrack> GetTracksFromAlbums(SpotifyWebAPI api, List<SimpleAlbum> albums)
        {
            List<SimpleTrack> tracks = new List<SimpleTrack>();

            for (int i = 0; i < albums.Count; i++)
            {
                List<SimpleTrack> albumTracks = api.GetAlbumTracks(albums[i].Id).Items;
                for (int j = 0; j < albumTracks.Count; j++)
                {
                    tracks.Add(albumTracks[j]);
                }
            }
            return tracks;
        }

        public DateTime GetDateFromString(string date, string precision)
        {
            List<string> dateValues = date.Split("-").ToList<string>();
            int year = 0;
            int month = 0;
            int day = 0;

            if (precision == "day" && dateValues.Count >= 3)
                day = Convert.ToInt32(dateValues[2]);
            if ((precision == "day" || precision == "month") && dateValues.Count >= 2)
                month = Convert.ToInt32(dateValues[1]);
            if (dateValues.Count >= 1)
                year = Convert.ToInt32(dateValues[0]);
            return new DateTime(year, month, day);
        }
    }
}
