using MediatR;

namespace PaymentService.Features.Queries.GetById
{
    public class GetByIdQueryRequest : IRequest<GetByIdQueryResponse>
    {
        public int Id { get; set; }
        public GetByIdQueryRequest(int id)
        {
            Id = id;
        }
    }
}
