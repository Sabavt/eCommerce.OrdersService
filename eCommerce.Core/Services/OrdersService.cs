using eCommerce.Core.Domain.Entities;
using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using MongoDB.Driver;

namespace eCommerce.Core.Services;

public class OrdersService : IOrdersService
{
    public Task<OrderResponse?> CreateOrderAsync(OrderAddRequest orderRequest, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrderAsync(Guid orderID, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponse?> GetOrderByConditionAsync(FilterDefinition<Order> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderResponse?>> GetOrdersAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderResponse?>> GetOrdersByConditionAsync(FilterDefinition<Order> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponse?> UpdateOrderAsync(OrderUpdateRequest orderRequest, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
