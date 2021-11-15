using BLL.Dto;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]UserRegistrationDto userRegistrationDto)
        {
            var result = await _userService.RegisterUserAsync(userRegistrationDto);
            return result != null ? Ok(result) : BadRequest();
        }
    }
}
