﻿using System;
using System.Threading.Tasks;
using PET.Application.Builders;
using PET.Application.DTOs;
using PET.Domain.Models;
using PET.Domain.Specifications;

namespace PET.Application.Services
{
    public class UserAppService
    {
        private readonly IDataService<User> userDataService;
        private readonly IUserDtoBuilder userDtoBuilder;
        private readonly IUserBuilder userBuilder;

        public UserAppService(IDataService<User> userDataService, IUserDtoBuilder userDtoBuilder, IUserBuilder userBuilder)
        {
            this.userDataService = userDataService;
            this.userDtoBuilder = userDtoBuilder;
            this.userBuilder = userBuilder;
        }

        public async Task<UserSaveDto> Get(string email)
        {
            var spec = new UserEmailSpecification(email);
            var user = await userDataService.GetAsync(spec);
            var userDto = userDtoBuilder.Build(user);
            return userDto;
        }

        public async Task<Guid> Create(UserSaveDto userSaveDto)
        {
            var user = userBuilder.Build(userSaveDto); 
            await userDataService.AddAsync(user);

            return user.Id;
        }
    }
}