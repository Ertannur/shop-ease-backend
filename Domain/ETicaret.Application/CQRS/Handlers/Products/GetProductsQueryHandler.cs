using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Queries.Products;
using ETicaret.Application.CQRS.Results.Products;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Products;

public class GetProductsQueryHandler(IProductService productService) : IRequestHandler<GetProductsQuery, GetProductsQueryResult>
{
    public async Task<GetProductsQueryResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        if (request.CurrentPage == 0 || request.PageSize == 0)
        {
            var result = await productService.GetProductsAsync(request.Category);
            return new GetProductsQueryResult()
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
            var result = await productService.GetProductsAsync(request.Category, request.CurrentPage, request.PageSize);
            return new GetProductsQueryResult()
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