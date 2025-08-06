using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Balance;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
    }

    public IEnumerable<BalanceDto> Balance { get; set; } = Enumerable.Empty<BalanceDto>();
    public IEnumerable<ResourceDto> Resources { get; set; } = Enumerable.Empty<ResourceDto>();
    public IEnumerable<UnitOfMeasureDto> Units { get; set; } = Enumerable.Empty<UnitOfMeasureDto>();

    public async Task<IActionResult> OnGetAsync(int[]? resourceIds = null, int[]? unitIds = null)
    {
        try
        {
            // Загружаем данные параллельно
            var balanceTask = _apiService.GetBalanceAsync(resourceIds, unitIds);
            var resourcesTask = _apiService.GetResourcesAsync();
            var unitsTask = _apiService.GetUnitsAsync();

            await Task.WhenAll(balanceTask, resourcesTask, unitsTask);

            Balance = await balanceTask;
            Resources = await resourcesTask;
            Units = await unitsTask;

            return Page();
        }
        catch (Exception)
        {
            // Логирование ошибки
            TempData["ErrorMessage"] = "Ошибка при загрузке данных баланса склада.";
            
            // Возвращаем пустые коллекции для корректного отображения страницы
            Balance = Enumerable.Empty<BalanceDto>();
            Resources = Enumerable.Empty<ResourceDto>();
            Units = Enumerable.Empty<UnitOfMeasureDto>();
            
            return Page();
        }
    }

    public async Task<IActionResult> OnGetBalanceAsync(int[]? resourceIds = null, int[]? unitIds = null)
    {
        try
        {
            var balance = await _apiService.GetBalanceAsync(resourceIds, unitIds);
            return new JsonResult(balance);
        }
        catch (Exception)
        {
            return new JsonResult(new { error = "Ошибка при загрузке баланса склада" })
            {
                StatusCode = 500
            };
        }
    }
}