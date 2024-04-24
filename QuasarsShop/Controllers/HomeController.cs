using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuasarShop.Models;
using QuasarShopData;
using System.Diagnostics;

namespace QuasarShop.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ICarouselImageService caroeselImageService;
        private readonly IProductsService productsService;
        private readonly ICatalogsService catalogsService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ICarouselImageService caroeselImageService,
            IProductsService productsService,
            ICatalogsService catalogsService,
            ILogger<HomeController> logger)
        {
            this.caroeselImageService = caroeselImageService;
            this.productsService = productsService;
            this.catalogsService = catalogsService;
            _logger = logger;
        }

        public async Task < IActionResult> Index()
        {
            ViewBag.CarouselImages = await caroeselImageService.GetAll().AsNoTracking().Where(p => p.Enabled && (DateTime.UtcNow > p.DateFirst || p.DateFirst == null) && (DateTime.UtcNow < p.DateEnd || p.DateEnd == null)).ToListAsync();
            ViewBag.BestSellers = await  productsService.GetBestSellersAsync(UserId, 12);
            return View();
        }
        public async Task<IActionResult> Catalog(Guid id)
        {
            var model = await catalogsService
                 .GetAll()
                 .AsNoTracking()
                 .Include(p => p.Products)
                 .Select(p => new
                 {
                     p.Id,
                     p.Name,
                     Products = p.Products
                     .Where(p=>p.Enabled)
                     .Select(q=> new ProductBoxViewModel {
                        Id = q.Id,
                         Name = q.Name,
                         DiscountedPrice= q.DiscountedPrice,
                         DiscountRate= q.DiscountRate,
                         Image= q.Image,
                         Price= q.Price,
                         IsInFavorites = q.Favorites.Any(r=> r.UserId == UserId),
                     })
                  })
                .SingleOrDefaultAsync(p=> p.Id == id);
            return View(model);
        }
         
        public async Task<IActionResult> Search(string  keywords)
        {
           
            return View(await productsService.GetByKeywords(keywords,UserId ));
        }

        public async Task<IActionResult> Product(Guid id)
        {

            var imageUrl = Url.Action("ProductImage", "Files", new { id });

            var numbers = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" }
            };

            // Store the list in ViewBag
            ViewBag.Rating = new SelectList(numbers, "Value", "Text");

            var item = await productsService
                .GetAll()
                .AsNoTracking()
                .Include(p => p.Comments)
                .ThenInclude(p => p.User)
                .Where(p => p.Enabled)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.DiscountRate,
                    p.DiscountedPrice,
                    p.Description,
                    Catalogs = p.Catalogs.Where(q => q.Enabled).Select(q => new { q.Id, q.Name }),
                    Comments = p.Comments.Where(q => q.Enabled || q.UserId == UserId!.Value).OrderByDescending(p => p.Date).Select(q => new { q.Id, q.Rate, q.Text, q.Date, q.UserName }),
                    ProductImages = p.ProductImages.Select(q => new { q.Id, q.Image }),
                    //Image = imageUrl,
                    p.Image,
                    IsInFavorites = p.Favorites.Any(q => q.UserId == UserId)
                })
                .SingleOrDefaultAsync(p => p.Id == id);
            if(item is null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        [Authorize(Roles = "Members")]
        public async Task<IActionResult> Favorites()
        {
            return View(await productsService.GetFavoriteProducts(UserId!.Value));
        }

        [Authorize(Roles = "Members")]

        public async Task<IActionResult>RemoveFromFavorites(Guid id)
        {
            await productsService.RemoveFromFavorites(id,UserId!.Value);
            return RedirectToAction(nameof(Favorites));
        }
        [Authorize(Roles = "Members")]

        public async Task<IActionResult> ClearFavorites()
        {
            await productsService.ClearFavorites( UserId!.Value);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AddToShoppingCart(Guid id, int? quantity,string? returnUrl)
        {
            if(User.IsInRole("Administrators"))
            {
                return Redirect(returnUrl ?? "/");
            }
         await productsService.AddToShoppingCart(id, UserId!.Value, quantity ?? 1);
            return Redirect(returnUrl ?? "/");  
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View( new CheckoutViewModel {ShoppingCartItems = await productsService.GetShoppingCart(UserId!.Value)});
        }
        [Authorize]
        public async Task<IActionResult> RemoveFromShoppingCart(Guid id)
        {
            await productsService.RemoveFromCart(id);
            return RedirectToAction(nameof(Checkout));
        }
        [Authorize]
        public async Task<IActionResult> ClearShoppingCart()
        {
            await productsService.ClearShoppingCart(UserId!.Value);
            return RedirectToAction(nameof(Checkout));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {
            await productsService.AddComment(model.ProductId, UserId!.Value,model.Text,model.Rating);
            return RedirectToAction(nameof(Product), new {id = model.ProductId });
        }
        [Authorize]
        public async Task<IActionResult> Payment()
        {
            var result = await productsService.Payment(UserId!.Value);
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}