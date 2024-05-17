using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Domain.Products
{
    public record ProductDto(int Id, string Name, decimal Price);
}