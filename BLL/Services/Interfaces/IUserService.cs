using BLL.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<(UserDto userDto, IEnumerable<String> errors)> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<UserDto> LoginUserAsync(UserLoginDto userLoginDto);
    }
}
