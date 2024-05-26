using bootcamp.Service.SharedDTOs;
using bootcamp.Service.Token;
using Bootcamp.Repository;
using Bootcamp.Repository.Identities;
using Bootcamp.Repository.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Channels;

namespace bootcamp.Service.Users
{
    public class UserService(
        IGenericRepository<RefreshToken> refreshTokenRepository,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IOptions<CustomTokenOptions> customTokenOptions,
        Channel<UserCreatedEvent> channel)
    {
        // signup
        public async Task<ResponseModelDto<Guid>> SignUp(SignUpRequestDto request)
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Lastname,
                BirthDate = request.BirthDate
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return ResponseModelDto<Guid>.Fail(result.Errors.Select(x => x.Description).ToList());
            }

            if (request.BirthDate.HasValue)
            {
                await userManager.AddClaimAsync(user,
                    new Claim(ClaimTypes.DateOfBirth, user.BirthDate!.Value.ToShortDateString()));
            }

            var userCreatedEvent = new UserCreatedEvent(user.Email);

            channel.Writer.TryWrite(userCreatedEvent);
            // UserCreatedEvent
            // Email => 2sn
            // Notify => 2sn
            // Discount => 3sn
            // X => 5 sn
            // hoş geldin emaili atılacak

            return ResponseModelDto<Guid>.Success(user.Id, HttpStatusCode.Created);
        }


        // signin
        public async Task<ResponseModelDto<TokenResponseDto>> SignIn(SignInRequestDto request)

        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return ResponseModelDto<TokenResponseDto>.Fail("Email or Password is wrong", HttpStatusCode.NotFound);
            }

            var result = await userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return ResponseModelDto<TokenResponseDto>.Fail("Email or Password is wrong");
            }

            var userClaims = await CreateUserClaims(user, customTokenOptions.Value);
            var accessToken = CreateAccessToken(userClaims, customTokenOptions.Value);
            var refreshToken = await CreateOrUpdateRefreshToken(user.Id, customTokenOptions.Value);

            return ResponseModelDto<TokenResponseDto>.Success(new TokenResponseDto(accessToken,
                refreshToken));
        }


        public async Task<ResponseModelDto<TokenResponseDto>> SignInByRefreshToken(
            SigninByRefreshTokenRequestDto request)
        {
            var hasRefreshToken =
                refreshTokenRepository.Where(x => x.Code == Guid.Parse(request.Code)).SingleOrDefault();


            if (hasRefreshToken is null)
            {
                return
                    ResponseModelDto<TokenResponseDto>.Fail("Refresh token not found");
            }

            // 7 26

            if (hasRefreshToken.Expire < DateTime.Now)
            {
                return ResponseModelDto<TokenResponseDto>.Fail("Refresh token expired");
            }

            var user = await userManager.FindByIdAsync(hasRefreshToken.UserId.ToString());


            if (user is null)
            {
                return ResponseModelDto<TokenResponseDto>.Fail("User not found");
            }


            var userClaims = await CreateUserClaims(user, customTokenOptions.Value);
            var accessToken = CreateAccessToken(userClaims, customTokenOptions.Value);
            var refreshToken = await CreateOrUpdateRefreshToken(user.Id, customTokenOptions.Value);

            return ResponseModelDto<TokenResponseDto>.Success(new TokenResponseDto(accessToken,
                refreshToken));
        }


        private async Task<string> CreateOrUpdateRefreshToken(Guid userId, CustomTokenOptions tokenOptions)
        {
            var hasRefreshToken = await refreshTokenRepository.Where(x => x.UserId == userId).SingleOrDefaultAsync();


            if (hasRefreshToken is null)
            {
                hasRefreshToken = new RefreshToken()
                {
                    Code = Guid.NewGuid(),
                    Expire = DateTime.Now.AddDays(tokenOptions.RefreshTokenExpireByDay),
                    UserId = userId
                };

                await refreshTokenRepository.Create(hasRefreshToken);
            }
            else
            {
                hasRefreshToken.Code = Guid.NewGuid();
                hasRefreshToken.Expire = DateTime.Now.AddDays(tokenOptions.RefreshTokenExpireByDay);

                await refreshTokenRepository.Update(hasRefreshToken);
            }

            await unitOfWork.CommitAsync();


            return hasRefreshToken.Code.ToString();
        }

        private string CreateAccessToken(List<Claim> claimList, CustomTokenOptions tokenOptions)
        {
            var tokenExpire = DateTime.Now.AddHours(tokenOptions.ExpireByHour);


            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Signature));


            //DateTimeOffset.Now.ToUnixTimeSeconds()
            var jwtToken = new JwtSecurityToken(
                claims: claimList,
                expires: tokenExpire,
                issuer: tokenOptions.Issuer,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));


            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(jwtToken);
        }

        private async Task<List<Claim>> CreateUserClaims(AppUser user, CustomTokenOptions tokenOptions)
        {
            var userClaimList = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!)
            };


            tokenOptions.Audience.ToList()
                .ForEach(x => { userClaimList.Add(new Claim(JwtRegisteredClaimNames.Aud, x)); });

            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                userClaimList.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var userClaims = await userManager.GetClaimsAsync(user);


            foreach (var userClaim in userClaims)
            {
                userClaimList.Add(new Claim(userClaim.Type, userClaim.Value));
            }


            foreach (var roleName in userRoles)
            {
                var role = await roleManager.FindByNameAsync(roleName);

                if (role is null)
                {
                    continue;
                }


                var roleClaim = await roleManager.GetClaimsAsync(role);

                foreach (var roleAsClaim in roleClaim)
                {
                    userClaimList.Add(roleAsClaim);
                }
            }

            return userClaimList;
        }
    }
}