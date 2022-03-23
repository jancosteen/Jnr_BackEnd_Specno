using Reddit_Api.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Reddit_Api.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController: ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly UserManager<User> _userManager;

        public AuthenticationController(IServiceManager service, UserManager<User> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _service.AuthenticationService.ValiateUser(user))
                return Unauthorized();

            var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);

            return Ok(tokenDto);
        }

        [HttpGet("userName/{userName}")]
        public async Task<IActionResult> GetAllUsers(string userName)
        {
            var users = await _userManager.FindByNameAsync(userName);
            return Ok(users);
        }
    }
}
