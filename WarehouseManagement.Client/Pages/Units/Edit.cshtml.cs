using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Units;

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
    public EditUnitModel Unit { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var unit = await _apiService.GetUnitAsync(id);
        if (unit == null)
        {
            return NotFound();
        }

        Id = id;
        Unit.Name = unit.Name;
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
            var updateDto = new UpdateUnitOfMeasureDto
            {
                Name = Unit.Name
            };

            await _apiService.UpdateUnitAsync(Id, updateDto);
            TempData["Success"] = "Единица измерения успешно обновлена";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при обновлении единицы измерения: " + ex.Message);
            return Page();
        }
    }

    public class EditUnitModel
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Наименование не может быть длиннее 50 символов")]
        public string Name { get; set; } = string.Empty;
    }
}