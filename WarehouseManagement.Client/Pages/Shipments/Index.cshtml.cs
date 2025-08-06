using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Shipments;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public IEnumerable<ShipmentDocumentDto> Shipments { get; set; } = Enumerable.Empty<ShipmentDocumentDto>();

    public async Task OnGetAsync()
    {
        try
        {
            Shipments = await _apiService.GetShipmentsAsync();
        }
        catch (Exception ex)
        {
            Shipments = Enumerable.Empty<ShipmentDocumentDto>();
            TempData["Error"] = "Ошибка при загрузке документов отгрузки: " + ex.Message;
        }
    }

    public async Task<IActionResult> OnPostApproveAsync(int id)
    {
        try
        {
            await _apiService.ApproveShipmentAsync(id);
            TempData["Success"] = "Документ отгрузки успешно подписан";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при подписании документа отгрузки: " + ex.Message;
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRevokeAsync(int id)
    {
        try
        {
            await _apiService.RevokeShipmentAsync(id);
            TempData["Success"] = "Документ отгрузки успешно отозван";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при отзыве документа отгрузки: " + ex.Message;
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            await _apiService.DeleteShipmentAsync(id);
            TempData["Success"] = "Документ отгрузки успешно удален";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при удалении документа отгрузки: " + ex.Message;
        }

        return RedirectToPage();
    }
}