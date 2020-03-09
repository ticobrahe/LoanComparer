using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanComparer.App.Models
{
    public class ProviderLink
    {
        [Required]
        public string SiteName { get; set; }
    }
}