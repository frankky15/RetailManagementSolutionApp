using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RMS.Models;
using RMS.Services;

namespace RMS.Controllers
{
    [Authorize(Roles = "Admin,StoreManager")]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly ISalesService _salesService;

        public OrdersController(ILogger<OrdersController> logger, ISalesService salesService)
        {
            _logger = logger;
            _salesService = salesService;
        }

        public IActionResult Index()
        {
            var storeId = 1; // Change based on the user's store.
            var orders = _salesService.GetOrdersWhere(o => o.StoreId == storeId);
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _salesService.GetOrderById(id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = _salesService.GetOrderById(id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _salesService.GetOrderById(id);

            if (order == null)
                return NotFound();

            if (!_salesService.DeleteOrder(order))
                return BadRequest("Failed to delete the order.");

            return RedirectToAction(nameof(Index));
        }
    }
}
