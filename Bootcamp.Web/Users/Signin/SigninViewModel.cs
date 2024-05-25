using System.ComponentModel.DataAnnotations;

namespace Bootcamp.Web.Signin
{
    public record SigninViewModel([Required] string Email, [Required] string Password, bool RememberMe);
}