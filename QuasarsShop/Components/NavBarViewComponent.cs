using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuasarShop.Models;
using System.Security.Claims;

namespace QuasarShop.Components;

public class NavBarViewComponent : ViewComponent
{
    private readonly ICatalogsService catalogsService;
    private readonly IProductsService productsService;

    public NavBarViewComponent(
        ICatalogsService catalogsService,
        IProductsService productsService
        ) 
    {
        this.catalogsService = catalogsService;
        this.productsService = productsService;
    }

    public async Task <IViewComponentResult> InvokeAsync()
    {
        var userId = Guid.Parse( UserClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());
        return View(new NavBarViewModel 
        {
            Catalogs = await catalogsService.GetAll().Where (p=>p.Enabled).ToListAsync(),
            FavoriteCount = User.Identity.IsAuthenticated ? await productsService.GetFavoriteCount(userId) : 0,
            ShoppingCartItemCount = User.Identity.IsAuthenticated ? await productsService.GetShoppingCartCount(userId) :0
        });
    }
}
