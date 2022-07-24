using System;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Exceptions.NotFoundExceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> UpdateUserAsync(string name, UserForUpdateDto userForUpdate,
            bool trackChanges)
        {
            User user = await _userManager.FindByNameAsync(name);

            if (user is null)
                throw new UserNotFoundException(new Guid(name));


            
          
            user.FirstName = userForUpdate.FirstName;
            user.LastName = userForUpdate.LastName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
               result =  await _userManager.SetTwoFactorEnabledAsync(user, userForUpdate.IsTwoFactorAuthorizationEnabled);
            }

            return result;
        }

        public async Task<UserForUpdateMe> GetUser(string name, bool trackChanges)
        {
            User user = await _userManager.FindByNameAsync(name);   

            if (user is null)
                throw new UserNotFoundException(new Guid(name));

            var result = _mapper.Map<UserForUpdateMe>(user);
            return result;
        }
    }
}