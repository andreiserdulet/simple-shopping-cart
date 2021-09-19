using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOf.Dtos
{
   public class CartDto
    {
        public long id { get; set; }
        public string Status { get; set; }
        public ICollection<ProductDto> Products { get; set; }
         
    }
}
