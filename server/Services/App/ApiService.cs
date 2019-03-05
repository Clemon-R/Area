using Area.Enums;
using Area.Models;
using Area.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Services.App
{
    public abstract class ApiService
    {
        private readonly ApplicationDbContext _context;
        private ServiceTypeEnum _type;

        public ApiService(
            ApplicationDbContext context,
            ServiceTypeEnum type)
        {
            _context = context;
            _type = type;
        }

        public IViewModel IsTokenAvailable(Account owner)
        {
            return !_context.Tokens.Where(t => t.Owner.Id == owner.Id && t.Type == _type).Any()
                ? (IViewModel)new ErrorViewModel()
                : (IViewModel)new SuccessViewModel();
        }

        public abstract IViewModel GenerateToken(Account owner, string code);
    }
}
