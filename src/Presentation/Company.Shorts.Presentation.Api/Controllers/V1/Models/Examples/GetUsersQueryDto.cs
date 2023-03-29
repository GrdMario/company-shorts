namespace Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples
{
    using System;

    public record GetUsersQueryDto() : IApiDto;

    public record GetUserByIdQueryDto(Guid Id) : IApiDto;

    public record CreateUserCommandDto(string UserName, string Email, string Address, string ProfilePicture) : IApiDto;
}
