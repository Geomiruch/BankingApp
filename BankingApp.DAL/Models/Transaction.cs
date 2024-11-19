using BankingApp.DAL.Models.Enums;

namespace BankingApp.DAL.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
