using Area.Models;
using Area.ViewModels;
using Area.Wrappers.Models;
using Area.Wrappers.Yammer;
using Area.Wrappers.Yammer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.App
{
    public class YammerService : ApiService, IService
    {
        private readonly ApplicationDbContext _context;
        private readonly YammerWrapper _yammerWrapper;

        public YammerService(ApplicationDbContext context,
            YammerWrapper yammerWrapper) : base(context, Enums.ServiceTypeEnum.Yammer)
        {
            _context = context;
            _yammerWrapper = yammerWrapper;
        }

        public override IViewModel GenerateToken(Account owner, string code)
        {
            Console.WriteLine($"YammerService(GenerateYammerToken): The user code is {code}");
            var result = _yammerWrapper.GenerateYammerToken(code);
            if (!result.Success)
            {
                Console.WriteLine("YammerService(GenerateYammerToken): Failed to get token");
                return new ErrorViewModel() { Error = (result as RequestFailedModel).Error };
            }
            _context.Tokens.RemoveRange(owner.Tokens.Where(t => t.Type == Enums.ServiceTypeEnum.Yammer));
            var tokenModel = result as YammerTokenModel;
            var token = new Token()
            {
                AccessToken = tokenModel.Token,
                RefreshToken = null,
                ExpireIn = -1,
                Type = Enums.ServiceTypeEnum.Yammer
            };
            _context.Tokens.Add(token);
            owner.Tokens.Add(token);
            _context.Update(owner);
            _context.SaveChanges();
            Console.WriteLine("YammerService(GenerateYammerToken): Token successfully saved");
            return new SuccessViewModel();
        }

        public Token GetToken(Account user)
        {
            return user.Tokens.Where(t => t.Type == Enums.ServiceTypeEnum.Yammer).FirstOrDefault();
        }
    }
}
