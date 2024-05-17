using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.Domain.Products
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}