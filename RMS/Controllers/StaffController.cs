using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RMS.Models;
using RMS.Services;

namespace RMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly ISalesService _salesService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public StaffController(ILogger<StaffController> logger,
        ISalesService salesService,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _salesService = salesService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var staff = _salesService.GetStaff();
            return View(staff);
        }

        public async Task<IActionResult> Details(int id)
        {
            var staff = _salesService.GetStaffById(id);

            if (staff == null)
                return NotFound();

            if (staff.User != null)
            {
                var roles = _roleManager.Roles.AsNoTracking().ToList();
                var userRoles = new List<string>();
                for (int i = 0; i < roles.Count(); i++)
                {
                    var role = roles[i].Name ?? string.Empty;
                    if (await _userManager.IsInRoleAsync(staff.User, role))
                        userRoles.Add(role);
                }

                ViewData["Roles"] = userRoles;
            }

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

        public async Task<IActionResult> RolesDetails(int id)
        {
            var staff = _salesService.GetStaffById(id);

            if (staff == null)
                return NotFound();

            if (staff.User == null)
                return NotFound();

            var roles = _roleManager.Roles.AsNoTracking().ToList();
            var userRoles = new List<string>();
            for (int i = 0; i < roles.Count(); i++)
            {
                var role = roles[i].Name ?? string.Empty;
                if (await _userManager.IsInRoleAsync(staff.User, role))
                    userRoles.Add(role);
            }

            ViewData["Roles"] = userRoles;
            return View(staff);
        }

        public async Task<IActionResult> AddRole(int id)
        {
            var staff = _salesService.GetStaffById(id);

            if (staff == null)
                return NotFound();

            if (staff.User == null)
                return NotFound();

            var roles = _roleManager.Roles.AsNoTracking().ToList();
            var missingRoles = new List<String>();
            for (int i = 0; i < roles.Count(); i++)
            {
                var role = roles[i].Name ?? string.Empty;
                if (!await _userManager.IsInRoleAsync(staff.User, role))
                    missingRoles.Add(role);
            }

            ViewData["Roles"] = new SelectList(missingRoles);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(int id, [Bind("Name")] IdentityRole role)
        {
            var staff = _salesService.GetStaffById(id);

            var result = await _userManager.AddToRoleAsync(staff.User, role.Name ?? "");
            if (!result.Succeeded)
                Console.WriteLine("Error: There was a problem while trying to add a role to a user.");

            return RedirectToAction(nameof(RolesDetails), new { id });
        }


        public async Task<IActionResult> RemoveRole(int id, string role)
        {
            var staff = _salesService.GetStaffById(id);

            if (staff == null)
                return NotFound();

            if (staff.User == null)
                return NotFound();

            if (!await _userManager.IsInRoleAsync(staff.User, role))
                return BadRequest("Error: User does not have that role.");

            ViewData["Role"] = role;
            return View();
        }

        [HttpPost, ActionName("RemoveRole")]
        public async Task<IActionResult> RemoveConfirmed(int id, [Bind("Name")] IdentityRole role)
        {
            var staff = _salesService.GetStaffById(id);

            var result = await _userManager.RemoveFromRoleAsync(staff.User, role.Name ?? "");
            if (!result.Succeeded)
                Console.WriteLine("Error: There was a problem while trying to remove a role from a user.");

            return RedirectToAction(nameof(RolesDetails), new { id });
        }
    }
}