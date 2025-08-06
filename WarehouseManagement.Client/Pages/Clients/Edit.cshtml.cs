using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Clients;

public class EditModel : PageModel
{
    private readonly IApiService _apiService;

    public EditModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public int Id { get; set; }

    [BindProperty]
    public EditClientModel Client { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = await _apiService.GetClientAsync(id);
        if (client == null)
        {
            return NotFound();
        }

        Id = id;
        Client.Name = client.Name;
        Client.Address = client.Address;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var updateDto = new UpdateClientDto
            {
                Name = Client.Name,
                Address = Client.Address
            };

            await _apiService.UpdateClientAsync(Id, updateDto);
            TempData["Success"] = "Клиент успешно обновлен";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при обновлении клиента: " + ex.Message);
            return Page();
        }
    }

    public class EditClientModel
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Наименование не может быть длиннее 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Адрес обязателен для заполнения")]
        [StringLength(500, ErrorMessage = "Адрес не может быть длиннее 500 символов")]
        public string Address { get; set; } = string.Empty;
    }
}