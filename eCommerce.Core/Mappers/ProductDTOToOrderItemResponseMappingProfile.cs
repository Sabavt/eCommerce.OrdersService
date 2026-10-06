using AutoMapper;
using eCommerce.Core.DTO;

namespace eCommerce.Core.Mappers;

public class ProductDTOToOrderItemResponseMappingProfile : Profile
{
    public ProductDTOToOrderItemResponseMappingProfile()
    {
        CreateMap<ProductDTO, OrderItemResponse>();
    }
} 