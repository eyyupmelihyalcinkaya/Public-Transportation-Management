namespace PaymentService.Events
{
    public class BoardingCompletedEvent
    {
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? VehicleType { get; set; }

    }
}
