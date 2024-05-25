using Bootcamp.Web.Models;
using Bootcamp.Web.Signin;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using Bootcamp.Web.Users.Signin;
using Microsoft.AspNetCore.DataProtection;

namespace Bootcamp.Web.Users
{
    public class UserService(
        HttpClient client,
        IHttpContextAccessor contextAccessor,
        IDataProtectionProvider iDataProtectionProvider)
    {
        public async Task<ServiceResponseModel<NoContent>> Signin(SigninViewModel signinViewModel)
        {
            var requestModel = new SigninRequestDto(signinViewModel.Email, signinViewModel.Password);


            var response = await client.PostAsJsonAsync("/api/Users/SignIn", requestModel);

            var responseBody = await response.Content.ReadFromJsonAsync<ResponseModelDto<SigninResponseDto>>();


            if (!response.IsSuccessStatusCode)
            {
                return ServiceResponseModel<NoContent>.Fail(responseBody!.FailMessages);
            }

            var accessToken = responseBody!.Data!.AccessToken;


            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.ReadJwtToken(accessToken);


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(jwtSecurityToken.Claims,
                CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


            var authenticationTokenList = new List<AuthenticationToken>();

            authenticationTokenList.Add(new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = accessToken
            });

            authenticationTokenList.Add(new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = "custom refresh token"
            });


            var authenticationProperties = new AuthenticationProperties();
            authenticationProperties.StoreTokens(authenticationTokenList);


            await contextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal, authenticationProperties);


            var dataProtector = iDataProtectionProvider.CreateProtector("abc");


            var readAsEncrypt = dataProtector.Protect("red");


            contextAccessor.HttpContext.Response.Cookies.Append("bgcolor", readAsEncrypt);

            return ServiceResponseModel<NoContent>.Success();
        }
    }
}