using ETicaret.Application.CQRS.Results.Products;
using MediatR;

namespace ETicaret.Application.CQRS.Queries.Products;

public class GetFilteredProductsQuery : IRequest<GetFilteredProductsQueryResult>
{
    public string? Type { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}