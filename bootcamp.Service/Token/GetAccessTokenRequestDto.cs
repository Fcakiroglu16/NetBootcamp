using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootcamp.Service.Token
{
    public record GetAccessTokenRequestDto(string ClientId, string ClientSecret);
}