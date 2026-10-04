using AutoMapper;
using eCommerce.Core.Domain.Entities;
using eCommerce.Core.Domain.RepositoryContracts;
using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using MongoDB.Driver;

namespace eCommerce.Core.Services;

public class OrdersService : IOrdersService
{
    private readonly IMapper _mapper;
    private readonly IOrdersRepository _ordersRepository;

    public OrdersService(IMapper mapper, IOrdersRepository ordersRepository)
    {
        _mapper = mapper;
        _ordersRepository = ordersRepository;
    }

    public async Task<OrderResponse?> CreateOrderAsync(OrderAddRequest orderRequest, CancellationToken cancellationToken = default)
    {
        var order_to_add = _mapper.Map<Order>(orderRequest);
        var order_from_db = await _ordersRepository.CreateOrderAsync(order_to_add);
        var order_added = _mapper.Map<OrderResponse>(order_from_db);
        return order_added;
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
