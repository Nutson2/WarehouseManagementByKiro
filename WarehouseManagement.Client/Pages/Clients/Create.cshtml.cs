using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Clients;

public class CreateModel : PageModel
{
    private readonly IApiService _apiService;

    public CreateModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public CreateClientModel Client { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var createDto = new CreateClientDto
            {
                Name = Client.Name,
                Address = Client.Address
            };

            await _apiService.CreateClientAsync(createDto);
            TempData["Success"] = "Клиент успешно создан";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при создании клиента: " + ex.Message);
            return Page();
        }
    }

    public class CreateClientModel
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Наименование не может быть длиннее 100 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Адрес обязателен для заполнения")]
        [StringLength(500, ErrorMessage = "Адрес не может быть длиннее 500 символов")]
        public string Address { get; set; } = string.Empty;
    }
}