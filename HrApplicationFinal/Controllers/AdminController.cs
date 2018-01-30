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
using HrApplicationFinal.Models.VieModels.AdminViewModels;
using HrApplicationFinal.Models.VievModels.AdminViewModels;

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
            var users = await _context.ApplicationUsers.ToListAsync();
            foreach(var user in users)
            {
                listOfUsers.Add(new ListUserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                });
            }
            return View(listOfUsers);
        }
    }
}