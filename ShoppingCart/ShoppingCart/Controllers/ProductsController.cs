using Microsoft.AspNetCore.Mvc;
using SchoolOf.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        public async Task<IActionResult> GetProducts()
        {
            var myListOfProducts = new List<ProductDto>();
            myListOfProducts.Add(new ProductDto
            {
                Category = "test category",
                Description = "test description",
                Id = 10,
                Image = "no image yet",
                Name = "test product",
                Price = 100m

            });
            return Ok(myListOfProducts);
        }
    }
}
