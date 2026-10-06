using AutoMapper;
using eCommerce.Core.Domain.Entities;
using eCommerce.Core.Domain.RepositoryContracts;
using eCommerce.Core.DTO;
using eCommerce.Core.HttpClients;
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
    private readonly UsersMicroserviceHttpClient _usersMicroserviceHttpClient;
    private readonly ProductsMicroserviceHttpClient _productsMicroserviceHttpClient;

    public OrdersService(IMapper mapper, IOrdersRepository ordersRepository, IValidator<OrderAddRequest> orderAddRequestValidator, IValidator<OrderUpdateRequest> orderUpdateRequestValidator, IValidator<OrderItemAddRequest> orderItemAddRequestValidator, IValidator<OrderItemUpdateRequest> orderItemUpdateRequestValidator, UsersMicroserviceHttpClient usersMicroserviceHttpClient, ProductsMicroserviceHttpClient productsMicroserviceHttpClient)
    {
        _orderAddRequestValidator = orderAddRequestValidator;
        _orderItemAddRequestValidator = orderItemAddRequestValidator;
        _orderUpdateRequestValidator = orderUpdateRequestValidator;
        _orderItemUpdateRequestValidator = orderItemUpdateRequestValidator;
        _usersMicroserviceHttpClient = usersMicroserviceHttpClient;
        _productsMicroserviceHttpClient = productsMicroserviceHttpClient;
        _mapper = mapper;
        _ordersRepository = ordersRepository;
    }

    public async Task<OrderResponse?> CreateOrderAsync(OrderAddRequest? orderRequest, CancellationToken cancellationToken = default)
    {
        if(orderRequest == null)
        {
            throw new ArgumentNullException(nameof(orderRequest));
        }

        if (!await _usersMicroserviceHttpClient.IsUserExistsAsync(orderRequest.UserID))
        {
            throw new ArgumentException($"User with ID {orderRequest.UserID} does not exist.");
        }

        if (!await _productsMicroserviceHttpClient.IsProductExistsAsync(orderRequest.Items[0].ProductID))
        {
            throw new ArgumentException($"Product with ID {orderRequest.Items[0].ProductID} does not exist.");
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

    public async Task<bool> DeleteOrderAsync(Guid orderID, CancellationToken cancellationToken = default)
    {
        return await _ordersRepository.DeleteOrderAsync(orderID);
    }

    public async Task<OrderResponse?> GetOrderByConditionAsync(FilterDefinition<Order> filter, CancellationToken cancellationToken = default)
    {
        var order = await _ordersRepository.GetOrderByConditionAsync(filter);
        return order != null ? _mapper.Map<OrderResponse>(order) : null;
    }

    public async Task<List<OrderResponse?>> GetOrdersAsync(CancellationToken cancellationToken = default)
    {
        var orders = await _ordersRepository.GetAllOrdersAsync();
        return orders.Select(_mapper.Map<OrderResponse?>).ToList();
    } 

    public async Task<List<OrderResponse?>> GetOrdersByConditionAsync(FilterDefinition<Order> filter, CancellationToken cancellationToken = default)
    {
        var orders = await _ordersRepository.GetOrdersByConditionAsync(filter);
        if(orders == null)
        {
            return new List<OrderResponse?>();
        }
        return orders.Select(_mapper.Map<OrderResponse?>).ToList();
    }

    public async Task<OrderResponse?> UpdateOrderAsync(OrderUpdateRequest? orderRequest, CancellationToken cancellationToken = default)
    {
        if(orderRequest == null)
        {
            throw new ArgumentNullException(nameof(orderRequest));
        }

        if(!await _usersMicroserviceHttpClient.IsUserExistsAsync(orderRequest.UserID))
        {
            throw new ArgumentException($"User with ID {orderRequest.UserID} does not exist.");
        }

        if (!await _productsMicroserviceHttpClient.IsProductExistsAsync(orderRequest.Items[index: 0].ProductID))
        {
            throw new ArgumentException($"Product with ID {orderRequest.Items[0].ProductID} does not exist.");
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