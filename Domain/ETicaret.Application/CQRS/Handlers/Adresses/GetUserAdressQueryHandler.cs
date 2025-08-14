using ETicaret.Application.Abstractions;
using ETicaret.Application.CQRS.Queries.Adresses;
using ETicaret.Application.CQRS.Results.Adress;
using MediatR;

namespace ETicaret.Application.CQRS.Handlers.Adresses;

public class GetUserAdressQueryHandler(IAdressService adressService) : IRequestHandler<GetUserAdressQuery, IEnumerable<GetUserAdressQueryResult>>
{
    public async Task<IEnumerable<GetUserAdressQueryResult>> Handle(GetUserAdressQuery request, CancellationToken cancellationToken)
    {
       var result = await adressService.GetUserAdressAsync(request.UserId);
       return result.Select(x=> new GetUserAdressQueryResult()
       {
           Address = x.Address,
           City = x.City,
           District = x.District,
           Email = x.Email,
           Name = x.Name,
           Surname = x.Surname,
           Phone = x.Phone,
           PostCode = x.PostCode,
           Title = x.Title,
           AdressId = x.AdressId
       });
    }
}