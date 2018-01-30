using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HrApplicationFinal.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser () : base () { }

        public String Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SetupDate { get; set; }

        [Display(Name = "First Name"), Required(ErrorMessage = "Field can't be empty"), StringLength(maximumLength: 200, MinimumLength = 0, ErrorMessage = "must be between {1} to {2}")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name"), Required(ErrorMessage = "Field can't be empty"), StringLength(maximumLength: 200, MinimumLength = 0, ErrorMessage = "must be between {1} to {2}")]
        public String LastName { get; set; }

        [Display(Name = "Email adress"), Required(ErrorMessage = "Field can't be empty"), DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public override String Email { get; set; }

        [Display(Name = "Phone number"), Required(ErrorMessage = "Field can't be empty"), DataType(DataType.PhoneNumber, ErrorMessage = "Not a number"), Range(0, 30, ErrorMessage = "Please use values between {1} to {2}")]
        public override String PhoneNumber { get; set; }

        [Display(Name = "Hired Date"), Required(ErrorMessage = "Field can't be empty"), DataType(DataType.Date, ErrorMessage = "Not a date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        [Display(Name = "Salary"), Required(ErrorMessage = "Field can't be empty"), Range(0, 99999.99, ErrorMessage = "Salary can't be lower than {1} or bigger than {2}")]
        public Decimal Salary { get; set; }

        [Display(Name = "Minimum Salary"), Required(ErrorMessage = "Field can't be empty"), Range(0, 99999.99, ErrorMessage = "Minimum salary can't be lower than {1} or bigger than {2}")]
        public Decimal SalaryMin { get; set; }

        [Display(Name = "Maximum Salary"), Required(ErrorMessage = "Field can't be empty"), Range(0, 99999.99, ErrorMessage = "Maximum salary can't be lower than {1} or bigger than {2}")]
        public Decimal SalaryMax { get; set; }

        public virtual Department Department { get; set; }

        public string FullName
        {
            get { return FirstName + ", " + LastName; }
        }

    }
}
