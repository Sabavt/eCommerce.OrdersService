using eCommerce.Core.Domain.Entities;
using MongoDB.Driver;

namespace eCommerce.Core.Domain.RepositoryContracts;

/// <summary>
/// Orders repository contract for accessing order data from the database.
/// </summary>
public interface IOrdersRepository
{
    /// <summary>
    /// Gets all orders from the database asynchronously.
    /// </summary>
    /// <returns>Returns collection of retrieved orders from data set.</returns>
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    /// <summary>
    /// Gets orders from the database based on a specified filter condition asynchronously.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>Returning matching orders from data set.</returns>
    Task<IEnumerable<Order>?> GetOrdersByConditionAsync(FilterDefinition<Order> filter);
    /// <summary>
    /// Gets a single order from the database based on a specified filter condition asynchronously.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>Returning matching order from data set.</returns>
    Task<Order?> GetOrderByConditionAsync(FilterDefinition<Order> filter);
    /// <summary>
    /// Creates a new order in the database asynchronously.
    /// </summary>
    /// <param name="order"></param>
    /// <returns>Returns added Order object or null if unsuccessful.</returns>
    Task<Order> CreateOrderAsync(Order order);
    /// <summary>
    /// Updates an existing order in the database asynchronously.
    /// </summary>
    /// <param name="order"></param>
    /// <returns>Returns updated Order object or null if not found.</returns>
    Task<Order> UpdateOrderAsync(Order order);
    /// <summary>
    /// Deletes an order from the database based on the specified order ID asynchronously.
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    Task<bool> DeleteOrderAsync(Guid orderID);
} 