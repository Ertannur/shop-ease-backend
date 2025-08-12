using ETicaret.Application.Abstractions;
using ETicaret.Application.Configurations;
using ETicaret.Application.CQRS.Commands.Products;
using ETicaret.Application.CQRS.Results.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Application.CQRS.Handlers.Products
{
    public class AddProductCommandHandler(IProductService productService) : IRequestHandler<AddProductCommandRequest, AddProductCommandResult>
    {
        

        public async Task<AddProductCommandResult> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            var mapper = ModelMapper.MapAddProductRequest(request);
            var result = await productService.AddProductAsync(mapper);
            
            return new AddProductCommandResult() { Message = result.Message, Success=result.Success };

        }
    }
}
