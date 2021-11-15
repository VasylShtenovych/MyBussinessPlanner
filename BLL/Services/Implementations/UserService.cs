using BLL.Dto;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto> RegisterUserAsync(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto == null || (userRegistrationDto.Password != userRegistrationDto.ConfirmPassword))
                return null;

            var identityUser = new IdentityUser
            {
                Email = userRegistrationDto.Email,
                UserName = userRegistrationDto.Email
            };

            var result = await _userManager.CreateAsync(identityUser, userRegistrationDto.Password);

            return result.Succeeded ? new UserDto() { Email = userRegistrationDto.Email } : null;
        }
    }
}
