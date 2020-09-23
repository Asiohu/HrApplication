using HrApplicationFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
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

            if(!context.Countries.Any())
            {
                logger.LogInformation($"No Countries! lets create some!");
                await CreateDefaultCountries(logger, context);
            }
            if (!context.Departments.Any())
            {
                logger.LogInformation($"No departments! lets create some!");
                await CreateDefaultDepartments(logger, context);
            }
            if (!context.Users.Any())
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

        private static async Task CreateDefaultCountries(ILogger<ApplicationDbInitialize> logger, ApplicationDbContext context)
        {
            logger.LogInformation($"Create default countries");
            var countries = new Country[]
            {
                new Country {CountryId = Guid.NewGuid().ToString(), CountryName = CountryNames.Denmark.ToString(), Capital = CountryCapitalName.Copenhagen.ToString(), SetupDate = new DateTime(2015, 1, 12) },
                new Country {CountryId = Guid.NewGuid().ToString(), CountryName = CountryNames.Sweden.ToString(), Capital = CountryCapitalName.Stockholm.ToString(), SetupDate = new DateTime(2015, 1, 12) },
                new Country {CountryId = Guid.NewGuid().ToString(), CountryName = CountryNames.Norway.ToString(), Capital = CountryCapitalName.Oslo.ToString(), SetupDate = new DateTime(2016, 1, 12) },
                new Country {CountryId = Guid.NewGuid().ToString(), CountryName = CountryNames.Japan.ToString(), Capital = CountryCapitalName.Tokyo.ToString(), SetupDate = new DateTime(2014, 1, 12) }
            };
            foreach (Country c in countries)
            {
                context.Countries.Add(c);
            }
            await context.SaveChangesAsync();
        }

        private static async Task CreateDefaultDepartments(ILogger<ApplicationDbInitialize> logger, ApplicationDbContext context)
        {
            var Japan = context.Countries.Where(x => x.CountryName.Equals(CountryNames.Japan.ToString())).Single();
            var Sweden = context.Countries.Where(x => x.CountryName.Equals(CountryNames.Sweden.ToString())).Single();
            var Denmark = context.Countries.Where(x => x.CountryName.Equals(CountryNames.Denmark.ToString())).Single();
            var Norway = context.Countries.Where(x => x.CountryName.Equals(CountryNames.Norway.ToString())).Single();


            logger.LogInformation($"Create default departments");
            var departments = new Department[]
            {
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.Economics.ToString(), SetupDate = new DateTime(2015, 1, 12), DepartmentBudget = 300, Country = Japan, DepartmentCity = "Tokyo", DepartmentPhone = "+81123456", DepartmentAddressLine = "2 Chome-7-7-9 Nagatachō, 千代田区 Chiyoda-ku, Tōkyō-to 100-0014" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.HR.ToString(), SetupDate = new DateTime(2015, 4, 12), DepartmentBudget = 500, Country = Japan, DepartmentCity = "Tokyo", DepartmentPhone = "+834636", DepartmentAddressLine = "2 Chome-10-5 Nagatachō, Chiyoda-ku, Tōkyō-to 100-0014" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.IT.ToString(), SetupDate = new DateTime(2015, 12, 12), DepartmentBudget = 5500, Country = Japan, DepartmentCity = "Tokyo", DepartmentPhone = "+834636", DepartmentAddressLine = "2 Chome-1-2 Nagatachō, Chiyoda-ku, Tōkyō-to 100-0014, Japan" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.Economics.ToString(), SetupDate = new DateTime(2015, 1, 12), DepartmentBudget = 300, Country = Sweden, DepartmentCity = "Malmö", DepartmentPhone = "+46879494", DepartmentAddressLine = "Gatan 1" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.HR.ToString(), SetupDate = new DateTime(2015, 4, 12), DepartmentBudget = 500, Country = Sweden, DepartmentCity = "Hässleholm", DepartmentPhone = "+46465464", DepartmentAddressLine = "Gatan 2" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.Economics.ToString(), SetupDate = new DateTime(2015, 1, 12), DepartmentBudget = 300, Country = Denmark, DepartmentCity = "Kolding", DepartmentPhone = "+4523564", DepartmentAddressLine = "Dansk Gatan 1" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.HR.ToString(), SetupDate = new DateTime(2015, 4, 12), DepartmentBudget = 500, Country = Denmark, DepartmentCity = " Copenhagen[", DepartmentPhone = "+4546235", DepartmentAddressLine = "Dansk Gatan 2" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.IT.ToString(), SetupDate = new DateTime(2015, 12, 12), DepartmentBudget = 5500, Country = Denmark, DepartmentCity = "Copenhagen[", DepartmentPhone = "+45124464", DepartmentAddressLine = "Dansk Gatan 3" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.Economics.ToString(), SetupDate = new DateTime(2015, 1, 12), DepartmentBudget = 300, Country = Norway, DepartmentCity = "Bodø", DepartmentPhone = "+4742354", DepartmentAddressLine = "Norsk Gatan 1" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.HR.ToString(), SetupDate = new DateTime(2015, 4, 12), DepartmentBudget = 500, Country = Norway, DepartmentCity = "Arendal", DepartmentPhone = "+471251", DepartmentAddressLine = "Norsk Gatan 2" },
                new Department {DepartmentId = Guid.NewGuid().ToString(), DepartmentName = DepartmentNames.IT.ToString(), SetupDate = new DateTime(2015, 12, 12), DepartmentBudget = 5500, Country = Norway, DepartmentCity = "Gjøvik", DepartmentPhone = "+47346346", DepartmentAddressLine = "Norsk Gatan 3" },
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
            var ITDepartmentJapan = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.IT.ToString()) && x.Country.CountryName.Equals(CountryNames.Japan.ToString())).Single();
            var ITDepartmentDenmark = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.IT.ToString()) && x.Country.CountryName.Equals(CountryNames.Denmark.ToString())).Single();
            var ITDepartmentNorway = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.IT.ToString()) && x.Country.CountryName.Equals(CountryNames.Norway.ToString())).Single();

            var HRDepartmentJapan = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.HR.ToString()) && x.Country.CountryName.Equals(CountryNames.Japan.ToString())).Single();
            var HRDepartmentSweden = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.HR.ToString()) && x.Country.CountryName.Equals(CountryNames.Sweden.ToString())).Single();
            var HRDepartmentDenmark = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.HR.ToString()) && x.Country.CountryName.Equals(CountryNames.Denmark.ToString())).Single();
            var HRDepartmentNorway = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.HR.ToString()) && x.Country.CountryName.Equals(CountryNames.Norway.ToString())).Single();

            var EconomicsDepartmentJapan = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.Economics.ToString()) && x.Country.CountryName.Equals(CountryNames.Japan.ToString())).Single();
            var EconomicsDepartmentSweden = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.Economics.ToString()) && x.Country.CountryName.Equals(CountryNames.Sweden.ToString())).Single();
            var EconomicsTDepartmentDenmark = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.Economics.ToString()) && x.Country.CountryName.Equals(CountryNames.Denmark.ToString())).Single();
            var EconomicsDepartmentNorway = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.Economics.ToString()) && x.Country.CountryName.Equals(CountryNames.Norway.ToString())).Single();

            logger.LogInformation($"Create default users with email for application");
            var applicationUsers = new ApplicationUser[]
            {
                new ApplicationUser{UserName = "Brian@hr.com", Email = "Brian@hr.com", FirstName = "Brian", LastName = "Cicero", HireDate = new DateTime(2011, 1, 1), Salary = 1423, SalaryMax = 12314, SalaryMin = 13, PhoneNumber = "124124", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartmentJapan },
                new ApplicationUser{UserName = "Phillip@hr.com", Email = "Phillip@hr.com", FirstName = "Phillip", LastName = "Newman", HireDate = new DateTime(2014, 1, 1), Salary = 1223, SalaryMax = 12354, SalaryMin = 1235, PhoneNumber = "1246344124", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartmentDenmark },
                new ApplicationUser{UserName = "Michael@hr.com", Email = "Michael@hr.com", FirstName = "Michael", LastName = "Dillon", HireDate = new DateTime(2015, 1, 1), Salary = 1223, SalaryMax = 12324, SalaryMin = 513, PhoneNumber = "4124124", SetupDate = new DateTime(2015, 1, 12), Department = ITDepartmentNorway },
                new ApplicationUser{UserName = "User5@hr.com", Email = "User5@hr.com", FirstName = "ey", LastName = "Johansson", HireDate = new DateTime(2017, 1, 1), Salary = 1223, SalaryMax = 12234, SalaryMin = 5313, PhoneNumber = "235", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartmentJapan },
                new ApplicationUser{UserName = "User7@hr.com", Email = "User7@hr.com", FirstName = "Jodgdfgnathan", LastName = "Nilsson", HireDate = new DateTime(2011, 1, 1), Salary = 1423, SalaryMax = 12314, SalaryMin = 13, PhoneNumber = "124124", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartmentSweden },
                new ApplicationUser{UserName = "User8@hr.com", Email = "User8@hr.com", FirstName = "Erdfgdgika", LastName = "Erikasson", HireDate = new DateTime(2013, 1, 1), Salary = 1253, SalaryMax = 12354, SalaryMin = 1254, PhoneNumber = "543636", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartmentDenmark },
                new ApplicationUser{UserName = "User9@hr.com", Email = "User9@hr.com", FirstName = "dfgdfg", LastName = "Ólsson", HireDate = new DateTime(2014, 1, 1), Salary = 455, SalaryMax = 12354, SalaryMin = 1355, PhoneNumber = "1241", SetupDate = new DateTime(2015, 1, 12), Department = HRDepartmentNorway },
                new ApplicationUser{UserName = "User10@hr.com", Email = "User10@hr.com", FirstName = "dfgdfg", LastName = "Månsson", HireDate = new DateTime(2015, 1, 1), Salary = 124, SalaryMax = 12324, SalaryMin = 5143, PhoneNumber = "44", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsDepartmentJapan },
                new ApplicationUser{UserName = "User11@hr.com", Email = "User11@hr.com", FirstName = "dfgdfg", LastName = "Svensson", HireDate = new DateTime(2016, 1, 1), Salary = 634, SalaryMax = 12324, SalaryMin = 1153, PhoneNumber = "235235", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsDepartmentSweden },
                new ApplicationUser{UserName = "User12@hr.com", Email = "User12@hr.com", FirstName = "dfgdfg", LastName = "Kyllönen", HireDate = new DateTime(2017, 1, 1), Salary = 23523, SalaryMax = 12234, SalaryMin = 313, PhoneNumber = "235", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsTDepartmentDenmark },
                new ApplicationUser{UserName = "User13@hr.com", Email = "User13@hr.com", FirstName = "dfgdfg", LastName = "Kyllönen", HireDate = new DateTime(2011, 1, 1), Salary = 2352, SalaryMax = 12314, SalaryMin = 13, PhoneNumber = "124142124", SetupDate = new DateTime(2015, 1, 12), Department = EconomicsDepartmentNorway }
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
            var ITDepartment = context.Departments.Where(x => x.DepartmentName.Equals(DepartmentNames.IT.ToString()) && x.Country.CountryName.Equals(CountryNames.Japan.ToString())).Single();
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
