using ETicaret.Application.CQRS.Results.Products;
using MediatR;

namespace ETicaret.Application.CQRS.Queries.Products;

public class GetProductsQuery : IRequest<GetProductsQueryResult>
{
    
    public string? Category { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}