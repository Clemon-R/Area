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

        public Account GetAccount(IConnectedViewModel model)
        {
            Console.WriteLine($"AccountService(GetAccount): Getting account for token({model.Token})");
            Account current = _context.Accounts
                .Where(account => account.Token.Equals(model.Token))
                .FirstOrDefault();
            return current;
        }

        public ErrorViewModel Register(RegisterViewModel model)
        {
            Console.WriteLine("AccountService(Register): Trying to register");
            if (!model.Password.Equals(model.PasswordConfirm))
            {
                Console.WriteLine("AccountService(Register): Wrong password");
                return new ErrorViewModel() { Error = "Les mot de passe ne sont pas les même" };
            } else if (_context.Accounts.Any(a => a.UserName.Equals(model.UserName)))
            {
                Console.WriteLine($"AccountService(Register): UserName already exist({model.UserName})");
                return new ErrorViewModel() { Error="Nom de compte déjà existent"};
            }
            var account = new Account()
            {
                UserName = model.UserName,
                Password = model.Password
            };
            _context.Add(account);
            _context.SaveChanges();
            Console.WriteLine("AccountService(Register): Registered");
            return null;
        }

        public IViewModel Login(LoginViewModel model)
        {
            foreach (var account in _context.Accounts)
                Console.WriteLine($"{account.UserName} {account.Password}");
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
            
            AccountViewModel result = new AccountViewModel(){
                Token = encoded
            };
            if (current.Token == null || !current.Token.Equals(encoded)){
                current.Token = encoded;
                Save(current);
            }
            Console.WriteLine($"AccountService(Login): Account({current.Id}) connected");
            return result;
        }

        public void Logout(Account current)
        {
            Console.WriteLine($"AccountService(Logout): Donnection account({current.Id})");
            current.Token = null;
            _context.Update(current);
            _context.SaveChanges();
        }

        public void Save(Account current)
        {
            _context.Update(current);
            _context.SaveChanges();
        }
    }
}
