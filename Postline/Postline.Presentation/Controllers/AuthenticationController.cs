using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postline.Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ForAuth;

namespace Postline.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service)
        {
            _service = service;
           
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok(new
            {
                message = "User successfully created."
            });
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized();
            return Ok(new
            {
                accessToken = await _service
                    .AuthenticationService.CreateToken()
            });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetAuthUser()
        {

            string id = GetIdFromToken();
            var result = await _service.AuthenticationService.GetAuthUser(id);

            return Ok(result);
        }

        private string GetIdFromToken()
        {
            var identity = User.Identity as ClaimsIdentity;
            string id = "";
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                id = claims.FirstOrDefault(p => p.Type == "id")?.Value;
            }

            return id;
        }

        [HttpPost("checkEmail")]
        public async Task<IActionResult> CheckEmail([FromBody] CheckEmail email)
        {
            var result = await _service.AuthenticationService.ValidateEmail(email.Email);

            return Ok(result);
        }  
        
        [HttpPost("checkUserName")]
        public async Task<IActionResult> CheckUserName([FromBody] CheckUserName userName)
        {
            var result = await _service.AuthenticationService.ValidateUserName(userName.UserName);

            return Ok(result);
        }   
    }
}