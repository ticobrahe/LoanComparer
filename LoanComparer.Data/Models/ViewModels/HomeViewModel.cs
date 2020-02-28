using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanComparer.Data.Models.ViewModels
{
    public class HomeViewModel
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int Duration { get; set; }
        public string LoanType { get; set; }
    }
}
