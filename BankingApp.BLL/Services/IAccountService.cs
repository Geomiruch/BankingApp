using BankingApp.DAL.Models;

namespace BankingApp.BLL.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccount(string owner, decimal initialBalance);
        Task<Account> GetAccountDetails(string accountNumber); 
        Task<IEnumerable<Account>> GetAllAccounts();
    }
}
