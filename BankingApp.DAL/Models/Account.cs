namespace BankingApp.DAL.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = Guid.NewGuid().ToString();
        public string OwnerName { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
