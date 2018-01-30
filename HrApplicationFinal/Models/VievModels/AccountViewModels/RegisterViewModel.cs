using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrApplicationFinal.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        
        [Display(Name = "Email adress"), Required(ErrorMessage = "Field can't be empty"), DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Display(Name = "Password"), Required(ErrorMessage = "Field can't be empty"), DataType(DataType.Password), StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Confirm password"), Required(ErrorMessage = "Field can't be empty"), DataType(DataType.Password),  Compare("Password", ErrorMessage = "The password and confirmation password do not match."), StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
        /**
        [Display(Name = "First Name"), Required(ErrorMessage = "Field can't be empty"), StringLength(maximumLength: 200, MinimumLength = 0, ErrorMessage = "{0} is between {1} to {2}")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name"), Required(ErrorMessage = "Field can't be empty"), StringLength(maximumLength: 200, MinimumLength = 0, ErrorMessage = "{0} is between to {2}")]
        public string LastName { get; set; }

        [Display(Name = "Phone number"), Required(ErrorMessage = "Field can't be empty"), DataType(DataType.PhoneNumber, ErrorMessage = "Not a number"), Range(0, 30, ErrorMessage = "Please use values between 0 to 30")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hired Date"), Required(ErrorMessage = "Field can't be empty"), DataType(DataType.DateTime, ErrorMessage = "Not a date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Salary"), Required(ErrorMessage = "Field can't be empty"), Range(0, 99999.99, ErrorMessage = "Salary can't be lower than {1} or bigger than {2}")]
        public Decimal Salary { get; set; }

        [Display(Name = "Minimum Salary"), Required(ErrorMessage = "Field can't be empty"), Range(0, 99999.99, ErrorMessage = "Minimum salary can't be lower than {1} or bigger than {2}")]
        public Decimal SalaryMin { get; set; }

        [Display(Name = "Maximum Salary"), Required(ErrorMessage = "Field can't be empty"), Range(0, 99999.99, ErrorMessage = "Maximum salary can't be lower than {1} or bigger than {2}")]
        public Decimal SalaryMax { get; set; } **/
    }
}
