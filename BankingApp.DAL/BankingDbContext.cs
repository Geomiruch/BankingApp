using BankingApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.DAL
{
    public class BankingDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
        }
    }
}
