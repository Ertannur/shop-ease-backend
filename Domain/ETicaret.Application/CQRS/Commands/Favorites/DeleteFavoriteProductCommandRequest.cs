using MediatR;

namespace ETicaret.Application.CQRS.Commands.Favorites;

public class DeleteFavoriteProductCommandRequest : IRequest<bool>
{
    public Guid ProductId { get; set; }
}