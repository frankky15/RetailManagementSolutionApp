using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.Models;
using RMS.Services;

namespace RMS.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {
        private readonly ILogger<BrandsController> _logger;
        private readonly IProductionService _productionService;

        public BrandsController(ILogger<BrandsController> logger, IProductionService productionService)
        {
            _logger = logger;
            _productionService = productionService;
        }

        public IActionResult Index()
        {
            var brands = _productionService.GetBrands();
            return View(brands);
        }

        public IActionResult Details(int id)
        {
            var product = _productionService.GetBrandById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("BrandName")] Brand brand)
        {
            if (!_productionService.AddBrand(brand))
                return View(brand);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var brand = _productionService.GetBrandById(id);

            if (brand == null)
                return NotFound();

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("BrandId,BrandName")] Brand brand)
        {
            if (!_productionService.UpdateBrand(brand))
                return View(brand);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var brand = _productionService.GetBrandById(id);

            if (brand == null)
                return NotFound();

            return View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var brand = _productionService.GetBrandById(id);

            if (brand == null)
                return NotFound();

            if (!_productionService.DeleteBrand(brand))
                return BadRequest("Failed to delete the brand.");

            return RedirectToAction(nameof(Index));
        }
    }
}
