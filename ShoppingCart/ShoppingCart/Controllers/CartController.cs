using AutoMapper;
using Common.Exceptions;
using Data.Abstraction;
using Data.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SchoolOf.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly _cartValidator;
        public CartController(IUnitOfWork unitOfWork, IMapper mapper, AbstractValidator<CartProductDto> cartValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartValidator = cartValidator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(CartProductDto), 200)]
        public async Task<IActionResult> AddProductToCart([FromBody] CartProductDto cartProduct)
        {
            var validationResult = await _cartValidator.ValidateAsync(cartProduct);
            var cartRepo = _unitOfWork.GetRepository<Cart>();
            var productRepo = _unitOfWork.GetRepository<Product>();
            Cart cart = null;
            if(cartProduct.CartId > 0)
            {
                 cart = await cartRepo.GetByIdAsync(cartProduct.CartId);
                if(cart != null && !cart.IsDeleted)
                {
                    var product = await productRepo.GetByIdAsync(cartProduct.ProductId);
                    if(product == null || product.IsDeleted)
                    {
                        throw new InvalidParameterException("Invalid product Id.");
                    }
                    cart.Products.Add(product);
                    cartRepo.Update(cart);
                }
                else
                {
                    throw new InvalidParameterException("Invalid cart Id.");
                }
                
            }
            else
            {
                var product = await productRepo.GetByIdAsync(cartProduct.ProductId);
                if (product == null || product.IsDeleted)
                {
                    throw new InvalidParameterException("Invalid product Id.");
                }
               
            cart = new Cart
                {
                    Status = Common.Enums.CartStatus.Created,
                    Products = new List<Product> { product }
                };
                cartRepo.Add(cart);
                
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(_mapper.Map<CartDto>(cart));
        }
    }
}
