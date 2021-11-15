using BLL.Dto;
using BLL.Services.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private IConfiguration _configuration;

        public UserService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserDto> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto == null || (userRegistrationDto.Password != userRegistrationDto.ConfirmPassword))
                return null;

            var identityUser = new User
            {
                Email = userRegistrationDto.Email,
                UserName = userRegistrationDto.Email
            };

            var result = await _userManager.CreateAsync(identityUser, userRegistrationDto.Password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userRegistrationDto.Email);
                return new UserDto() { Email = userRegistrationDto.Email, Token = GetUserToken(userRegistrationDto.Email, user.Id) };
            }

            return null;
        }
        public async Task<UserDto> LoginUserAsync(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);

            if (user == null)
                return null;

            var result = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

            if (!result)
                return null;

            return new UserDto() { Email = userLoginDto.Email, Token = GetUserToken(userLoginDto.Email, user.Id) };
        }

        private string GetUserToken(string email, string id)
        {
            var claims = new[]
            {
                new Claim("Email", email),
                new Claim(ClaimTypes.NameIdentifier, id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims, 
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256), 
                expires: DateTime.Now.AddDays(30));

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenAsString;
        }
    }
}
