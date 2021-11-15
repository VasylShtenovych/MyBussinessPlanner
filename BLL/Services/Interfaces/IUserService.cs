using BLL.Dto;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
    }
}
