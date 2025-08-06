using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Balance;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public IEnumerable<BalanceDto> Balance { get; set; } = Enumerable.Empty<BalanceDto>();

    public async Task OnGetAsync()
    {
        try
        {
            Balance = await _apiService.GetBalanceAsync();
        }
        catch (Exception ex)
        {
            // Log error and show empty list
            Balance = Enumerable.Empty<BalanceDto>();
            TempData["Error"] = "Ошибка при загрузке баланса: " + ex.Message;
        }
    }
}