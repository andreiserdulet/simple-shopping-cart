using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Common.Exceptions;
using Data.Abstraction;
using Data.Models;
using SchoolOf.Dtos;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOf.ShoppingCart.Controllers
{
    namespace SchoolOfDotNet.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class OrderController : ControllerBase
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IValidator<CreateOrderDto> _createOrderValidator;
            private readonly IMapper _mapper;

            public OrderController(IUnitOfWork unitOfWork, IValidator<CreateOrderDto> validator, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _createOrderValidator = validator;
                _mapper = mapper;
            }

            [HttpPost]
            [Route("")]
            public async Task<IActionResult> Create([FromBody] CreateOrderDto orderDto)
            {
                var validationResult = await _createOrderValidator.ValidateAsync(orderDto);
                if (!validationResult.IsValid)
                {
                    throw new InternalValidationException(validationResult.Errors.Select(validationError => validationError.ErrorMessage).ToList());
                }

                var cartRepo = _unitOfWork.GetRepository<Cart>();
                var orderRepo = _unitOfWork.GetRepository<Order>();

                var cart = cartRepo.Find(x => x.Id == orderDto.CartId, nameof(Cart.Products))
                                                            .FirstOrDefault();
                if (cart == null)
                {
                    throw new InternalValidationException("Invalid 'Cart id'");
                }

                var order = _mapper.Map<Order>(orderDto);
                order.Total = cart.Products.Sum(x => x.Price);
                order.Cart = cart;
                cart.Status = Common.Enums.CartStatus.Completed;

                cartRepo.Update(cart);
                orderRepo.Add(order);

                await _unitOfWork.SaveChangesAsync();

                return Ok(_mapper.Map<OrderDto>(order));
            }
        }
    }
}