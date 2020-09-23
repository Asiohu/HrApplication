using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using HrApplicationFinal.Models;
using HrApplicationFinal.Services;
using HrApplicationFinal.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HrApplicationFinal.Models.ViewModels.AdminViewModels;

namespace HrApplicationFinal.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public AdminController(UserManager<ApplicationUser> userManager, ILogger<ManageController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ListUser()
        {
            _logger.LogInformation($"ListUsers in the AdminController called");
           
            var listOfUsers = await _context.ApplicationUsers.Include(a => a.Department).ThenInclude(b => b.Country).Select(c => new ListUserViewModel()
            {
                Id = c.Id,
                FullName = c.FullName,
                DepartmentName = c.Department.DepartmentName,
                CountryName = c.Department.Country.CountryName,
            }).OrderBy(c => c.CountryName).ThenBy(y => y.DepartmentName).ThenBy(a => a.FullName).ToListAsync();
           
            return View(listOfUsers);
        }

        

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDeparmentByDeparmentId(string deparmentId)
        {
            _logger.LogInformation($"GetDeparmentByDeparmentId in the AdminController called");

            var deparment = await _context.Departments.Where(c => c.DepartmentId == deparmentId).Select(c => new EditDeparmentViewModel()
            {
                Id = c.DepartmentId,
                DepartmentName = c.DepartmentName,
                DepartmentCity = c.DepartmentCity,
                DepartmentAddressLine = c.DepartmentAddressLine,
                DepartmentPhone = c.DepartmentPhone
            }).FirstOrDefaultAsync();

            return View(deparment);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveDeparmentId(EditDeparmentViewModel editDeparmentViewModel)
        {
            if(ModelState.IsValid)
            {
                if(!String.IsNullOrWhiteSpace(editDeparmentViewModel.Id))
                {
                    var departmentToBeEdited = await _context.Departments.Where(c => c.DepartmentId == editDeparmentViewModel.Id).SingleOrDefaultAsync();

                    departmentToBeEdited.DepartmentName = editDeparmentViewModel.DepartmentName;
                    departmentToBeEdited.DepartmentCity = editDeparmentViewModel.DepartmentCity;
                    departmentToBeEdited.DepartmentPhone = editDeparmentViewModel.DepartmentPhone;
                    departmentToBeEdited.DepartmentAddressLine = editDeparmentViewModel.DepartmentAddressLine;

                    _context.SaveChanges();

                    return RedirectToAction("ListCompanyTreeStructure");
                }
            }

            return RedirectToAction("ListCompanyTreeStructure");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetEmployeesByDeparmentId(string deparmentId)
        {
            _logger.LogInformation($"GetEmployeesByDeparmentId in the AdminController called");

            var listOfUsers = await _context.ApplicationUsers.Include(a => a.Department).Where(c => c.Department.DepartmentId == deparmentId).Select(c => new ListUserViewModel()
            {
                Id = c.Id,
                FullName = c.FullName
            }).OrderBy(c => c.FullName).ToListAsync();

            return View(listOfUsers);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ListDeparment()
        {
            _logger.LogInformation($"ListDeparments in the AdminController called");

            var listOfDeparments = await _context.Departments.Select(c => new ListDeparmentViewModel()
            {
                Id = c.DepartmentId,
                DepartmentName = c.DepartmentName,
                DepartmentBudget = c.DepartmentBudget
            }).ToListAsync();
           
            return View(listOfDeparments);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ListCompanyTreeStructure()
        {
            _logger.LogInformation($"ListCountry in the AdminController called");

            var listOfCountries = await _context.Countries.Include("Departments").Select(c => new ListCompanyTreeStructureViewModel() 
            {
                Id = c.CountryId,
                CountryName = c.CountryName,
                Capital = c.Capital,
                Department = c.Departments.ToList()
            }).ToListAsync();

            return View(listOfCountries);
        }
    }
}