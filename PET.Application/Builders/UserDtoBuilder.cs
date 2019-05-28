using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class UserDtoBuilder : IUserDtoBuilder
    {
        public UserDto Build(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Animals = user.Animals   
            };
        }
    }
}