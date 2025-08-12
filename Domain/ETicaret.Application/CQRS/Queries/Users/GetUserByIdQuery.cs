using ETicaret.Application.CQRS.Results.Users;
using MediatR;

namespace ETicaret.Application.CQRS.Queries.Users;

public class GetUserByIdQuery : IRequest<GetUserByIdQueryResult?>
{
    public Guid Id { get; set; }
}