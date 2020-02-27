namespace LoanComparer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Loaner")]
    public partial class Loaner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loaner()
        {
            Visits = new HashSet<Visit>();
        }

        public int LoanerId { get; set; }

        [Required]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(50)]
        public string LoanType { get; set; }

        public decimal MinimumAmount { get; set; }

        public decimal MaximumAmount { get; set; }

        public int Duration { get; set; }

        public int SiteId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Terms { get; set; }

        public decimal Rate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visit> Visits { get; set; }

        public virtual LoanerWebsite LoanerWebsite { get; set; }
    }
}
