
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Common.Exceptions;
using Data.Abstraction;
using Data.Models;
using SchoolOf.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CartProductDto> _cartValidator;
        private readonly Cart _cart;

        public CartsController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CartProductDto> cartValidator, Cart cart)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartValidator = cartValidator;
            _cart = cart;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CartProductDto), 200)]
        public async Task<IActionResult> AddProductToCart([FromBody] CartProductDto cartProduct)
        {
            var validationResult = await _cartValidator.ValidateAsync(cartProduct);
            if (!validationResult.IsValid)
            {
                throw new InternalValidationException(validationResult.Errors.Select(validationError => validationError.ErrorMessage).ToList());
            }

            var cartRepo = _unitOfWork.GetRepository<Cart>();
            var productRepo = _unitOfWork.GetRepository<Product>();

            Cart cart = null;

            cart = cartRepo.Find(x => x.Id == cartProduct.CartId, nameof(Cart.Products)).FirstOrDefault();

            if (cart == null)
            {
                var product = await productRepo.GetByIdAsync(cartProduct.ProductId);

                cart = new Cart
                {
                    Status = Common.Enums.CartStatus.Created,
                    Products = new List<Product> { product }
                };

                cartRepo.Add(cart);
            }
            else
            {
                var product = await productRepo.GetByIdAsync(cartProduct.ProductId);

                cart.Products.Add(product);
                cartRepo.Update(cart);
            }

            await _unitOfWork.SaveChangesAsync();

            return Ok(_mapper.Map<CartDto>(cart));
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CartProductDto>), 200)]
        public async Task<IActionResult> GetCart()
        {
            var cartsFromDb = this._unitOfWork
            .GetRepository<Cart>()
            .Find(cartProduct => !cartProduct.IsDeleted);
            var myListOfCartProduct = _mapper.Map<List<CartDto>>(cartsFromDb);

            return Ok(myListOfCartProduct);
        }
        [HttpDelete]
        [Route("carts")]
        [ProducesResponseType(typeof(IEnumerable<Cart>), 200)]
        public async Task<IActionResult> DeleteCart()
        {
            
            var cartFromDb = this._unitOfWork.GetRepository<Cart>().DeleteByIdAsync(_cart.Id);
            return Ok(cartFromDb);
        }
    }
}