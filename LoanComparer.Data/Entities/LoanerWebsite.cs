namespace LoanComparer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoanerWebsite")]
    public partial class LoanerWebsite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoanerWebsite()
        {
            Loaners = new HashSet<Loaner>();
        }

        [Key]
        public int SiteId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string siteName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loaner> Loaners { get; set; }
    }
}
