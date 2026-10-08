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

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync(CancellationToken cancellationToken)
        {
            var ordersResponse = await _ordersService.GetOrdersAsync(cancellationToken);
            return Ok(ordersResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(OrderAddRequest orderRequest, CancellationToken cancellationToken)
        {
            var orderResponse = await _ordersService.CreateOrderAsync(orderRequest, cancellationToken);
            return Ok(orderResponse);
        }

        [HttpDelete("{orderID}")]
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

        [HttpGet("{orderID}")]
        public async Task<IActionResult> GetOrderByIDAsync(Guid orderID, CancellationToken cancellationToken)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.OrderID, orderID);
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

        [HttpGet("product/{productID}")]
        public async Task<IActionResult> GetOrdersByProductIDAsync(int productID, CancellationToken cancellationToken)
        {
            var filter = Builders<Order>.Filter.ElemMatch(o => o.Items, item => item.ProductID == productID);
            var ordersResponse = await _ordersService.GetOrdersByConditionAsync(filter, cancellationToken);
            return Ok(ordersResponse);
        }

        [HttpGet("date/{orderDate}")]
        public async Task<IActionResult> GetOrdersByOrderDateAsync(DateTime orderDate, CancellationToken cancellationToken)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.OrderDate, orderDate);
            var ordersResponse = await _ordersService.GetOrdersByConditionAsync(filter, cancellationToken);
            return Ok(ordersResponse);
        }

        [HttpPut]
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