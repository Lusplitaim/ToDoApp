﻿using Microsoft.AspNetCore.Identity;
using ToDoApp.Core.Data;
using ToDoApp.Core.Data.Entities;
using ToDoApp.Core.DTOs.User;
using ToDoApp.Core.Exceptions;
using ToDoApp.Core.Models;
using ToDoApp.Core.Models.Constants;

namespace ToDoApp.Core.Storages
{
    internal class UserStorage : IUserStorage
    {
        private readonly UserManager<UserEntity> m_UserManager;
        private readonly IUnitOfWork m_UnitOfWork;
        public UserStorage(UserManager<UserEntity> userManager, IUnitOfWork uow)
        {
            m_UserManager = userManager;
            m_UnitOfWork = uow;
        }

        public async Task<ExecResult> CreateAsync(RegisterUserDto model)
        {
            var result = new ExecResult();

            UserEntity user = new()
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var createResult = await m_UserManager.CreateAsync(user, model.Password);
            if (!createResult.Succeeded)
            {
                result.AddErrors(createResult);
                return result;
            }

            var roleAddResult = await m_UserManager.AddToRoleAsync(user, UserRoleConstants.User);
            if (!roleAddResult.Succeeded)
            {
                result.AddErrors(roleAddResult);
                return result;
            }

            return result;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var userEntity = await m_UserManager.FindByEmailAsync(email);
            if (userEntity is null)
            {
                throw new NotFoundCoreException();
            }

            var roles = await m_UserManager.GetRolesAsync(userEntity);

            UserDto result = new()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                Email = userEntity.Email,
            };

            return result;
        }

        public async Task<List<UserDto>> GetAsync()
        {
            var entities = await m_UnitOfWork.UserRepository.GetAsync();
            return entities.Select(UserDto.From).ToList();
        }
    }
}
