using Common.Enums;
using Data.Abstraction;
using System.Collections.Generic;

namespace Data.Models
{

    public class Cart : BaseEntityModel
    {
        public CartStatus Status { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
    }
}
