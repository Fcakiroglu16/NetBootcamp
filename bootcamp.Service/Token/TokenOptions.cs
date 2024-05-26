using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootcamp.Service.Token
{
    public record CustomTokenOptions
    {
        public string Signature { get; set; } = default!;
        public int ExpireByHour { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string[] Audience { get; set; } = default!;

        public int RefreshTokenExpireByDay { get; set; } = default!;
    }
}