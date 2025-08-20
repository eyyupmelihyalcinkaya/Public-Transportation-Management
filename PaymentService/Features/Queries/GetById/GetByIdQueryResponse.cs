namespace PaymentService.Features.Queries.GetById
{
    public class GetByIdQueryResponse
    {
        public int Id { get; set; }
        
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }

    }
}
