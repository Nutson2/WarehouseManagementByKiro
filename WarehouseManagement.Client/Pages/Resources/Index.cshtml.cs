using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Resources;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public IEnumerable<ResourceDto> Resources { get; set; } = Enumerable.Empty<ResourceDto>();

    public async Task OnGetAsync()
    {
        try
        {
            Resources = await _apiService.GetResourcesAsync();
        }
        catch (Exception ex)
        {
            // Log error and show empty list
            Resources = Enumerable.Empty<ResourceDto>();
            TempData["Error"] = "Ошибка при загрузке ресурсов: " + ex.Message;
        }
    }

    public async Task<IActionResult> OnPostArchiveAsync(int id)
    {
        try
        {
            await _apiService.ArchiveResourceAsync(id);
            TempData["Success"] = "Ресурс успешно архивирован";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при архивировании ресурса: " + ex.Message;
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            await _apiService.DeleteResourceAsync(id);
            TempData["Success"] = "Ресурс успешно удален";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при удалении ресурса: " + ex.Message;
        }

        return RedirectToPage();
    }
}