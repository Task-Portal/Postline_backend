using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> UpdateUserAsync(string name ,UserForUpdateDto userForUpdate, bool trackChanges);
        Task<UserForUpdateMe> GetUser(string name, bool trackChanges);
    }
}