using Area.Enums;
using Area.Models;
using Area.ViewModels;
using Area.Wrappers.Models;
using Area.Wrappers.Twitch;
using Area.Wrappers.Twitch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.App
{
    public class TwitchService : ApiService, IService
    {
        private readonly ApplicationDbContext _context;
        private readonly TwitchWrapper _twitchWrapper;

        public TwitchService(ApplicationDbContext context,
            TwitchWrapper twitchWrapper) : base(context, ServiceTypeEnum.Twitch)
        {
            _context = context;
            _twitchWrapper = twitchWrapper;
        }

        public override IViewModel GenerateToken(Account owner, string code)
        {
            Console.WriteLine($"TwitchService(GenerateTwitchToken): The user code is {code}");
            var result = _twitchWrapper.GenerateTwitchToken(code);
            if (!result.Success)
            {
                Console.WriteLine("TwitchService(GenerateTwitchToken): Failed to get token");
                return new ErrorViewModel() { Error = (result as RequestFailedModel).Error };
            }
            _context.Tokens.RemoveRange(_context.Tokens.Where(t => t.Owner.Id == owner.Id && t.Type == ServiceTypeEnum.Twitch));
            var tokenModel = result as TwitchTokenModel;
            var token = new Token()
            {
                AccessToken = tokenModel.Access_Token,
                RefreshToken = tokenModel.Refresh_Token,
                ExpireIn = tokenModel.Expires_In,
                Owner = owner,
                Type = ServiceTypeEnum.Twitch
            };
            _context.Tokens.Add(token);
            _context.SaveChanges();
            Console.WriteLine("TwitchService(GenerateTwitchToken): Token successfully saved");
            return new SuccessViewModel();
        }
    }
}
