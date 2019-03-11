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
using TwitchLib.Api;

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
            _context.Tokens.RemoveRange(owner.Tokens.Where(t => t.Type == ServiceTypeEnum.Twitch));
            var tokenModel = result as TwitchTokenModel;
            var token = new Token()
            {
                AccessToken = tokenModel.Access_Token,
                RefreshToken = tokenModel.Refresh_Token,
                ExpireIn = tokenModel.Expires_In,
                Type = ServiceTypeEnum.Twitch
            };
            _context.Tokens.Add(token);
            owner.Tokens.Add(token);
            _context.Update(owner);
            _context.SaveChanges();
            Console.WriteLine("TwitchService(GenerateTwitchToken): Token successfully saved");
            return new SuccessViewModel();
        }

        public Token GetToken(Account user)
        {
            var tmp = user.Tokens.Where(t => t.Type == ServiceTypeEnum.Twitch).FirstOrDefault();
            if (tmp == null)
                return null;
            var result = _twitchWrapper.RefreshTwitchToken(tmp);
            if (!result.Success)
                return null;
            var model = result as TwitchTokenModel;
            tmp.AccessToken = model.Access_Token;
            return tmp;
        }

        public TwitchAPI GetApi(Token token)
        {
            TwitchAPI api = new TwitchAPI();

            api.Settings.Scopes.Add(TwitchLib.Api.Core.Enums.AuthScopes.Channel_Subscriptions);
            api.Settings.ClientId = "g2kkfu5px956qtxvzfvsi9jbqhip4n";
            api.Settings.AccessToken = token.AccessToken;
            api.Settings.Secret = "a2w11c1wu4q8mb5wqjzf4q5njq41co";
            return api;
        }
    }
}
