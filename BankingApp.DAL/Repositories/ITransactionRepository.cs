using BankingApp.DAL.Models;

namespace BankingApp.DAL.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransaction(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountNumber(string accountNumber);
    }
}
