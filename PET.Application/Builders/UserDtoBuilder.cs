using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class UserDtoBuilder : IUserDtoBuilder
    {
        public UserSaveDto Build(User user)
        {
            return new UserSaveDto
            {
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}