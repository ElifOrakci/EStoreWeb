using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuasarShop.Models;
using System.Globalization;
using X.PagedList;
using IO = System.IO;

namespace QuasarShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrators,ProductAdministrators")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService productsService;
    private readonly ICatalogsService catalogsService;
    private readonly IFilesService filesService;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly string entityName = "Ürün";
    public ProductsController(
        IProductsService productsService,
        ICatalogsService catalogsService,
        IFilesService filesService,
        IWebHostEnvironment webHostEnvironment
        )
    {
        this.productsService = productsService;
        this.catalogsService = catalogsService;
        this.filesService = filesService;
        this.webHostEnvironment = webHostEnvironment;
    }

    [Authorize(Roles = "Administrators,ProductAdministrators,OrderAdministrators")]
    public IActionResult Index(int? page)
    {
        var result = productsService
            .GetAll().Include(p => p.User).Include(p => p.Catalogs).ToPagedList(page ?? 1, 10);
        return View(result);
    }
    public async Task<IActionResult> Create()
    {
        await PopulateDropdowns();
        return View(new ProductViewModel { Enabled = true, DiscountRate = "0",Price = "0" });
    }

    private async Task PopulateDropdowns()
    {
        ViewBag.Catalogs = (await catalogsService.GetAll().Where(p => p.Enabled).OrderBy(p => p.Name).Select(p => new { p.Id, p.Name }).ToListAsync()).Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel model)
    {
        var image = model.Image is not null ? await filesService.ResizeImageAsync(
                model.Image.OpenReadStream(),
                IO.File.Open(Path.Combine(webHostEnvironment.WebRootPath, "images", "logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read),
                new SixLabors.ImageSharp.Size(800, 600)) : default;
        var images = model.Images is not null ? model.Images.Select(p => filesService.ResizeImageAsync(
                        p.OpenReadStream(),
                        IO.File.Open(Path.Combine(webHostEnvironment.WebRootPath, "images", "logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read),
                        new SixLabors.ImageSharp.Size(800, 600)).Result) : default;
        await productsService.Create(
            model.Name,
            model.Enabled,
            UserId!.Value,
            description: model.Description,
            price: decimal.Parse(model.Price, CultureInfo.CreateSpecificCulture("tr-TR")),
            discountRate: int.Parse(model.DiscountRate),
            image: image,
            images: images,
            catalogs: model.Catalogs
            );
        TempData["success"] = $"{entityName} ekleme işlemi başarıyla tamamlanmıştır!";
        return RedirectToAction(nameof(Index));

    }
    public async Task<IActionResult> Edit(Guid id)
    {
        var item = await productsService.GetByIdWithCatalogs(id);
        if (item is null)
            return RedirectToAction(nameof(Index));
        await PopulateDropdowns();
        return View(new ProductViewModel
        {
            Name = item.Name,
            Enabled = item.Enabled,
            Description = item.Description,
            DiscountRate = item.DiscountRate.ToString(),
            OriginalImage = item.Image,
            OriginalImages = item.ProductImages.Select(p => new OriginalImage { Id = p.Id, Image = p.Image }).ToList(),
            Price = item.Price.ToString("f2", CultureInfo.CreateSpecificCulture("tr-TR")),
            Catalogs = item.Catalogs.Select(p => p.Id).ToList()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, ProductViewModel model)
    {
        var image = model.Image is not null ? await filesService.ResizeImageAsync(
                model.Image.OpenReadStream(),
                IO.File.Open(Path.Combine(webHostEnvironment.WebRootPath, "images", "logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read),
                new SixLabors.ImageSharp.Size(800, 600)) : default;
        var images = model.Images is not null ? model.Images.Select(p => filesService.ResizeImageAsync(
                        p.OpenReadStream(),
                        IO.File.Open(Path.Combine(webHostEnvironment.WebRootPath, "images", "logo.png"), FileMode.Open, FileAccess.Read, FileShare.Read),
                        new SixLabors.ImageSharp.Size(800, 600)).Result) : default;


        var item = await productsService.GetByIdWithCatalogs(id);

        item.Name = model.Name;
        item.Enabled = model.Enabled;
        item.Price = decimal.Parse(model.Price, CultureInfo.CreateSpecificCulture("tr-TR"));
        item.Description = model.Description;
        item.DiscountRate = int.Parse(model.DiscountRate);
        if (image is not null)
            item.Image = image;

        TempData["success"] = $"{entityName} güncelleme işlemi başarıyla tamamlanmıştır!";

        await productsService.Update(item, model.Catalogs, images, model.ImagesToDelete);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        await productsService.Delete(id);
        TempData["success"] = $"{entityName} silme işlemi başarıyla tamamlanmıştır!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> EnableComment(Guid id)
    {
      await productsService.EnableComment(id);
        return RedirectToAction("Index" ,"Dashboard");
    }
    public async Task<IActionResult> RemoveComment(Guid id)
    {
        await productsService.RemoveComment(id);
        return RedirectToAction("Index", "Dashboard");
    }
}