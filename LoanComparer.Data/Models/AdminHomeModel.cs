using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanComparer.Data.Models
{
    public class AdminHomeModel
    {
        public string CompanyName { get; set; }
        public string LoanType { get; set; }
        public int UniqueVisit { get; set; }
        public int Visits { get; set; }
    }
}
