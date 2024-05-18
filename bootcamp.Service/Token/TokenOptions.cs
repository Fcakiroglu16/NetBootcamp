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
    }
}