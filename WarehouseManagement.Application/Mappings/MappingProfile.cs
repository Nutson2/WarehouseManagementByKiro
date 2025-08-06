using AutoMapper;
using WarehouseManagement.Application.DTOs;
using WarehouseManagement.Domain.Entities;

namespace WarehouseManagement.Application.Mappings;

/// <summary>
/// Профиль маппинга для AutoMapper
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Resource mappings
        CreateMap<Resource, ResourceDto>();
        CreateMap<CreateResourceDto, Resource>();
        CreateMap<UpdateResourceDto, Resource>();

        // UnitOfMeasure mappings
        CreateMap<UnitOfMeasure, UnitOfMeasureDto>();
        CreateMap<CreateUnitOfMeasureDto, UnitOfMeasure>();
        CreateMap<UpdateUnitOfMeasureDto, UnitOfMeasure>();

        // Client mappings
        CreateMap<Client, ClientDto>();
        CreateMap<CreateClientDto, Client>();
        CreateMap<UpdateClientDto, Client>();

        // ReceiptDocument mappings
        CreateMap<ReceiptDocument, ReceiptDocumentDto>();
        CreateMap<ReceiptResource, ReceiptResourceDto>()
            .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource.Name))
            .ForMember(dest => dest.UnitOfMeasureName, opt => opt.MapFrom(src => src.UnitOfMeasure.Name));

        // ShipmentDocument mappings
        CreateMap<ShipmentDocument, ShipmentDocumentDto>();
        CreateMap<ShipmentResource, ShipmentResourceDto>();

        // Balance mappings
        CreateMap<Balance, BalanceDto>()
            .ForMember(dest => dest.ResourceName, opt => opt.MapFrom(src => src.Resource.Name))
            .ForMember(dest => dest.ResourceStatus, opt => opt.MapFrom(src => src.Resource.Status))
            .ForMember(dest => dest.UnitOfMeasureName, opt => opt.MapFrom(src => src.UnitOfMeasure.Name))
            .ForMember(dest => dest.UnitOfMeasureStatus, opt => opt.MapFrom(src => src.UnitOfMeasure.Status));
    }
}