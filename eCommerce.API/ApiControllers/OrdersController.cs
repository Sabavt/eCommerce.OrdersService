using eCommerce.Core.Domain.Entities;
using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace eCommerce.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        public async Task<IActionResult> CreateOrderAsync(OrderAddRequest orderRequest, CancellationToken cancellationToken)
        {
            var orderResponse = await _ordersService.CreateOrderAsync(orderRequest, cancellationToken);
            return Ok(orderResponse);
        }

        public async Task<IActionResult> DeleteOrderAsync(Guid orderID, CancellationToken cancellationToken)
        {
            var result = await _ordersService.DeleteOrderAsync(orderID, cancellationToken);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> GetOrderByConditionAsync(FilterDefinition<Order> filter, CancellationToken cancellationToken)
        {
            var orderResponse = await _ordersService.GetOrderByConditionAsync(filter, cancellationToken);
            if (orderResponse != null)
            {
                return Ok(orderResponse);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> GetOrdersByConditionAsync(FilterDefinition<Order> filter, CancellationToken cancellationToken)
        {
            var ordersResponse = await _ordersService.GetOrdersByConditionAsync(filter, cancellationToken);
            return Ok(ordersResponse);
        }

        public async Task<IActionResult> UpdateOrderAsync(OrderUpdateRequest orderRequest, CancellationToken cancellationToken)
        {
            var orderResponse = await _ordersService.UpdateOrderAsync(orderRequest, cancellationToken);
            if (orderResponse != null)
            {
                return Ok(orderResponse);
            }
            else
            {
                return NotFound();
            }
        }  
    }
}