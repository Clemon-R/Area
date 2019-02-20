using Area.Models;
using Area.ViewModels;
using Area.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Area.Services.App
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;
        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewModel Login(LoginViewModel model)
        {
            Console.WriteLine("AccountService(Login): Trying to connect");
            Account current = _context.Accounts
                .Where(account => account.UserName.ToLower().Equals(model.UserName.ToLower()) && account.Password.Equals(model.Password))
                .FirstOrDefault();

            if (current == null)
            {
                Console.WriteLine("AccountService(Login): Impossible to connect");
                return new ErrorViewModel() { Error = "Nom de compte ou mot de passe incorrect" };
            }
            byte[] encodedPassword = new UTF8Encoding().GetBytes($"{current.Id}{current.UserName}{current.Password}");
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            AccountViewModel result = new AccountViewModel()
            {
                Token = encoded
            };
            Console.WriteLine($"AccountService(Login): Account({current.Id}) connected");
            return result;
        }
    }
}
