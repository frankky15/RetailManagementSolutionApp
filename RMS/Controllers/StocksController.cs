using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RMS.Models;
using RMS.Services;

namespace RMS.Controllers
{
    [Authorize(Roles = "Admin,StockManager,Cashier,StoreManager")]
    public class StocksController : Controller
    {
        private readonly ILogger<StocksController> _logger;
        private readonly IProductionService _productionService;
        private readonly ISalesService _salesService;
        private readonly UserManager<ApplicationUser> _userManager;

        public StocksController(ILogger<StocksController> logger,
        IProductionService productionService,
        ISalesService salesService,
        UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _productionService = productionService;
            _salesService = salesService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Admin"))
                return RedirectToAction(nameof(AdminIndex));

            var staff = _salesService.GetStaffById(user.StaffId ?? 0);

            var stocks = _productionService.GetStocksWhere(s => s.StoreId == staff.StoreId);
            return View(stocks);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            ViewData["Stores"] = new SelectList(_salesService.GetStores(), "StoreId", "StoreId");
            var stocks = _productionService.GetStocksWhere(s => s.StoreId == 1);
            return View(stocks);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex(int StoreId = 1)
        {
            ViewData["Stores"] = new SelectList(_salesService.GetStores(), "StoreId", "StoreId", StoreId);
            var stocks = _productionService.GetStocksWhere(s => s.StoreId == StoreId);
            return View(stocks);
        }

        public IActionResult Details(int id)
        {
            var product = _productionService.GetProductById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [Authorize(Roles = "Admin,StockManager")]
        public async Task<IActionResult> Edit(int id, int StoreId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                var staff = _salesService.GetStaffById(user.StaffId ?? 0);
                if (staff.StoreId != StoreId)
                    return BadRequest("You don't have access to modify other stores stock.");
            }

            var stock = _productionService.GetStockFrom(id, StoreId);

            if (stock == null)
                return NotFound();

            return View(stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,StockManager")]
        public IActionResult Edit(int id, [Bind("ProductId,StoreId,Quantity")] Stock stock)
        {
            if (!_productionService.UpdateStock(stock))
                return View(stock);

            return RedirectToAction(nameof(Index));
        }
    }
}
