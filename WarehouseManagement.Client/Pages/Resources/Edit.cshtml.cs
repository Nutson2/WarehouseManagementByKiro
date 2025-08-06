using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WarehouseManagement.Client.Models;
using WarehouseManagement.Client.Services;

namespace WarehouseManagement.Client.Pages.Resources;

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
    public EditResourceModel Resource { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var resource = await _apiService.GetResourceAsync(id);
        if (resource == null)
        {
            return NotFound();
        }

        Id = id;
        Resource.Name = resource.Name;
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
            var updateDto = new UpdateResourceDto
            {
                Name = Resource.Name
            };

            await _apiService.UpdateResourceAsync(Id, updateDto);
            TempData["Success"] = "Ресурс успешно обновлен";
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Ошибка при обновлении ресурса: " + ex.Message);
            return Page();
        }
    }

    public class EditResourceModel
    {
        [Required(ErrorMessage = "Наименование обязательно для заполнения")]
        [StringLength(100, ErrorMessage = "Наименование не может быть длиннее 100 символов")]
        public string Name { get; set; } = string.Empty;
    }
}