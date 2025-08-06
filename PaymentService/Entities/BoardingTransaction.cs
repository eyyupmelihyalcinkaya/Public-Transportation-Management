using PaymentService.Enums;

namespace PaymentService.Entities
{
    public class BoardingTransaction
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? VehicleType { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Status { get; set; }
        public bool isStudent { get; set; }
    }
}
