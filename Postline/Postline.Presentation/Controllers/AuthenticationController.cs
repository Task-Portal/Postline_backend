using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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

        [HttpPost("registration")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
            
            
            if (!result.Succeeded) 
            { 
                var errors = result.Errors.Select(e => e.Description); 
                
                return BadRequest(new RegistrationResponseDto { Errors = errors }); 
            }
            return StatusCode(201); 
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]  
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
           
            
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized();
            // return Ok(new
            // {
            //     accessToken = await _service
            //         .AuthenticationService.CreateToken()
            // });
            var token = await _service
                .AuthenticationService.CreateToken();
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token =token});
        }
        
        [HttpGet("privacy")]
        // [Authorize]
        [Authorize(Roles = "Manager")]
        public IActionResult Privacy()
        {
            var claims = User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();
            return Ok(claims);
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
        
        // [HttpPost("sendEmail")]
        // public async Task<IActionResult> SendEmail()
        // {
        //     var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
        //     var message = new Message(new string[] { "therewego123xy123@gmail.com" }, "Test mail with Attachments", "This is the content from our mail with attachments.", files);
        //     await _emailSender.SendEmailAsync(message);
        //     return Ok();
        // }   
        
        [HttpPost("forgotPassword")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]  
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var response =await  _service.AuthenticationService.SendRestoreLinkToEmail(forgotPasswordDto);
            if (!response)
                return BadRequest("Invalid Request");
            
            
            return Ok();
        }
    }
}