using eCommerce.Core.Domain.Entities;
using eCommerce.Core.DTO;
using MongoDB.Driver;

namespace eCommerce.Core.ServiceContracts;

/// <summary>
/// Interface for managing orders in the eCommerce system.
/// </summary>
public interface IOrdersService
{
    /// <summary>
    /// Synchronously retrieves a list of all orders.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<OrderResponse?>> GetOrdersAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Synchronously retrieves a order that match the specified filter condition.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<OrderResponse?> GetOrderByConditionAsync(FilterDefinition<Order> filter, CancellationToken cancellationToken = default);
    /// <summary>
    /// Synchronously retrieves a list of orders that match the specified filter condition.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<List<OrderResponse?>> GetOrdersByConditionAsync(FilterDefinition<Order> filter, CancellationToken cancellationToken = default);
    /// <summary>
    /// Creates a new order based on the provided order request.
    /// </summary>
    /// <param name="orderRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<OrderResponse?> CreateOrderAsync(OrderAddRequest orderRequest, CancellationToken cancellationToken = default);
    /// <summary>
    /// Updates an existing order based on the provided order request.
    /// </summary>
    /// <param name="orderRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<OrderResponse?> UpdateOrderAsync(OrderUpdateRequest orderRequest, CancellationToken cancellationToken = default);
    /// <summary>
    /// Deletes an order based on the specified order ID.
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> DeleteOrderAsync(Guid orderID, CancellationToken cancellationToken = default);
} 