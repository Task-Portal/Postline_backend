using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postline.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.ForAuth;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForUpdate;

namespace Postline.Presentation.Controllers
{
    [Route("api/me")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UserController(IServiceManager service)
        {
            _service = service;
        }


        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UserForUpdateDto userForUpdate)
        {
            var result = await _service.UserService.UpdateUserAsync(User.Identity.Name,userForUpdate, true);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new UserForUpdateResponse { Errors = errors, IsSuccessfulUpdate = false});
            }

             return NoContent();
        
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = await _service.UserService.GetUser(User.Identity.Name, false);
            return Ok(user);
        }
    }
}