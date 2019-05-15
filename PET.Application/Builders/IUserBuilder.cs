using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public interface IUserBuilder
    {
        User Build(UserSaveDto userSaveDto);
    }
}