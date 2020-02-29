using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanComparer.Data.Models
{
    public class RepaymentDetails
    {
        public decimal MonthlyPayment { get; set; }
        public decimal AmountLeft { get; set; }
        public int Duration { get; set; }
        public decimal PaymentPercentage { get; set; }
    }
}
