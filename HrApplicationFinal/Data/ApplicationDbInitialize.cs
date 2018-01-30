using HrApplicationFinal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApplicationFinal.Data
{
    public class ApplicationDbInitialize
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
         RoleManager<IdentityRole> roleManager, ILogger<ApplicationDbInitialize> logger)
        {
            context.Database.EnsureCreated();

            // Look for any users and deparments.
            if(!context.Departments.Any())
            {
                logger.LogInformation($"No departments! lets create some!");
                await CreateDefaultDepartment(logger, context);
            }
            if(!context.Users.Any())
            {
                logger.LogInformation($"No users! lets create some!");
                await CreateDefaultUserAndRoleForApplication(userManager, roleManager, logger, context);
            }
            else 
            {
                logger.LogInformation($"Database already seeded");
                return; // DB has already been seeded
            }
        }

        private static async Task CreateDefaultDepartment(ILogger<ApplicationDbInitialize> logger, ApplicationDbContext context)
        {
            logger.LogInformation($"Create default departments");
            var departments = new Department[]
            {
                new Department {Id = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.Economics, SetupDate = new DateTime(2015, 1, 12) },
                new Department {Id = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.HR, SetupDate = new DateTime(2015, 4, 12) },
                new Department {Id = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.IT, SetupDate = new DateTime(2015, 12, 12) },
                new Department {Id = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.Economics, SetupDate = new DateTime(2015, 5, 12) }
            }; 
            foreach(Department d in departments)
            {
                context.Departments.Add(d);
            }
        await context.SaveChangesAsync();   
        }

        private static async Task CreateDefaultUserAndRoleForApplication(UserManager<ApplicationUser> userManger, RoleManager<IdentityRole> roleManger, ILogger<ApplicationDbInitialize> logger, ApplicationDbContext context)
        {
            const string administratorRole = "Administrator";
            const string administratorEmail = "noreply@hr.com";
            const string userRole = "User";

            if (!await roleManger.RoleExistsAsync(administratorRole))
            {
                await CreateDefaultRole(roleManger, logger, administratorRole);
            }
            if (!await roleManger.RoleExistsAsync(userRole))
            {
                await CreateDefaultRole(roleManger, logger, userRole);
            }

            var adminUser = await CreateDefaultAdminUser(userManger, logger, administratorEmail, context);
            await SetPasswordForDefaultUser(userManger, logger, administratorEmail, adminUser);
            await AddDefaultRoleToDefaultUser(userManger, logger, administratorEmail, administratorRole, adminUser);
            await CreateDefaultUsers(userManger, logger, context, userRole);
        }

        private static async Task CreateDefaultUsers(UserManager<ApplicationUser> userManger, ILogger<ApplicationDbInitialize> logger, ApplicationDbContext context, string role)
        {
            var ITDepartment = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.IT)).FirstOrDefault();
            var HRDepartment = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.HR)).FirstOrDefault();
            var EconomicsDepartment = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.Economics)).FirstOrDefault();

            logger.LogInformation($"Create default users with email for application");
            var applicationUsers = new ApplicationUser[]
            {
                new ApplicationUser{UserName = "User1@hr.com", Email = "User1@hr.com", FirstName = "Daniel", LastName = "Bubben", HireDate = new DateTime(2011, 1, 1), Salary = 1423, SalaryMax = 12314, SalaryMin = 13, PhoneNumber = "124124", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartment },
                new ApplicationUser{UserName = "User2@hr.com", Email = "User2@hr.com", FirstName = "Erik", LastName = "Babe", HireDate = new DateTime(2013, 1, 1), Salary = 1253, SalaryMax = 12354, SalaryMin = 124, PhoneNumber = "543636", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartment },
                new ApplicationUser{UserName = "User3@hr.com", Email = "User3@hr.com", FirstName = "Fredrik", LastName = "Nilsson", HireDate = new DateTime(2014, 1, 1), Salary = 1223, SalaryMax = 12354, SalaryMin = 1235, PhoneNumber = "1246344124", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartment },
                new ApplicationUser{UserName = "User4@hr.com", Email = "User4@hr.com", FirstName = "Tri", LastName = "Svensson", HireDate = new DateTime(2015, 1, 1), Salary = 1223, SalaryMax = 12324, SalaryMin = 513, PhoneNumber = "4124124", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartment },
                new ApplicationUser{UserName = "User5@hr.com", Email = "User5@hr.com", FirstName = "Thin", LastName = "Nilsson", HireDate = new DateTime(2016, 1, 1), Salary = 1523, SalaryMax = 12324, SalaryMin = 1553, PhoneNumber = "235235", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsDepartment },
                new ApplicationUser{UserName = "User6@hr.com", Email = "User6@hr.com", FirstName = "Andreas", LastName = "Johansson", HireDate = new DateTime(2017, 1, 1), Salary = 1223, SalaryMax = 12234, SalaryMin = 5313, PhoneNumber = "235", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsDepartment },
                new ApplicationUser{UserName = "User7@hr.com", Email = "User7@hr.com", FirstName = "Jonathan", LastName = "Nilsson", HireDate = new DateTime(2011, 1, 1), Salary = 1423, SalaryMax = 12314, SalaryMin = 13, PhoneNumber = "124124", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartment },
                new ApplicationUser{UserName = "User8@hr.com", Email = "User8@hr.com", FirstName = "Erika", LastName = "Erikasson", HireDate = new DateTime(2013, 1, 1), Salary = 1253, SalaryMax = 12354, SalaryMin = 1254, PhoneNumber = "543636", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartment },
                new ApplicationUser{UserName = "User9@hr.com", Email = "User9@hr.com", FirstName = "Andrea", LastName = "Ólsson", HireDate = new DateTime(2014, 1, 1), Salary = 455, SalaryMax = 12354, SalaryMin = 1355, PhoneNumber = "1241", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartment },
                new ApplicationUser{UserName = "User10@hr.com", Email = "User10@hr.com", FirstName = "Sam", LastName = "Månsson", HireDate = new DateTime(2015, 1, 1), Salary = 124, SalaryMax = 12324, SalaryMin = 5143, PhoneNumber = "44", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartment },
                new ApplicationUser{UserName = "User11@hr.com", Email = "User11@hr.com", FirstName = "Werner", LastName = "Svensson", HireDate = new DateTime(2016, 1, 1), Salary = 634, SalaryMax = 12324, SalaryMin = 1153, PhoneNumber = "235235", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsDepartment },
                new ApplicationUser{UserName = "User12@hr.com", Email = "User12@hr.com", FirstName = "Vincent", LastName = "Kyllönen", HireDate = new DateTime(2017, 1, 1), Salary = 23523, SalaryMax = 12234, SalaryMin = 313, PhoneNumber = "235", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsDepartment },
                new ApplicationUser{UserName = "User13@hr.com", Email = "User13@hr.com", FirstName = "Lotta", LastName = "Kyllönen", HireDate = new DateTime(2011, 1, 1), Salary = 2352, SalaryMax = 12314, SalaryMin = 13, PhoneNumber = "124142124", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartment },
                new ApplicationUser{UserName = "User14@hr.com", Email = "User14@hr.com", FirstName = "Jannie", LastName = "Dansken", HireDate = new DateTime(2013, 1, 1), Salary = 1253, SalaryMax = 12354, SalaryMin = 1244, PhoneNumber = "541243636", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartment },
                new ApplicationUser{UserName = "User15@hr.com", Email = "User15@hr.com", FirstName = "eva", LastName = "Nilsson", HireDate = new DateTime(2014, 1, 1), Salary = 1223, SalaryMax = 12354, SalaryMin = 1435, PhoneNumber = "124124", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartment },
                new ApplicationUser{UserName = "User16@hr.com", Email = "User16@hr.com", FirstName = "Jan", LastName = "Dihn", HireDate = new DateTime(2015, 1, 1), Salary = 1623, SalaryMax = 12324, SalaryMin = 5113, PhoneNumber = "124", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartment },
                new ApplicationUser{UserName = "User17@hr.com", Email = "User17@hr.com", FirstName = "Dan", LastName = "Dihn", HireDate = new DateTime(2016, 1, 1), Salary = 414, SalaryMax = 12324, SalaryMin = 1153, PhoneNumber = "12414", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsDepartment },
                new ApplicationUser{UserName = "User18@hr.com", Email = "User18@hr.com", FirstName = "Hoa", LastName = "Dihn", HireDate = new DateTime(2017, 1, 1), Salary = 512, SalaryMax = 12234, SalaryMin = 3113, PhoneNumber = "241", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsDepartment }
            };
            foreach (ApplicationUser user in applicationUsers)
            {
                var ir = await userManger.CreateAsync(user);
                if (ir.Succeeded)
                {
                    logger.LogDebug($"Created default with email: `{user.Email}` successfully");
                }
                else
                {
                    var exception = new ApplicationException($"Default user with email `{user.Email}` cannot be created");
                    logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                    throw exception;
                }

                var createdUser = await userManger.FindByEmailAsync(user.Email);
                await SetPasswordForDefaultUser(userManger, logger, user.Email, user);
                await AddDefaultRoleToDefaultUser(userManger, logger, user.Email, role, user);

            }
            await context.SaveChangesAsync();
        }
        

        private static async Task CreateDefaultRole(RoleManager<IdentityRole> roleManger, ILogger<ApplicationDbInitialize> logger, string role)
        {
            logger.LogInformation($"Create the role `{role}` for application");
            var ir = await roleManger.CreateAsync(new IdentityRole(role)); 
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created the role `{role}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default role `{role}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task<ApplicationUser> CreateDefaultAdminUser(UserManager<ApplicationUser> userManger, ILogger<ApplicationDbInitialize> logger, string email, ApplicationDbContext context)
        {
            logger.LogInformation($"Create default Admin user with email `{email}` for application");
            var ITDepartment = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.IT)).FirstOrDefault();
            var user = new ApplicationUser {UserName = email, Email = email, FirstName = "Admin", LastName = "Adminsson", HireDate = new DateTime(1970, 1, 1), Salary = 123, SalaryMax = 1234, SalaryMin = 13, PhoneNumber = "124124", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartment};
            var ir = await userManger.CreateAsync(user);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created default Admin user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default Admin user `{email}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }

            var createdUser = await userManger.FindByEmailAsync(email);
            return createdUser;
        }


        private static async Task SetPasswordForDefaultUser(UserManager<ApplicationUser> userManger, ILogger<ApplicationDbInitialize> logger, string email, ApplicationUser user)
        {
            logger.LogInformation($"Set password for default user `{email}`");
            const string password = "Abc123!";
            var ir = await userManger.AddPasswordAsync(user, password);
            if (ir.Succeeded)
            {
                logger.LogTrace($"Set password `{password}` for default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Password for the user `{email}` cannot be set");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task AddDefaultRoleToDefaultUser(UserManager<ApplicationUser> userManger, ILogger<ApplicationDbInitialize> logger, string email, string role, ApplicationUser user)
        {
            logger.LogInformation($"Add default user `{email}` to role '{role}'");
            var ir = await userManger.AddToRoleAsync(user, role);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Added the role '{role}' to default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"The role `{role}` cannot be set for the user `{email}`");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }
        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            string errors = null;
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
    }
}
