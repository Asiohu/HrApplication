using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApplicationFinal.Models.ViewModels.AdminViewModels
{
    public class ListCompanyTreeStructureViewModel
    {
        public string Id { get; set; }
        public string CountryName { get; set; }
        public string Capital { get; set; }
        public List<Department> Department{ get; set; }
    }
}
