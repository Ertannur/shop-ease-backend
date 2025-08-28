using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Queries.Users;
using ETicaret.Application.CQRS.Results.Users;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Users;

public class GetCurrentUserQueryHandler(IUserService userService) : IRequestHandler<GetCurrentUserQuery, GetCurrentUserQueryResult>
{
    public async Task<GetCurrentUserQueryResult> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var result = await userService.GetCurrentUserAsync();
        return new()
        {
            UserId = result.UserId,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            Roles = result.Roles,
            BirthDate = result.BirthDate,
            Gender = result.Gender,
            PhoneNumber = result.PhoneNumber
        };
    }
}