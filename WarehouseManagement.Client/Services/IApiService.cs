using WarehouseManagement.Client.Models;

namespace WarehouseManagement.Client.Services;

public interface IApiService
{
    // Resources
    Task<IEnumerable<ResourceDto>> GetResourcesAsync();
    Task<ResourceDto?> GetResourceAsync(int id);
    Task<ResourceDto> CreateResourceAsync(CreateResourceDto resource);
    Task<ResourceDto> UpdateResourceAsync(int id, UpdateResourceDto resource);
    Task DeleteResourceAsync(int id);
    Task ArchiveResourceAsync(int id);

    // Units of Measure
    Task<IEnumerable<UnitOfMeasureDto>> GetUnitsAsync();
    Task<UnitOfMeasureDto?> GetUnitAsync(int id);
    Task<UnitOfMeasureDto> CreateUnitAsync(CreateUnitOfMeasureDto unit);
    Task<UnitOfMeasureDto> UpdateUnitAsync(int id, UpdateUnitOfMeasureDto unit);
    Task DeleteUnitAsync(int id);
    Task ArchiveUnitAsync(int id);

    // Clients
    Task<IEnumerable<ClientDto>> GetClientsAsync();
    Task<ClientDto?> GetClientAsync(int id);
    Task<ClientDto> CreateClientAsync(CreateClientDto client);
    Task<ClientDto> UpdateClientAsync(int id, UpdateClientDto client);
    Task DeleteClientAsync(int id);
    Task ArchiveClientAsync(int id);

    // Balance
    Task<IEnumerable<BalanceDto>> GetBalanceAsync(int[]? resourceIds = null, int[]? unitIds = null);

    // Receipt Documents
    Task<IEnumerable<ReceiptDocumentDto>> GetReceiptsAsync(DateTime? dateFrom = null, DateTime? dateTo = null, 
        string[]? numbers = null, int[]? resourceIds = null, int[]? unitIds = null);
    Task<ReceiptDocumentDto?> GetReceiptAsync(int id);
    Task<ReceiptDocumentDto> CreateReceiptAsync(CreateReceiptDocumentDto receipt);
    Task<ReceiptDocumentDto> UpdateReceiptAsync(int id, UpdateReceiptDocumentDto receipt);
    Task DeleteReceiptAsync(int id);

    // Shipment Documents
    Task<IEnumerable<ShipmentDocumentDto>> GetShipmentsAsync(DateTime? dateFrom = null, DateTime? dateTo = null,
        string[]? numbers = null, int[]? resourceIds = null, int[]? unitIds = null);
    Task<ShipmentDocumentDto?> GetShipmentAsync(int id);
    Task<ShipmentDocumentDto> CreateShipmentAsync(CreateShipmentDocumentDto shipment);
    Task<ShipmentDocumentDto> UpdateShipmentAsync(int id, UpdateShipmentDocumentDto shipment);
    Task DeleteShipmentAsync(int id);
    Task ApproveShipmentAsync(int id);
    Task RevokeShipmentAsync(int id);
}