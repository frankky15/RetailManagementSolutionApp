using Microsoft.AspNetCore.Mvc;
using RMS.Models;
using RMS.Services;

namespace RMS.Controllers
{
    public class StocksController : Controller
    {
        private readonly ILogger<StocksController> _logger;
        private readonly IProductionService _productionService;

        public StocksController(ILogger<StocksController> logger, IProductionService productionService)
        {
            _logger = logger;
            _productionService = productionService;
        }

        public IActionResult Index()
        {
            var storeId = 1; // Change based on the user's store.
            var stocks = _productionService.GetStocksWhere(s => s.StoreId == storeId);
            return View(stocks);
        }

        public IActionResult Details(int id)
        {
            var product = _productionService.GetProductById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var storeId = 1; // Change based on the user's store.
            var stock = _productionService.GetStockFrom(id, storeId);

            if (stock == null)
                return NotFound();

            return View(stock);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,StoreId,Quantity")] Stock stock)
        {
            if (!_productionService.UpdateStock(stock))
                return View(stock);

            return RedirectToAction(nameof(Index));
        }
    }
}
