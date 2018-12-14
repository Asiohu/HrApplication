using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApplicationFinal.Models
{
    public class Department
    {
        [Key]
        public string DepartmentId { get; set; }

        [Display(Name = "Department name"), Required(ErrorMessage = "Field can't be empty"), StringLength(maximumLength: 200, MinimumLength = 0, ErrorMessage = "must be between {1} to {2}")]
        public string DepartmentName { get; set; }

        [Display(Name = "Department budget"), Required(ErrorMessage = "Field can't be empty"), DataType(DataType.Currency)]
        public decimal DepartmentBudget { get; set; }

        [Display(Name = "City"), Required(ErrorMessage = "Field can't be empty"), StringLength(maximumLength: 200, MinimumLength = 0, ErrorMessage = "must be between {1} to {2}")]
        public string DepartmentCity { get; set; }

        [Display(Name = "Phone"), Required(ErrorMessage = "Field can't be empty"), RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage = "The Phone Number field must contain only numbers")]
        public string DepartmentPhone { get; set; }

        [Display(Name = "Address"), Required(ErrorMessage = "Field can't be empty"), StringLength(maximumLength: 300, MinimumLength = 0, ErrorMessage = "must be between {1} to {2}")]
        public string DepartmentAddressLine { get; set; }

   
        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SetupDate { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

        [Required]
        public Country Country { get; set; }
    }

    public enum DepartmentNames
    {
        Economics, IT, HR
    }
}
