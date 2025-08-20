using PaymentService.Enums;

namespace PaymentService.Events
{
    public class BoardingCompletedEvent
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? VehicleType { get; set; }
        public bool isStudent { get; set; }

    }
} 