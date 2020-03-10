using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanComparer.App.Models
{
    public class CreateProviderViewModel
    {
        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        public string LoanType { get; set; }
        [Required]
        public decimal MinimumAmount { get; set; }
        [Required]
        public decimal MaximumAmount { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string ProviderLink { get; set; }

        [Required]
        public string Terms { get; set; }
        [Required]
        public decimal Rate { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}