using BankingApp.DAL.Models;
using BankingApp.DAL.Models.Enums;
using BankingApp.DAL.Repositories;

namespace BankingApp.BLL.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> Deposit(string accountNumber, decimal amount)
        {
            var account = await _accountRepository.GetAccountByNumber(accountNumber);
            if (account == null || amount <= 0) return false;

            account.Balance += amount;
            await _accountRepository.UpdateAccount(account);

            var transaction = new Transaction
            {
                AccountNumber = accountNumber,
                Amount = amount,
                Type = TransactionType.Deposit
            };
            await _transactionRepository.AddTransaction(transaction);
            return true;
        }

        public async Task<bool> Withdraw(string accountNumber, decimal amount)
        {
            var account = await _accountRepository.GetAccountByNumber(accountNumber);
            if (account == null || amount <= 0 || account.Balance < amount) return false;

            account.Balance -= amount;
            await _accountRepository.UpdateAccount(account);

            var transaction = new Transaction
            {
                AccountNumber = accountNumber,
                Amount = amount,
                Type = TransactionType.Withdraw
            };
            await _transactionRepository.AddTransaction(transaction);
            return true;
        }

        public async Task<bool> Transfer(string fromAccountNumber, string toAccountNumber, decimal amount)
        {
            var fromAccount = await _accountRepository.GetAccountByNumber(fromAccountNumber);
            var toAccount = await _accountRepository.GetAccountByNumber(toAccountNumber);

            if (fromAccount == null || toAccount == null || amount <= 0 || fromAccount.Balance < amount)
                return false;

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            await _accountRepository.UpdateAccount(fromAccount);
            await _accountRepository.UpdateAccount(toAccount);

            var transactionFrom = new Transaction
            {
                AccountNumber = fromAccountNumber,
                Amount = amount,
                Type = TransactionType.Transfer
            };
            await _transactionRepository.AddTransaction(transactionFrom);

            var transactionTo = new Transaction
            {
                AccountNumber = toAccountNumber,
                Amount = amount,
                Type = TransactionType.Transfer
            };
            await _transactionRepository.AddTransaction(transactionTo);

            return true;
        }
    }
}
