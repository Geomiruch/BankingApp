using BankingApp.DAL.Repositories.Implementation;
using BankingApp.DAL.Repositories;
using BankingApp.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.BLL.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterDbContext(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<BankingDbContext>(db => db.UseSqlServer(connectionString));
        }

        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAccountRepository, AccountRepository>();
            serviceCollection.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
