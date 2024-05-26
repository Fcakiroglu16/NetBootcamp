using bootcamp.Service.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;

namespace NetBootcamp.API.Token
{
    [Authorize]
    public class TokenController(ITokenService tokenService) : CustomBaseController
    {
        [AllowAnonymous]
        [HttpPost("CreateClientCredential")]
        public async Task<IActionResult> CreateClientToken(GetAccessTokenRequestDto request)
        {
            var response = await tokenService.CreateClientAccessToken(request);
            return CreateActionResult(response);
        }

        [HttpPost("RevokeRefreshToken/{code}")]
        public async Task<IActionResult> RevokeRefreshToken(Guid code)
        {
            var response = await tokenService.RevokeRefreshToken(code);
            return CreateActionResult(response);
        }
    }
}