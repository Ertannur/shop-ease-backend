using ETicaret.Application.CQRS.Results.Products;
using MediatR;

namespace ETicaret.Application.CQRS.Queries.Products;

public class GetProductByIdQuery : IRequest<GetProductByIdQueryResult>
{
    public Guid Id { get; set; }
}