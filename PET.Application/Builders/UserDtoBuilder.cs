using System;
using PET.Application.DTOs;
using PET.Domain.Models;

namespace PET.Application.Builders
{

    public interface IUserDtoBuilder
    {
        UserSaveDto Build(User user);
    }

    public interface IUserBuilder
    {
        User Build(UserSaveDto userSaveDto);
    }

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