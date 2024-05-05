using Microsoft.AspNetCore.Mvc;

namespace NetBootcamp.API.ModelBindingExample
{
    public record UserDto([FromQuery] int Id, [FromHeader] string Name, [FromHeader] string Email);
}