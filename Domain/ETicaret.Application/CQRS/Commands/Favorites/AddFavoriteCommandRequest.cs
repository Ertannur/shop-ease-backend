using ETicaret.Application.CQRS.Results.Favorites;
using ETicaret.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Application.CQRS.Commands.Favorites
{
    public class AddFavoriteCommandRequest:IRequest<AddFavoritesCommandResult>
    {

        public Guid ProductId { get; set; }


    }
}
