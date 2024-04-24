using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuasarShop.Models;

namespace QuasarShop.Areas.Admin.Controllers;

[Area ("Admin")]
[Authorize(Roles ="Administrators,ProductAdministrators")]

public class CatalogsController : ControllerBase
{
    private readonly ICatalogsService catalogsService;
    private readonly string entityName = "Katalog";

    public CatalogsController(
      ICatalogsService catalogsService
        )
    {
        this.catalogsService = catalogsService;
    }
    [Authorize(Roles = "Administrators,ProductAdministrators ,OrderAdministrators")]
    public async Task <IActionResult> Index()
    {
        var result = await catalogsService.GetAll().Include(p => p.User).ToListAsync();
        return View(result);
    }
    public IActionResult Create()
    {
        return View(new CatalogViewModel { Enabled = true });
    }
    [HttpPost]
    public async Task <IActionResult> Create(CatalogViewModel model)
    {
       try
       {
            await catalogsService.Create(model.Name, model.Enabled, UserId!.Value);
            TempData["success"] = $"{entityName} ekleme işlemi başarıyla tamamlanmıştır!";
            return RedirectToAction(nameof(Index));
        }
        // 2627 is unique constraint (includes primary key), 2601 is unique index
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
        {
            TempData["error"] = $"{model.Name} kataloğu zaten var";
            return View();
        }
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var item= await catalogsService.GetById(id);
        if (item is null)
            return RedirectToAction(nameof(Index));
            return View(new CatalogViewModel
            {
                Name = item.Name,
                Enabled =item.Enabled
          });
    }
    [HttpPost]
    public async Task < IActionResult> Edit(Guid id, CatalogViewModel model)
    {

        var item = await catalogsService.GetById(id);

        item.Name = model.Name;
        item.Enabled = model.Enabled;

        TempData["success"] = $"{entityName} güncelleme işlemi başarıyla tamamlanmıştır!";

        await catalogsService.Update(item);
        return RedirectToAction(nameof (Index));
    }
    public async Task<IActionResult> Delete(Guid id)
    {
        await catalogsService.Delete(id);
        TempData["success"] = $"{entityName} silme işlemi başarıyla tamamlanmıştır!";
        return RedirectToAction(nameof (Index));
    }
}
