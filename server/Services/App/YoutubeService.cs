using System;
using System.Linq;
using Area.Enums;
using Area.Models;
using Area.ViewModels;
using Area.Wrappers.Models;
using Area.Wrappers.Youtube;
using Area.Wrappers.Youtube.Models;
using Google.Apis.Services;

namespace Area.Services.App
{
    public class YoutubeService : ApiService, IService
    {
        private readonly YoutubeWrapper _youtubeWrapper;
        private readonly ApplicationDbContext _context;

        public YoutubeService(ApplicationDbContext context, YoutubeWrapper youtubeWrapper) : base(context, ServiceTypeEnum.Youtube)
        {
            _youtubeWrapper = youtubeWrapper;
            _context = context;
        }

        public override IViewModel GenerateToken(Account owner, string code)
        {
            Console.WriteLine($"YoutubeService(GenerateToken): The user code is {code}");
            var result = _youtubeWrapper.GenerateYoutubeToken(code);
            if (!result.Success)
            {
                Console.WriteLine("YoutubeService(GenerateToken): Failed to get token");
                return new ErrorViewModel() { Error = (result as RequestFailedModel).Error };
            }
            _context.Tokens.RemoveRange(owner.Tokens.Where(t => t.Type == Enums.ServiceTypeEnum.Youtube));
            var tokenModel = result as YoutubeTokenModel;
            var token = new Token()
            {
                AccessToken = tokenModel.Access_Token,
                RefreshToken = tokenModel.Rrefresh_Token,
                ExpireIn = tokenModel.Expires_In,
                Type = Enums.ServiceTypeEnum.Youtube
            };
            _context.Tokens.Add(token);
            owner.Tokens.Add(token);
            _context.Update(owner);
            _context.SaveChanges();
            Console.WriteLine("YoutubeService(GenerateToken): Token successfully saved");
            return new SuccessViewModel();
        }

        public Google.Apis.YouTube.v3.YouTubeService GetApi(Account user)
        {
            var api = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
            {
                ApplicationName = "Area",
                ApiKey = "473291186491-5tiqjh1gb7uoiv717utbf8ibjgcvfbj0",
            });

            return api;
        }
    }
}