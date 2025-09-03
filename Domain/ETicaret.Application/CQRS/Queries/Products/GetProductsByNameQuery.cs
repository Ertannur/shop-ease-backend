using ETicaret.Application.CQRS.Results.Products;
using MediatR;

namespace ETicaret.Application.CQRS.Queries.Products;

public class GetProductsByNameQuery : IRequest<GetProductsByNameQueryResult>
{
    public string? ProductName { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}