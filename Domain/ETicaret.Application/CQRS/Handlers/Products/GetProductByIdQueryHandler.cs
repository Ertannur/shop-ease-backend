using ETicaret.Application.Abstractions;
using ETicaret.Application.Configurations;
using ETicaret.Application.CQRS.Queries.Products;
using ETicaret.Application.CQRS.Results.Products;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Products;

public class GetProductByIdQueryHandler(IProductService productService) : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResult>
{
    public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await productService.GetProductByIdAsync(request.Id);
        return ModelMapper.MapGetProductByIdQueryResult(result);
    }
}