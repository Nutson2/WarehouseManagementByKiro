using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Units;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public IEnumerable<UnitOfMeasureDto> Units { get; set; } = Enumerable.Empty<UnitOfMeasureDto>();

    public async Task OnGetAsync()
    {
        try
        {
            Units = await _apiService.GetUnitsAsync();
        }
        catch (Exception ex)
        {
            Units = Enumerable.Empty<UnitOfMeasureDto>();
            TempData["Error"] = "Ошибка при загрузке единиц измерения: " + ex.Message;
        }
    }

    public async Task<IActionResult> OnPostArchiveAsync(int id)
    {
        try
        {
            await _apiService.ArchiveUnitAsync(id);
            TempData["Success"] = "Единица измерения успешно архивирована";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при архивировании единицы измерения: " + ex.Message;
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            await _apiService.DeleteUnitAsync(id);
            TempData["Success"] = "Единица измерения успешно удалена";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при удалении единицы измерения: " + ex.Message;
        }

        return RedirectToPage();
    }
}