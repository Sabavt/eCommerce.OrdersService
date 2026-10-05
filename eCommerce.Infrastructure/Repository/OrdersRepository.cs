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
        order.Id = order.OrderID.ToString();
        await _ordersCollection.InsertOneAsync(order);
        return order;
    }

    public async Task<bool> DeleteOrderAsync(Guid orderID)
    {
        var filter = Builders<Order>.Filter.Eq(o => o.OrderID, orderID);
        var order = await _ordersCollection.DeleteManyAsync(filter);
        
        return order.DeletedCount > 0;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _ordersCollection.FindAsync(_ => true).Result.ToListAsync();
    }

    public async Task<Order?> GetOrderByConditionAsync(FilterDefinition<Order> filter)
    {
        return await _ordersCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>?> GetOrdersByConditionAsync(FilterDefinition<Order> filter)
    {
        return await _ordersCollection.FindAsync(filter).Result.ToListAsync();
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        await _ordersCollection.ReplaceOneAsync(o => o.OrderID == order.OrderID, order);
        return order;
    }
} 