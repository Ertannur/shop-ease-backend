using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Queries.Products;
using ETicaret.Application.CQRS.Results.Products;
using ETicaret.Application.DTOs.Products.Results;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Products;

public class GetProductsByNameQueryHandler(IProductService productService) : IRequestHandler<GetProductsByNameQuery, GetProductsByNameQueryResult>
{
    public async Task<GetProductsByNameQueryResult> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        if (request.CurrentPage == 0 || request.PageSize == 0)
        {
            var result = await productService.GetProductsByNameAsync(request.ProductName);
            return new GetProductsByNameQueryResult()
            {
                Products = result.Products,
                TotalCount = result.TotalCount,
                TotalPage = result.TotalPage,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage,
            };
        }
        else
        {
            var result = await productService.GetProductsByNameAsync(request.ProductName, request.CurrentPage, request.PageSize);
            return new GetProductsByNameQueryResult()
            {
                Products = result.Products,
                TotalCount = result.TotalCount,
                TotalPage = result.TotalPage,
                HasNextPage = result.HasNextPage,
                HasPreviousPage = result.HasPreviousPage,
            };
        }
    }
}