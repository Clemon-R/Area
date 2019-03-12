using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Area.Models
{
    public class Account
    {
        public Account()
        {
            Triggers = new List<Trigger>();
            Tokens = new List<Token>();
        }
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public ICollection<Trigger> Triggers { get; set; }
        public ICollection<Token> Tokens { get; set; }
    }
}
