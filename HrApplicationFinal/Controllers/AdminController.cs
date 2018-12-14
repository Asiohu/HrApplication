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
            var listOfUsers = new List<ListUserViewModel>();
            var users = await _context.ApplicationUsers.Include(a => a.Department).ThenInclude(b => b.Country).OrderBy(x => x.Department.Country.CountryName).ThenBy(y => y.Department.DepartmentName).ThenBy(a => a.FullName).ToListAsync();
            foreach(var user in users)
            {
                listOfUsers.Add(new ListUserViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    DepartmentName = user.Department?.DepartmentName,
                    CountryName = user.Department.Country?.CountryName
                });
            }
            return View(listOfUsers);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ListDeparment()
        {
            _logger.LogInformation($"ListDeparments in the AdminController called");
            var listOfDeparments = new List<ListDeparmentViewModel>();
            var deparments = await _context.Departments.ToListAsync();
            foreach (var department in deparments)
            {
                listOfDeparments.Add(new ListDeparmentViewModel
                {
                    Id = department.DepartmentId,
                    DepartmentName = department.DepartmentName,
                    DepartmentBudget = department.DepartmentBudget
                });
            }
            return View(listOfDeparments);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ListCompanyTreeStructure()
        {
            _logger.LogInformation($"ListCountry in the AdminController called");
            var listOfCountries = new List<ListCompanyTreeStructureViewModel>();
            var countries = await _context.Countries.Include("Departments").ToListAsync();

            foreach (var country in countries)
            {
                listOfCountries.Add(new ListCompanyTreeStructureViewModel
                {
                    Id = country.CountryId,
                    CountryName = country.CountryName,
                    Capital = country.Capital,
                    Department = country.Departments.ToList()
                });
            }

            return View(listOfCountries);
        }
    }
}