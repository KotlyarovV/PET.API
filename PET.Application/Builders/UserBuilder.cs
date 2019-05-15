using System;
using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{
    public class UserBuilder : IUserBuilder
    {
        public User Build(UserSaveDto userSaveDto)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = userSaveDto.Email,
                Password = userSaveDto.Password
            };
        }
    }
}