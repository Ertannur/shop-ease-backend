using ETicaret.Application.Abstractions;
using ETicaret.Application.Configurations;
using ETicaret.Application.CQRS.Queries.Users;
using ETicaret.Application.CQRS.Results.Users;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Users;

public class GetUserByIdQueryHandler(IUserService userService) : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResult?>
{
    public async Task<GetUserByIdQueryResult?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await userService.GetUserByIdAsync(request.Id);
        if (result is null)
            return null;
        return ModelMapper.MapGetUserByIdQueryResult(result);
    }
}