using ETicaret.Application.CQRS.Results.Products;
using ETicaret.Application.DTOs.Products.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ETicaret.Application.CQRS.Commands.Products
{
    public class AddProductCommandRequest:IRequest<AddProductCommandResult>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }

    }
}
