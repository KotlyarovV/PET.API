using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class UserDtoBuilder : IUserDtoBuilder
    {
        public UserRegisterDto Build(User user)
        {
            return new UserRegisterDto
            {
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}