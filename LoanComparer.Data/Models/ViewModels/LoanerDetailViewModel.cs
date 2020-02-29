using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanComparer.Data.Models.ViewModels
{
    public class LoanerDetailViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string LoanType { get; set; }
        public string Terms { get; set; }
        public decimal Rate { get; set; }
        public string SiteName { get; set; }
    }
}
