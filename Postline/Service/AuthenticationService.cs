﻿using System;
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
using EmailService;
using LoggerService;
using Microsoft.AspNetCore.WebUtilities;
using Shared.DataTransferObjects.ForAuth;
using Shared.DataTransferObjects.ForShow;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        #region Properties

        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private User _user;

        #endregion

        #region Ctor

        public AuthenticationService(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration, IEmailSender emailSender)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }
        

        #endregion
        
        #region Register User

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

//Disable Avast Antivirus
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await GenerateEmailConfirmationToken(userForRegistration, user);
              
            }


            return result;
        }

        #endregion


        #region Validate User

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
            if (!result)
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user email or password.");

            return result;
        }
        

        #endregion
        
        #region Is Email Confirmed

        public async Task<bool> IsEmailConfirmed(UserForAuthenticationDto userForAuthentication)
        {
            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            var response = await _userManager.IsEmailConfirmedAsync(user);
            return response;
        }

        #endregion

        #region Validate Email

        public async Task<bool> ValidateEmail(string email) => await _userManager.FindByEmailAsync(email) != null;
        

        #endregion

        #region Get Auth User

        public async Task<UserDto> GetAuthUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var mappedUser = _mapper.Map<UserDto>(user);
            mappedUser.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return mappedUser;
        }

        

        #endregion
        
        #region Create Token

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            _logger.LogInfo($"Creating Token (CreateToken)");
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


        private SigningCredentials GetSigningCredentials()
        {
            // var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var securityKey = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(securityKey.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.Email)
            };
            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
        

        #endregion

        #region Send Restore Link To Email

        public async Task<bool> SendRestoreLinkToEmail(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                return false;
            }


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                { "token", token },
                { "email", forgotPasswordDto.Email }
            };
            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientUrl, param);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);
            return true;
        }
        

        #endregion

        #region Reset Password

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            return await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        }

        #endregion

        
        public async Task<IdentityResult> EmailConfirmation(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
          var result=  await _userManager.ConfirmEmailAsync(user, token);

          return result;
        }
        
        
        #region Generate Email Confirmation Token

        private async Task GenerateEmailConfirmationToken(UserForRegistrationDto userForRegistration, User user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                { "token", token },
                { "email", user.Email }
            };
            var callback = QueryHelpers.AddQueryString(userForRegistration.ClientURL, param);
            var message = new Message(new string[] { user.Email }, "Email Confirmation token", callback, null);
            await _emailSender.SendEmailAsync(message);
          
        }
        

        #endregion
    }
}