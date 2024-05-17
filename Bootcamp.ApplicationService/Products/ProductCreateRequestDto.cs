using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.ApplicationService.Products
{
    public record ProductCreateRequestDto(string Name, decimal Price);
}