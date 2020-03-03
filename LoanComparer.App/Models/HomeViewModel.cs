using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanComparer.App.Models
{
    public class HomeViewModel
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string LoanType { get; set; }
    }
}