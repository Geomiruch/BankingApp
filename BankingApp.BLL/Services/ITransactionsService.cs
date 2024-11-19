namespace BankingApp.BLL.Services
{
    public interface ITransactionService
    {
        Task<bool> Deposit(string accountNumber, decimal amount); 
        Task<bool> Withdraw(string accountNumber, decimal amount); 
        Task<bool> Transfer(string fromAccountNumber, string toAccountNumber, decimal amount); 
    }
}
