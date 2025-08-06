using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Clients;

public class IndexModel : PageModel
{
    private readonly IApiService _apiService;

    public IndexModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    public IEnumerable<ClientDto> Clients { get; set; } = Enumerable.Empty<ClientDto>();

    public async Task OnGetAsync()
    {
        try
        {
            Clients = await _apiService.GetClientsAsync();
        }
        catch (Exception ex)
        {
            Clients = Enumerable.Empty<ClientDto>();
            TempData["Error"] = "Ошибка при загрузке клиентов: " + ex.Message;
        }
    }

    public async Task<IActionResult> OnPostArchiveAsync(int id)
    {
        try
        {
            await _apiService.ArchiveClientAsync(id);
            TempData["Success"] = "Клиент успешно архивирован";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при архивировании клиента: " + ex.Message;
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            await _apiService.DeleteClientAsync(id);
            TempData["Success"] = "Клиент успешно удален";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Ошибка при удалении клиента: " + ex.Message;
        }

        return RedirectToPage();
    }
}