using AutoMapper;
using eCommerce.Core.Domain.Entities;
using eCommerce.Core.Domain.RepositoryContracts;
using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using FluentValidation;
using MongoDB.Driver;

namespace eCommerce.Core.Services;

public class OrdersService : IOrdersService
{
    private readonly IValidator<OrderAddRequest> _orderAddRequestValidator;
    private readonly IValidator<OrderUpdateRequest> _orderUpdateRequestValidator;
    private readonly IValidator<OrderItemAddRequest> _orderItemAddRequestValidator;
    private readonly IValidator<OrderItemUpdateRequest> _orderItemUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IOrdersRepository _ordersRepository;

    public OrdersService(IMapper mapper, IOrdersRepository ordersRepository, IValidator<OrderAddRequest> orderAddRequestValidator, IValidator<OrderUpdateRequest> orderUpdateRequestValidator, IValidator<OrderItemAddRequest> orderItemAddRequestValidator, IValidator<OrderItemUpdateRequest> orderItemUpdateRequestValidator)
    {
        _orderAddRequestValidator = orderAddRequestValidator;
        _orderItemAddRequestValidator = orderItemAddRequestValidator;
        _orderUpdateRequestValidator = orderUpdateRequestValidator;
        _orderItemUpdateRequestValidator = orderItemUpdateRequestValidator;
        _mapper = mapper;
        _ordersRepository = ordersRepository;
    }

    public async Task<OrderResponse?> CreateOrderAsync(OrderAddRequest? orderRequest, CancellationToken cancellationToken = default)
    {
        if(orderRequest == null)
        {
            throw new ArgumentNullException(nameof(orderRequest));
        }

        await _orderAddRequestValidator.ValidateAndThrowAsync(orderRequest);

        foreach(var item in orderRequest.Items)
        {
            await _orderItemAddRequestValidator.ValidateAndThrowAsync(item);
        }

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

    public async Task<OrderResponse?> UpdateOrderAsync(OrderUpdateRequest? orderRequest, CancellationToken cancellationToken = default)
    {
        if(orderRequest == null)
        {
            throw new ArgumentNullException(nameof(orderRequest));
        }

        await _orderUpdateRequestValidator.ValidateAndThrowAsync(orderRequest);

        foreach(var item in orderRequest.Items)
        {
            await _orderItemUpdateRequestValidator.ValidateAndThrowAsync(item);
        }

        var order_to_update = _mapper.Map<Order>(orderRequest);
        var order_from_db = await _ordersRepository.UpdateOrderAsync(order_to_update);
        var order_updated = _mapper.Map<OrderResponse>(order_from_db);
        return order_updated;
    }
} 