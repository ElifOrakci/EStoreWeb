using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuasarShopData;

namespace QuasarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrators,OrderAdministrators")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext context;

        public OrdersController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var result = await context
                .Orders
                .AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
            return View(result);
        }
        public async Task<IActionResult> Deliver(Guid id, string trackingNumber)
        {
            var result = await context.Orders.FindAsync(id);
            result.Status = DeliveryStatus.OnDelivery;
            result.CargoTrackingNumber = trackingNumber;
            context.Orders.Update(result);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Complete(Guid id)
        {
            var result = await context.Orders.FindAsync(id);
            result.Status = DeliveryStatus.Shipped;
            context.Orders.Update(result);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
