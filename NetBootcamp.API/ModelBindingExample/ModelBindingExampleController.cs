using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Products;

namespace NetBootcamp.API.ModelBindingExample
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelBindingExampleController : ControllerBase
    {
        // [FromBody] = complex type => default
        // [FromHeader]
        // [FromQuery] => simple type => default
        // [FromRoute]
        //  localhost/api/ModelBindingExample?user.Id=5&user.Name=Test&user.Email="

        //localhost/api/ModelBindingExample/name/ahmet/email/ahmet@outlook.com
        //localhost/api/ModelBindingExample/ahmet/ahmet@outlook.com
        [HttpPost("name/{name}/email/{email}")]
        public IActionResult SaveProduct(string name, string email)
        {
            return Ok(new { Name = name, Email = email });
        }


        //[HttpPost]
        //public IActionResult SaveProduct([FromBody] string name)
        //{
        //    return Ok(new { Name = name });
        //}


        //[HttpGet]
        //public IActionResult SaveProduct([FromHeader] string name, [FromHeader] string email)
        //{
        //    return Ok(new { Name = name, Email = email });
        //}


        //[HttpPost]
        //public IActionResult SaveProduct([FromHeader] UserDto user)
        //{
        //    return Ok(user);
        //}

        //[HttpPost]
        //public IActionResult SaveProduct([FromQuery] UserDto user)
        //{
        //    return Ok(user);
        //}
    }
}