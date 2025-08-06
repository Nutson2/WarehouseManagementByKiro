using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Receipts;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public IEnumerable<ReceiptDocumentDto> Receipts { get; set; } = Enumerable.Empty<ReceiptDocumentDto>();

    public async Task OnGetAsync()
    {
        try
        {
            Receipts = await _apiService.GetReceiptsAsync();
        }
        catch (Exception ex)
        {
            Receipts = Enumerable.Empty<ReceiptDocumentDto>();
            TempData["Error"] = "Ошибка при загрузке документов поступления: " + ex.Message;
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            await _apiService.DeleteReceiptAsync(id);
            TempData["Success"] = "Документ поступления успешно удален";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при удалении документа поступления: " + ex.Message;
        }

        return RedirectToPage();
    }
}