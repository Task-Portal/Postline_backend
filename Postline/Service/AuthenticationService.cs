using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Shared.DataTransferObjects.ForAuth;
using Shared.DataTransferObjects.ForShow;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        private User _user;

        public AuthenticationService(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            userForRegistration.Role = "User";
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user,   new List<string>{userForRegistration.Role} );
            }
           

            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
            if (!result)
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user email or password.");

            _logger.LogInfo($"Found user {_user} Validate user");
            return result;
        }

        public async Task<bool> ValidateEmail(string email) => await _userManager.FindByEmailAsync(email) != null;
        
        public async Task<bool> ValidateUserName(string userName)=> await _userManager.FindByNameAsync(userName) != null;
        

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            _logger.LogInfo($"Creating Token (CreateToken)");
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<UserDto> GetAuthUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var mappedUser = _mapper.Map<UserDto>(user);
            mappedUser.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return mappedUser;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim("id", _user.Id),
                 
            };

            var roles = await _userManager.GetRolesAsync(_user);
            
            foreach (var role in roles)
            {
                //Todo role change it if roles are many
                  claims.Add(new Claim("Role", role)); // for me on the frontend
                    claims.Add(new Claim(ClaimTypes.Role, role)); //for identity to authorizate
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    }
}