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

        public Token GetToken(Account user)
        {
            return user.Tokens.Where(t => t.Type == ServiceTypeEnum.Reddit).FirstOrDefault();
        }

        /*public Token GetToken(Account user)
        {
            var tmp = user.Tokens.Where(t => t.Type == ServiceTypeEnum.Reddit).FirstOrDefault();

            return tmp;
            if (tmp == null)
                return null;
            var result = _redditWrapper.RefreshRedditToken(tmp);
            if (!result.Success)
                return null;
            var model = result as RedditTokenModel;
            tmp.AccessToken = model.Access_Token;
            return tmp;
        }*/

        public RedditAPI GetApi(Token token)
        {
            RedditAPI api = new RedditAPI("hfTIumnrQMLA_A", token.RefreshToken, "9wp2Hj1WzmAMSmS1srmgi99UdYM", token.AccessToken);

            return api;
        }
	}
}


