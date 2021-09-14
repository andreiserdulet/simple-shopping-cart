﻿using Data.Abstraction;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using SchoolOf.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        public async Task<IActionResult> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var myListOfProducts = this._unitOfWork
                .GetRepository<Product>()
                .Find(product => !product.IsDeleted, pageNumber, pageSize)
                .Select(p => new ProductDto
                {
                    Category = p.Category,
                    Description = p.Description,
                    Id = p.Id,
                    Image = p.Image,
                    Name = p.Name,
                    Price = p.Price
                });

            return Ok(myListOfProducts);
        }
    }
}