using eCommerce.Core.Domain.Entities;
using eCommerce.Core.Domain.RepositoryContracts;
using MongoDB.Driver;

namespace eCommerce.Infrastructure.Repository;

public class OrdersRepository : IOrdersRepository
{
    private readonly IMongoCollection<Order> _ordersCollection;
    public OrdersRepository(IMongoDatabase mongoDatabase)
    {
        _ordersCollection = mongoDatabase.GetCollection<Order>("Orders");
    }

    public Task<Order> CreateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteOrderAsync(Guid orderID)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetOrderByConditionAsync(FilterDefinition<Order> filter)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>?> GetOrdersByConditionAsync(FilterDefinition<Order> filter)
    {
        throw new NotImplementedException();
    }

    public Task<Order> UpdateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }
}
