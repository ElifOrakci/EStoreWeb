using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuasarShop.Models;
using System.Security.Claims;

namespace QuasarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrators,ProductAdministrators")]
    public class CarouselImagesController : ControllerBase
    {
        private readonly ICarouselImageService carouselImageService;
        private readonly IFilesService filesService;

        public CarouselImagesController(
           ICarouselImageService carouselImageService,
            IFilesService filesService
            )
        {
            this.carouselImageService = carouselImageService;
            this.filesService = filesService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await carouselImageService.GetAll().ToListAsync());
        }
        public IActionResult Create()
        {
            return View(new CarouselImageViewModel { Enabled = true });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarouselImageViewModel model)
        {
            if (model.ImageFile is null)
            {
                TempData["error"] = "Görsel dosyası yükleyiniz";
                return View(model);
            }

            await carouselImageService.CreateAsync(
                 await filesService.ResizeImageAsync(model.ImageFile.OpenReadStream(), 1440, 480),
                 model.DateFirst,
                 model.DateEnd,
                 UserId!.Value,
                 model.Enabled);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var item = carouselImageService.GetById(id);
            var model = new CarouselImageViewModel
            {
                Enabled = item.Enabled,
                DateFirst = item.DateFirst,
                DateEnd = item.DateEnd,
                Image = item.Image,
                Id = id

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarouselImageViewModel model)
        {

            await carouselImageService.UpdateAsync(
                model.Id,
               model.ImageFile is null ? model.Image : await filesService.ResizeImageAsync(model.ImageFile.OpenReadStream(), 1440, 480),
                 model.DateFirst,
                 model.DateEnd,
                 model.Enabled);


            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await carouselImageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
