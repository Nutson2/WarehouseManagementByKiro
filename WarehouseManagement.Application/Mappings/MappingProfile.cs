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
    }
}