using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;
using RMS.Data;
using RMS.Models;
using RMS.Repository;
using RMS.Services;

namespace RMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductionService _productionService;

        public ProductsController(ILogger<ProductsController> logger, IProductionService productionService)
        {
            _logger = logger;
            _productionService = productionService;
        }

        public IActionResult Index()
        {
            var products = _productionService.GetProducts();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productionService.GetProductById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_productionService.GetBrands(), "BrandId", "BrandName");
            ViewData["CategoryId"] = new SelectList(_productionService.GetCategories(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductName,BrandId,CategoryId,ModelYear,ListPrice")] Product product)
        {
            if (!_productionService.AddProduct(product))
            {
                ViewData["BrandId"] = new SelectList(_productionService.GetBrands(), "BrandId", "BrandName", product.BrandId);
                ViewData["CategoryId"] = new SelectList(_productionService.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = _productionService.GetProductById(id);

            if (product == null)
                return NotFound();

            ViewData["BrandId"] = new SelectList(_productionService.GetBrands(), "BrandId", "BrandName", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_productionService.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProductId,ProductName,BrandId,CategoryId,ModelYear,ListPrice")] Product product)
        {
            if (!_productionService.UpdateProduct(product))
            {
                ViewData["BrandId"] = new SelectList(_productionService.GetBrands(), "BrandId", "BrandName", product.BrandId);
                ViewData["CategoryId"] = new SelectList(_productionService.GetCategories(), "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _productionService.GetProductById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productionService.GetProductById(id);

            if (product == null)
                return NotFound();

            if (!_productionService.DeleteProduct(product))
                return BadRequest("Failed to delete the product.");

            return RedirectToAction(nameof(Index));
        }
    }
}
