using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Repository.Tokens
{
    public class RefreshToken : BaseEntity<int>
    {
        public Guid Code { get; set; }

        public DateTime Expire { get; set; }

        public Guid UserId { get; set; }
    }
}