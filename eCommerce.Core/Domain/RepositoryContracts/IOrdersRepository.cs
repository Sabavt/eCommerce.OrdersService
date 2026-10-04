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
    /// <returns>Returns collection of retrieved orders from data set</returns>
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    /// <summary>
    /// Gets orders from the database based on a specified filter condition asynchronously.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<IEnumerable<Order>?> GetOrdersByConditionAsync(FilterDefinition<Order> filter);
    /// <summary>
    /// Gets a single order from the database based on a specified filter condition asynchronously.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<Order?> GetOrderByConditionAsync(FilterDefinition<Order> filter);

} 