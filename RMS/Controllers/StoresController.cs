using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RMS.Models;
using RMS.Services;

namespace RMS.Controllers
{
    [Authorize]
    public class StoresController : Controller
    {
        private readonly ILogger<StoresController> _logger;
        private readonly ISalesService _salesService;

        public StoresController(ILogger<StoresController> logger, ISalesService salesService)
        {
            _logger = logger;
            _salesService = salesService;
        }

        public IActionResult Index()
        {
            var stores = _salesService.GetStores();
            return View(stores);
        }

        public IActionResult Details(int id)
        {
            var store = _salesService.GetStoreById(id);

            if (store == null)
                return NotFound();

            return View(store);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StoreName,Phone,Email,Street,City,State,ZipCode")] Store store)
        {
            if (!_salesService.AddStore(store))
                return View(store);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var store = _salesService.GetStoreById(id);

            if (store == null)
                return NotFound();

            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StoreId,StoreName,Phone,Email,Street,City,State,ZipCode")] Store store)
        {
            if (!_salesService.UpdateStore(store))
                return View(store);

            return RedirectToAction(nameof(Index));
        }
    }
}