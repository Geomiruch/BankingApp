using BankingApp.DAL.Models;
using BankingApp.DAL.Repositories;

namespace BankingApp.BLL.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAccount(string owner, decimal initialBalance)
        {
            var account = new Account { OwnerName = owner, Balance = initialBalance };
            await _accountRepository.AddAccount(account);
            return account;
        }

        public async Task<Account> GetAccountDetails(string accountNumber)
        {
            return await _accountRepository.GetAccountByNumber(accountNumber);
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAllAccounts();
        }
    }
}
