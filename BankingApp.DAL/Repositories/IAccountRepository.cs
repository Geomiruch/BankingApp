using BankingApp.DAL.Models;

namespace BankingApp.DAL.Repositories
{
    public interface IAccountRepository
    {
        Task AddAccount(Account account);
        Task<Account> GetAccountByNumber(string accountNumber); 
        Task<IEnumerable<Account>> GetAllAccounts();
        Task UpdateAccount(Account account);
    }
}
