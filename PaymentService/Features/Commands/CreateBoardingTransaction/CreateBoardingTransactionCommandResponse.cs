namespace PaymentService.Features.Commands.CreateBoardingTransaction
{
    public class CreateBoardingTransactionCommandResponse
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
