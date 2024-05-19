using Bootcamp.Repository.Identities;
using bootcamp.Service.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;

namespace NetBootcamp.API.Users
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserService userService) : CustomBaseController
    {
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequestDto request)
        {
            return CreateActionResult(await userService.SignUp(request));
        }


        //signin
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInRequestDto request)
        {
            return CreateActionResult(await userService.SignIn(request));
        }
    }
}