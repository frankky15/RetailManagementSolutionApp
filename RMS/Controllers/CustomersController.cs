using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RMS.Models;
using RMS.Services;

namespace RMS.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ISalesService _salesService;

        public CustomersController(ILogger<CustomersController> logger, ISalesService salesService)
        {
            _logger = logger;
            _salesService = salesService;
        }

        public IActionResult Index()
        {
            var customers = _salesService.GetCustomers();
            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var customer = _salesService.GetCustomerById(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var customer = _salesService.GetCustomerById(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var customer = _salesService.GetCustomerById(id);

            if (customer == null)
                return NotFound();

            if (!_salesService.DeleteCustomer(customer))
                return BadRequest("Failed to delete the customer.");

            return RedirectToAction(nameof(Index));
        }
    }
}
