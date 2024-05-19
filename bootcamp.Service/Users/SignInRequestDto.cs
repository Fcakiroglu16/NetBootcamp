using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootcamp.Service.Users
{
    public record SignInRequestDto(string Email, string Password);
}