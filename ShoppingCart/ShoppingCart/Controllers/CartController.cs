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

namespace SchoolOf.ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CartProductDto> _cartValidator;

        public CartsController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CartProductDto> cartValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartValidator = cartValidator;
        }

        [HttpDelete]
        [ProducesResponseType(typeof(CartDto), 200)]
        public async Task<IActionResult> DeleteProductToCartAsync(CartProductDto productToCartDto)
        {
            var validationResult = await _cartValidator.ValidateAsync(productToCartDto);
            if (!validationResult.IsValid)
            {
                throw new InternalValidationException(validationResult.Errors.Select(validationError => validationError.ErrorMessage).ToList());
            }

            var cartRepo = this._unitOfWork.GetRepository<Cart>();
            var cartEntity = cartRepo
                    .Find(x => !x.IsDeleted && x.Id == productToCartDto.CartId, nameof(Cart.Products))
                    .FirstOrDefault();

            cartEntity.Products.Remove(cartEntity.Products.FirstOrDefault(x => x.Id == productToCartDto.ProductId));

            await this._unitOfWork.SaveChangesAsync();

            return Ok(this._mapper.Map<CartDto>(cartEntity));
        }

        [HttpGet]
        [Route("{cartId}")]
        [ProducesResponseType(typeof(CartDto), 200)]
        public async Task<IActionResult> GetAsync(long cartId)
        {
            var cart = this._unitOfWork.GetRepository<Cart>()
                    .Find(x => !x.IsDeleted && x.Status == Common.Enums.CartStatus.Created && x.Id == cartId, nameof(Cart.Products))
                    .FirstOrDefault();

            if (cart is null)
            {
                throw new InternalValidationException("Invalid 'Cart id'");
            }

            return Ok(this._mapper.Map<CartDto>(cart));
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
    }
}
