using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public interface IUserDtoBuilder
    {
        UserDto Build(User user);
    }
}