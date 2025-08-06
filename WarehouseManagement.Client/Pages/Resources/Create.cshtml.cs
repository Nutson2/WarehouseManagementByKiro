using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Resources;

public class CreateModel : PageModel
{
    private readonly IApiService _apiService;

    public CreateModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public CreateResourceModel Resource { get; set; } = new();

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
            var createDto = new CreateResourceDto
            {
                Name = Resource.Name
            };

            await _apiService.CreateResourceAsync(createDto);
            TempData["Success"] = "Ресурс успешно создан";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при создании ресурса: " + ex.Message);
            return Page();
        }
    }

    public class CreateResourceModel
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Наименование не может быть длиннее 100 символов")]
        public string Name { get; set; } = string.Empty;
    }
}