using Area.Models;
using Microsoft.EntityFrameworkCore;

namespace Area
{
    public class ApplicationDbContext : DbContext
    {
        public static ApplicationDbContext Instance { get; private set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=db;Database=master;User=sa;Password=Your_password123;");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}