using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HrApplicationFinal.Models
{
    public class Department
    {
        [Key]
        public string Id { get; set; }
        public DepartmentNames DepartmentName { get; set; }
        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SetupDate { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }

    }

    public enum DepartmentNames
    {
        Economics, IT, HR
    }
}
