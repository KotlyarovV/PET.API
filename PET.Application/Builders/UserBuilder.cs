using System;
using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class UserBuilder : IUserBuilder
    {
        public User Build(UserRegisterDto userRegisterDto)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = userRegisterDto.Email,
                Password = userRegisterDto.Password,
                Name = userRegisterDto.Name
            };
        }

        public User Build(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Password = userDto.Password,
                Name = userDto.Name,
                Animals = userDto.Animals
            };
        }
    }
}