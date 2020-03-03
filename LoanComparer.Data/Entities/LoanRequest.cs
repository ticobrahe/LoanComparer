namespace LoanComparer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoanRequest")]
    public partial class LoanRequest
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string LoanType { get; set; }

        public int Duration { get; set; }
    }
}
