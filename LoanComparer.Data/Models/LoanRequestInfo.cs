using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanComparer.Data.Models
{
    public class LoanRequestInfo
    {
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public string LoanType { get; set; }
    }
}
