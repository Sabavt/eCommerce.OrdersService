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

    public async Task<Order> CreateOrderAsync(Order order)
    {
        order.OrderID = Guid.NewGuid();
        await _ordersCollection.InsertOneAsync(order);
        return order;
    }

    public async Task<bool> DeleteOrderAsync(Guid orderID)
    {
        var filter = Builders<Order>.Filter.Eq(o => o.OrderID, orderID);
        var order = await _ordersCollection.DeleteManyAsync(filter);
        if (order.DeletedCount > 0)
        {
            return true;
        }
        else
            return false;
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
