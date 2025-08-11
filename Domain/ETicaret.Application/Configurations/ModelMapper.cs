using ETicaret.Application.CQRS.Commands.Auths;
using ETicaret.Application.CQRS.Queries.Products;
using ETicaret.Application.CQRS.Results.Auths;
using ETicaret.Application.CQRS.Results.Products;
using ETicaret.Application.DTOs.Auths.Requests;
using ETicaret.Application.DTOs.Auths.Results;
using ETicaret.Application.DTOs.Products.Results;
using Riok.Mapperly.Abstractions;

namespace ETicaret.Application.Configurations;
[Mapper]
public static partial class ModelMapper
{
    public static partial RegisterDto MapRegisterDto(RegisterUserCommandRequest request);
    public static partial RegisterUserCommandResult MapRegisterUserCommandResult(RegisterResultDto registerResultDto);
    
    public static partial LoginDto MapLoginDto(LoginUserCommandRequest request);
    public static partial LoginUserCommandResult MapLoginUserCommandResult(LoginResultDto loginResultDto);
    public static partial GetProductByIdQueryResult MapGetProductByIdQueryResult(GetProductByIdResultDto  productResultDto);
}