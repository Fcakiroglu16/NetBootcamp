using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bootcamp.Repository.Identities;
using bootcamp.Service.SharedDTOs;
using bootcamp.Service.Token;
using Microsoft.AspNetCore.Identity;

namespace bootcamp.Service.Users
{
    public class UserService(UserManager<AppUser> userManager)
    {
        // signup
        public async Task<ResponseModelDto<Guid>> SignUp(SignUpRequestDto request)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Lastname
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return ResponseModelDto<Guid>.Fail(result.Errors.Select(x => x.Description).ToList());
            }


            return ResponseModelDto<Guid>.Success(user.Id, HttpStatusCode.Created);
        }


        // signin
        public async Task<ResponseModelDto<TokenResponseDto>> SignIn(SignInRequestDto request)

        {
            // Fast fail
            // Guard clauses
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return ResponseModelDto<TokenResponseDto>.Fail("Email or Password is wrong", HttpStatusCode.NotFound);
            }


            var result = await userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return ResponseModelDto<TokenResponseDto>.Fail("Email or Password is wrong", HttpStatusCode.BadRequest);
            }
        }
    }
}