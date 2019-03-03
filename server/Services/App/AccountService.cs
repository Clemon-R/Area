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
    public class AccountService : IService
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

        public AccountViewModel GetModel(Account account)
        {
            AccountViewModel result = new AccountViewModel()
            {
                Token = account.Token,
                Username = account.UserName
            };
            return result;
        }

        public IViewModel Register(RegisterViewModel model)
        {
            IViewModel result;
            try
            {
                Console.WriteLine("AccountService(Register): Trying to register");
                if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.PasswordConfirm))
                {
                    Console.WriteLine("AccountService(Register): Field empty");
                    return new ErrorViewModel() { Error = "Veuillez remplir tout les champs" };
                }
                else if (!model.Password.Equals(model.PasswordConfirm))
                {
                    Console.WriteLine("AccountService(Register): Wrong password");
                    return new ErrorViewModel() { Error = "Les mot de passe ne sont pas les même" };
                } else if (_context.Accounts.Any(a => a.UserName.ToLower().Equals(model.UserName.ToLower())))
                {
                    Console.WriteLine($"AccountService(Register): UserName already exist({model.UserName})");
                    return new ErrorViewModel() { Error="Nom de compte déjà existant"};
                }
                var account = new Account()
                {
                    UserName = model.UserName,
                    Password = model.Password
                };
                _context.Add(account);
                _context.SaveChanges();
                result = new SuccessViewModel();
                Console.WriteLine("AccountService(Register): Registered");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"AccountService(Login): {e.Message}\n{e.StackTrace}");
                result = new ErrorViewModel();
            }
            return result;
        }

        public IViewModel Login(LoginViewModel model)
        {
            return Login(model.UserName, model.Password);
        }

        public IViewModel Login(string username, string password)
        {
            IViewModel result;
            try
            {
                Console.WriteLine("AccountService(Login): Trying to connect");
                Account current = _context.Accounts
                    .Where(account => account.UserName.ToLower().Equals(username.ToLower()) && account.Password.Equals(password))
                    .FirstOrDefault();

                if (current == null)
                {
                    Console.WriteLine("AccountService(Login): Impossible to connect");
                    return new ErrorViewModel() { Error = "Nom de compte ou mot de passe incorrect" };
                }
                return Login(current);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"AccountService(Login): {e.Message}\n{e.StackTrace}");
                result = new ErrorViewModel();
            }
            return result;
        }

        public IViewModel Login(Account current)
        {
            IViewModel result;
            try
            {
                byte[] encodedPassword = new UTF8Encoding().GetBytes($"{current.Id}{current.UserName}{current.Password}");
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

                if (current.Token == null || !current.Token.Equals(encoded))
                {
                    current.Token = encoded;
                    Save(current);
                }
                result = GetModel(current);
                Console.WriteLine($"AccountService(Login): Account({current.Id}) connected");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"AccountService(Login): {e.Message}\n{e.StackTrace}");
                result = new ErrorViewModel();
            }
            return result;
        }

        public IViewModel Logout(Account current)
        {
            IViewModel result = new SuccessViewModel();
            try
            {
                Console.WriteLine($"AccountService(Logout): Deconnection account({current.Id})");
                current.Token = null;
                _context.Update(current);
                _context.SaveChanges();
            }catch (Exception e)
            {
                Console.Error.WriteLine($"AccountService(Logout): {e.Message}\n{e.StackTrace}");
                result = new ErrorViewModel();
            }
            return result;
        }

        public void Save(Account current)
        {
            _context.Update(current);
            _context.SaveChanges();
        }
    }
}
