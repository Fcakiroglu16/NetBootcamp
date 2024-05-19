using bootcamp.Service.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;

namespace NetBootcamp.API.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserService userService) : CustomBaseController
    {
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequestDto request)
        {
            return CreateActionResult(await userService.SignUp(request));
        }

        //[HttpGet]
        //public async Task<IActionResult> GetUsers()
        //{
        //    var response = await userService.GetUsers();
        //    return Ok(response);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUserById(int id)
        //{
        //    var response = await userService.GetUserById(id);
        //    return Ok(response);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateUser(int id, UpdateUserRequestDto request)
        //{
        //    var response = await userService.UpdateUser(id, request);
        //    return Ok(response);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(int id)
        //{
        //    var response = await userService.DeleteUser(id);
        //    return Ok(response);
        //}
    }
}