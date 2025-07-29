using MediatR;
using PaymentService.Enums;
namespace PaymentService.Features.Commands.CreateBoardingTransaction
{
    public class CreateBoardingTransactionCommandRequest : IRequest<CreateBoardingTransactionCommandResponse>
    {
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? VehicleType { get; set; }
        public string Status { get; set; }

    }
}
