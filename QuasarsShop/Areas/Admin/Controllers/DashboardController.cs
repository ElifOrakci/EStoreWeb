using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuasarShopData;

namespace QuasarShop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrators,ProductAdministrators,OrderAdministrators")]
public class DashboardController : Controller
{
    private readonly AppDbContext context;
    private readonly IProductsService productsService;

    public DashboardController(
        AppDbContext context,
        IProductsService productsService
        )
    {
        this.context = context;
        this.productsService = productsService;
    }
    public async Task<IActionResult> Index()
    {
        var month = DateTime.Today .AddMonths( -1 );
        var year = DateTime.Today.AddYears(-1);

        ViewBag.SalesMonthly = context
            .Orders
            .Where(p => p.Date > month)
            .AsEnumerable()
            .Sum(p => p.GrandTotal);

        ViewBag.SalesAnnually = context
           .Orders
           .Where(p => p.Date > month)
           .AsEnumerable()
           .Sum(p => p.GrandTotal);

        ViewBag.UsersCount = context
            .Users
            .Count();

        ViewBag.CommentsCount = context
            .Comments
            .Count(p => !p.Enabled);

        ViewBag.NewComments = await productsService.GetAllNewCommentsAsync();


        return View();
    }
}
  