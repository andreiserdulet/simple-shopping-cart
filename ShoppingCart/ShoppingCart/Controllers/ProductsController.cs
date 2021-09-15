using AutoMapper;
using Common.Exceptions;
using Data.Abstraction;
using Data.Models;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        public async Task<IActionResult> GetProducts()
        {
            throw new System.Exception();

            var myListOfProducts = new List<ProductDto>();
            var productsFromDb = this._unitOfWork.GetRepository<Product>().Find(product => !product.IsDeleted);

            foreach (var p in productsFromDb)
            {
                myListOfProducts.Add(new ProductDto
                {
                    Category = p.Category,
                    Description = p.Description,
                    Id = p.Id,
                    Image = p.Image,
                    Name = p.Name,
                    Price = p.Price
                });
            }

            return Ok(myListOfProducts);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        public async Task<IActionResult> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                throw new InvalidParameterException("Invalid pageNumber argument.");
            }

            if (pageSize < 1)
            {
                throw new InvalidParameterException("Invalid pageSize argument.");
            }
            var productsFromDb = this._unitOfWork
                .GetRepository<Product>()
                .Find(product => !product.IsDeleted, pageNumber, pageSize);
            var myListOfProducts = _mapper.Map<List<ProductDto>>(productsFromDb);
        
            return Ok(myListOfProducts);
        }
    }
}