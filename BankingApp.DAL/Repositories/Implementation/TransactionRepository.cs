using BankingApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.DAL.Repositories.Implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingDbContext _context;

        public TransactionRepository(BankingDbContext context)
        {
            _context = context;
        }

        public async Task AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountNumber(string accountNumber)
        {
            return await _context.Transactions
                .Where(t => t.AccountNumber == accountNumber).ToListAsync();
        }
    }
}
