using Data.Abstraction;
using System.Collections.Generic;

namespace Data.Models
{
    public class Product : BaseEntityModel
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
