using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMS.Models;
using RMS.Services;

namespace RMS.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IProductionService _productionService;

        public CategoriesController(ILogger<CategoriesController> logger, IProductionService productionService)
        {
            _logger = logger;
            _productionService = productionService;
        }

        public IActionResult Index()
        {
            var categories = _productionService.GetCategories();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = _productionService.GetCategoryById(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryName")] Category category)
        {
            if (!_productionService.AddCategory(category))
                return View(category);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var category = _productionService.GetCategoryById(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (!_productionService.UpdateCategory(category))
                return View(category);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _productionService.GetCategoryById(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _productionService.GetCategoryById(id);

            if (category == null)
                return NotFound();

            if (!_productionService.DeleteCategory(category))
                return BadRequest("Failed to delete the category.");

            return RedirectToAction(nameof(Index));
        }
    }
}
