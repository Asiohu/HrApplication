using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HrApplicationFinal.Models
{
    public class Country
    {
        [Key]
        public string CountryId { get; set; }

        [Display(Name = "Country name"), Required(ErrorMessage = "Field can't be empty"), StringLength(maximumLength: 200, MinimumLength = 0, ErrorMessage = "must be between {1} to {2}")]
        public string CountryName { get; set; }

        [Display(Name = "Country Capital name"), Required(ErrorMessage = "Field can't be empty"), StringLength(maximumLength: 200, MinimumLength = 0, ErrorMessage = "must be between {1} to {2}")]
        public string Capital { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SetupDate { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

    }

    public enum CountryNames
    {
        Sweden, Denmark, Norway, Japan
    }

    public enum CountryCapitalName
    {
        Stockholm, Copenhagen, Oslo, Tokyo
    }
}
