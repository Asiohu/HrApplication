using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApplicationFinal.Models.ViewModels.AdminViewModels
{
    public class ListDeparmentViewModel
    {
        public string Id { get; set; }
        public string DepartmentName { get; set; }
        public decimal DepartmentBudget { get; set; }
    }
}
