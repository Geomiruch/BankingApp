using BankingApp.BLL.Services.Implementation;
using BankingApp.BLL.Services;
using BankingApp.Controllers;
using BankingApp.DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using BankingApp.DAL;

namespace BankingAppTests
{
    public class BankingAppTestFixture : IDisposable
    {
        public BankingDbContext Context { get; private set; }
        public IAccountService AccountService { get; private set; }
        public ITransactionService TransactionService { get; private set; }
        public AccountController AccountController { get; private set; }
        public TransactionController TransactionController { get; private set; }

        public BankingAppTestFixture()
        {
            var options = new DbContextOptionsBuilder<BankingDbContext>()
                .UseInMemoryDatabase("BankingAppTestDb")  
                .Options;

            Context = new BankingDbContext(options);

            AccountService = new AccountService(new AccountRepository(Context));
            TransactionService = new TransactionService(new AccountRepository(Context), new TransactionRepository(Context));

            AccountController = new AccountController(AccountService);
            TransactionController = new TransactionController(TransactionService);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }

}
