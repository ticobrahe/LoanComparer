namespace LoanComparer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visit")]
    public partial class Visit
    {
        public int VisitId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public int VisitCount { get; set; }

        public int UniqueCount { get; set; }

        public int LoanerId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Loaner Loaner { get; set; }
    }
}
