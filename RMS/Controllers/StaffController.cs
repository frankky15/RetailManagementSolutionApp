using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RMS.Models;
using RMS.Services;

namespace RMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly ISalesService _salesService;

        public StaffController(ILogger<StaffController> logger, ISalesService salesService)
        {
            _logger = logger;
            _salesService = salesService;
        }

        public IActionResult Index()
        {
            var staff = _salesService.GetStaff();
            return View(staff);
        }

        public IActionResult Details(int id)
        {
            var staff = _salesService.GetStaffById(id);

            if (staff == null)
                return NotFound();

            return View(staff);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName,LastName,Email,Phone,Active,StoreId,ManagerId")] Staff staff)
        {
            if (!_salesService.AddStaff(staff))
                return View(staff);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var staff = _salesService.GetStaffById(id);

            if (staff == null)
                return NotFound();

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StaffId,FirstName,LastName,Email,Phone,Active,StoreId,ManagerId")] Staff staff)
        {
            if (!_salesService.UpdateStaff(staff))
                return View(staff);

            return RedirectToAction(nameof(Index));
        }
    }
}