namespace LoanComparer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Subscribe")]
    public partial class Subscribe
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public decimal? Amount { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
