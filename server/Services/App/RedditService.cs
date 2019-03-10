using System;
using System.Linq;
using Area.Enums;
using Area.Models;
using Area.ViewModels;
using Area.Wrappers.Models;
using Reddit;
using server.Wrappers.Reddit;
using server.Wrappers.Reddit.Models;

namespace Area.Services.App
{
    public class RedditService : ApiService, IService
    {
        private readonly RedditWrapper _redditWrapper;
        private readonly ApplicationDbContext _context;

		public RedditService(ApplicationDbContext context, RedditWrapper redditWrapper) : base(context, ServiceTypeEnum.Reddit)
		{
            _redditWrapper = redditWrapper;
            _context = context;
		}

		public override IViewModel GenerateToken(Account owner, string code)
		{
            Console.WriteLine($"RedditService(GenerateToken): The user code is {code}");
            var result = _redditWrapper.GenerateRedditToken(code);
            if (!result.Success)
            {
                Console.WriteLine("RedditService(GenerateToken): Failed to get token");
                return new ErrorViewModel() { Error = (result as RequestFailedModel).Error };
            }
            _context.Tokens.RemoveRange(owner.Tokens.Where(t => t.Type == Enums.ServiceTypeEnum.Reddit));
            var tokenModel = result as RedditTokenModel;
            var token = new Token()
            {
                AccessToken = tokenModel.Access_Token,
                RefreshToken = tokenModel.Rrefresh_Token,
                ExpireIn = tokenModel.Expires_In,
                Type = Enums.ServiceTypeEnum.Reddit
            };
            _context.Tokens.Add(token);
            owner.Tokens.Add(token);
            _context.Update(owner);
            _context.SaveChanges();
            Console.WriteLine("RedditService(GenerateToken): Token successfully saved");
            return new SuccessViewModel();
		}

        public RedditAPI GetApi(Account user)
        {
            RedditAPI api = new RedditAPI("appId", "refreshToken", "appSecret", "accessToken");

            return api;
        }
	}
}