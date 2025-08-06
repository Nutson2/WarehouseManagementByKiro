using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Units;

public class CreateModel : PageModel
{
    private readonly IApiService _apiService;

    public CreateModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public CreateUnitModel Unit { get; set; } = new();

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
            var createDto = new CreateUnitOfMeasureDto
            {
                Name = Unit.Name
            };

            await _apiService.CreateUnitAsync(createDto);
            TempData["Success"] = "Единица измерения успешно создана";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при создании единицы измерения: " + ex.Message);
            return Page();
        }
    }

    public class CreateUnitModel
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Наименование не может быть длиннее 50 символов")]
        public string Name { get; set; } = string.Empty;
    }
}