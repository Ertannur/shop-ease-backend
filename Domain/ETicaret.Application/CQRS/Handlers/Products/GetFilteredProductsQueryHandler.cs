using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Queries.Products;
using ETicaret.Application.CQRS.Results.Products;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Products;

public class GetFilteredProductsQueryHandler(IProductService productService) : IRequestHandler<GetFilteredProductsQuery,GetFilteredProductsQueryResult>
{
    public async Task<GetFilteredProductsQueryResult> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
    {
        if (request.CurrentPage == 0 || request.PageSize == 0)
        {
            var result = await productService.GetFilteredProducts(request.Type);
            return new GetFilteredProductsQueryResult()
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
            var result = await productService.GetFilteredProducts(request.Type, request.CurrentPage, request.PageSize);
            return new GetFilteredProductsQueryResult()
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