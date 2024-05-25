using Bootcamp.Web.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Bootcamp.Web.TokenServices
{
    public class TokenService(HttpClient client, IOptions<TokenOption> tokenOptions, IMemoryCache memoryCache)

    {
        private const string TokenKey = "client_credential:access_token";


        public async Task<(bool isSuccess, string? token, List<string>? error)> GetTokenWithClientCredentials()
        {
            //Tuple<bool,string,string>
            //  token,true,null
            //  null,false,error


            if (memoryCache.TryGetValue(TokenKey, out string? token))
            {
                return (true, token!, null);
            }


            var requestAsBody =
                new ClientCredentialTokenRequestDto(tokenOptions.Value.ClientId, tokenOptions.Value.ClientSecret);

            var response =
                await client.PostAsJsonAsync("/api/Token/CreateClientCredential",
                    requestAsBody);


            var responseAsBody =
                await response.Content.ReadFromJsonAsync<ResponseModelDto<ClientCredentialTokenResponseDto>>();


            if (!response.IsSuccessStatusCode)
            {
                return (false, null, responseAsBody!.FailMessages);
            }


            memoryCache.Set(TokenKey, responseAsBody!.Data!.AccessToken, TimeSpan.FromHours(9));

            return (true, responseAsBody.Data.AccessToken, null);
        }


        public void ClearTokenCache()
        {
            memoryCache.Remove(TokenKey);
        }
    }
}