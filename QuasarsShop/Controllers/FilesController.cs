using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace QuasarShop.Controllers
{
    public class FilesController : Controller
    {
        private readonly IProductsService productsService;

        public FilesController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        [OutputCache(Duration = 86400)]
        public IActionResult ProductImage(Guid id)
        {
                        
            return File(productsService.GetProductImageBytes(id),"image/jpeg");
        }
    }
}
