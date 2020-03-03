namespace LoanComparer.Data.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoanDbContext : DbContext
    {
        public LoanDbContext()
            : base("name=LoanModel")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Loaner> Loaners { get; set; }
        public virtual DbSet<LoanerWebsite> LoanerWebsites { get; set; }
        public virtual DbSet<Subscribe> Subscribes { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<LoanRequest> LoanRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Visits)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Subscribes)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Loaner>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Loaner>()
                .Property(e => e.LoanType)
                .IsUnicode(false);

            modelBuilder.Entity<Loaner>()
                .Property(e => e.MinimumAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Loaner>()
                .Property(e => e.MaximumAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Loaner>()
                .Property(e => e.Terms)
                .IsUnicode(false);

            modelBuilder.Entity<Loaner>()
                .HasMany(e => e.Visits)
                .WithRequired(e => e.Loaner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoanerWebsite>()
                .Property(e => e.siteName)
                .IsUnicode(false);

            modelBuilder.Entity<LoanerWebsite>()
                .HasMany(e => e.Loaners)
                .WithRequired(e => e.LoanerWebsite)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subscribe>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<LoanRequest>()
                .Property(e => e.Amount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<LoanRequest>()
                .Property(e => e.LoanType)
                .IsUnicode(false);
        }
    }
}
