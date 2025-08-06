using System.Text;
using System.Text.Json;
using WarehouseManagement.Client.Models;

namespace WarehouseManagement.Client.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("WarehouseAPI");
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
    }

    // Resources
    public async Task<IEnumerable<ResourceDto>> GetResourcesAsync()
    {
        var response = await _httpClient.GetAsync("api/resources");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<ResourceDto>>(json, _jsonOptions) ?? Enumerable.Empty<ResourceDto>();
    }

    public async Task<ResourceDto?> GetResourceAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/resources/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ResourceDto>(json, _jsonOptions);
    }

    public async Task<ResourceDto> CreateResourceAsync(CreateResourceDto resource)
    {
        var json = JsonSerializer.Serialize(resource, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/resources", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ResourceDto>(responseJson, _jsonOptions)!;
    }

    public async Task<ResourceDto> UpdateResourceAsync(int id, UpdateResourceDto resource)
    {
        var json = JsonSerializer.Serialize(resource, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/resources/{id}", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ResourceDto>(responseJson, _jsonOptions)!;
    }

    public async Task DeleteResourceAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/resources/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task ArchiveResourceAsync(int id)
    {
        var response = await _httpClient.PutAsync($"api/resources/{id}/archive", null);
        response.EnsureSuccessStatusCode();
    }

    // Units of Measure
    public async Task<IEnumerable<UnitOfMeasureDto>> GetUnitsAsync()
    {
        var response = await _httpClient.GetAsync("api/units");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<UnitOfMeasureDto>>(json, _jsonOptions) ?? Enumerable.Empty<UnitOfMeasureDto>();
    }

    public async Task<UnitOfMeasureDto?> GetUnitAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/units/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UnitOfMeasureDto>(json, _jsonOptions);
    }

    public async Task<UnitOfMeasureDto> CreateUnitAsync(CreateUnitOfMeasureDto unit)
    {
        var json = JsonSerializer.Serialize(unit, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/units", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UnitOfMeasureDto>(responseJson, _jsonOptions)!;
    }

    public async Task<UnitOfMeasureDto> UpdateUnitAsync(int id, UpdateUnitOfMeasureDto unit)
    {
        var json = JsonSerializer.Serialize(unit, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/units/{id}", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UnitOfMeasureDto>(responseJson, _jsonOptions)!;
    }

    public async Task DeleteUnitAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/units/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task ArchiveUnitAsync(int id)
    {
        var response = await _httpClient.PutAsync($"api/units/{id}/archive", null);
        response.EnsureSuccessStatusCode();
    }

    // Clients
    public async Task<IEnumerable<ClientDto>> GetClientsAsync()
    {
        var response = await _httpClient.GetAsync("api/clients");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<ClientDto>>(json, _jsonOptions) ?? Enumerable.Empty<ClientDto>();
    }

    public async Task<ClientDto?> GetClientAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/clients/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ClientDto>(json, _jsonOptions);
    }

    public async Task<ClientDto> CreateClientAsync(CreateClientDto client)
    {
        var json = JsonSerializer.Serialize(client, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/clients", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ClientDto>(responseJson, _jsonOptions)!;
    }

    public async Task<ClientDto> UpdateClientAsync(int id, UpdateClientDto client)
    {
        var json = JsonSerializer.Serialize(client, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/clients/{id}", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ClientDto>(responseJson, _jsonOptions)!;
    }

    public async Task DeleteClientAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/clients/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task ArchiveClientAsync(int id)
    {
        var response = await _httpClient.PutAsync($"api/clients/{id}/archive", null);
        response.EnsureSuccessStatusCode();
    }

    // Balance
    public async Task<IEnumerable<BalanceDto>> GetBalanceAsync(int[]? resourceIds = null, int[]? unitIds = null)
    {
        var queryParams = new List<string>();
        
        if (resourceIds?.Any() == true)
            queryParams.Add($"resourceIds={string.Join(",", resourceIds)}");
        
        if (unitIds?.Any() == true)
            queryParams.Add($"unitIds={string.Join(",", unitIds)}");

        var query = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
        var response = await _httpClient.GetAsync($"api/balance{query}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<BalanceDto>>(json, _jsonOptions) ?? Enumerable.Empty<BalanceDto>();
    }

    // Receipt Documents
    public async Task<IEnumerable<ReceiptDocumentDto>> GetReceiptsAsync(DateTime? dateFrom = null, DateTime? dateTo = null,
        string[]? numbers = null, int[]? resourceIds = null, int[]? unitIds = null)
    {
        var queryParams = new List<string>();
        
        if (dateFrom.HasValue)
            queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
        
        if (dateTo.HasValue)
            queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");
        
        if (numbers?.Any() == true)
            queryParams.Add($"numbers={string.Join(",", numbers)}");
        
        if (resourceIds?.Any() == true)
            queryParams.Add($"resourceIds={string.Join(",", resourceIds)}");
        
        if (unitIds?.Any() == true)
            queryParams.Add($"unitIds={string.Join(",", unitIds)}");

        var query = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
        var response = await _httpClient.GetAsync($"api/receipts{query}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<ReceiptDocumentDto>>(json, _jsonOptions) ?? Enumerable.Empty<ReceiptDocumentDto>();
    }

    public async Task<ReceiptDocumentDto?> GetReceiptAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/receipts/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ReceiptDocumentDto>(json, _jsonOptions);
    }

    public async Task<ReceiptDocumentDto> CreateReceiptAsync(CreateReceiptDocumentDto receipt)
    {
        var json = JsonSerializer.Serialize(receipt, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/receipts", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ReceiptDocumentDto>(responseJson, _jsonOptions)!;
    }

    public async Task<ReceiptDocumentDto> UpdateReceiptAsync(int id, UpdateReceiptDocumentDto receipt)
    {
        var json = JsonSerializer.Serialize(receipt, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/receipts/{id}", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ReceiptDocumentDto>(responseJson, _jsonOptions)!;
    }

    public async Task DeleteReceiptAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/receipts/{id}");
        response.EnsureSuccessStatusCode();
    }

    // Shipment Documents
    public async Task<IEnumerable<ShipmentDocumentDto>> GetShipmentsAsync(DateTime? dateFrom = null, DateTime? dateTo = null,
        string[]? numbers = null, int[]? resourceIds = null, int[]? unitIds = null)
    {
        var queryParams = new List<string>();
        
        if (dateFrom.HasValue)
            queryParams.Add($"dateFrom={dateFrom.Value:yyyy-MM-dd}");
        
        if (dateTo.HasValue)
            queryParams.Add($"dateTo={dateTo.Value:yyyy-MM-dd}");
        
        if (numbers?.Any() == true)
            queryParams.Add($"numbers={string.Join(",", numbers)}");
        
        if (resourceIds?.Any() == true)
            queryParams.Add($"resourceIds={string.Join(",", resourceIds)}");
        
        if (unitIds?.Any() == true)
            queryParams.Add($"unitIds={string.Join(",", unitIds)}");

        var query = queryParams.Any() ? "?" + string.Join("&", queryParams) : "";
        var response = await _httpClient.GetAsync($"api/shipments{query}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<ShipmentDocumentDto>>(json, _jsonOptions) ?? Enumerable.Empty<ShipmentDocumentDto>();
    }

    public async Task<ShipmentDocumentDto?> GetShipmentAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/shipments/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ShipmentDocumentDto>(json, _jsonOptions);
    }

    public async Task<ShipmentDocumentDto> CreateShipmentAsync(CreateShipmentDocumentDto shipment)
    {
        var json = JsonSerializer.Serialize(shipment, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/shipments", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ShipmentDocumentDto>(responseJson, _jsonOptions)!;
    }

    public async Task<ShipmentDocumentDto> UpdateShipmentAsync(int id, UpdateShipmentDocumentDto shipment)
    {
        var json = JsonSerializer.Serialize(shipment, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"api/shipments/{id}", content);
        response.EnsureSuccessStatusCode();
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ShipmentDocumentDto>(responseJson, _jsonOptions)!;
    }

    public async Task DeleteShipmentAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/shipments/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task ApproveShipmentAsync(int id)
    {
        var response = await _httpClient.PutAsync($"api/shipments/{id}/approve", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task RevokeShipmentAsync(int id)
    {
        var response = await _httpClient.PutAsync($"api/shipments/{id}/revoke", null);
        response.EnsureSuccessStatusCode();
    }
}