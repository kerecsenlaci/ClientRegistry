namespace CRegistry.Dal
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RegistryModel : DbContext
    {
        public RegistryModel()
            : base("name=RegistryModel")
        {
        }

        public virtual DbSet<ContactDbModel> contacts { get; set; }
        public virtual DbSet<CountyDbModel> county { get; set; }
        public virtual DbSet<ParameterDbModel> parameters { get; set; }
        public virtual DbSet<PartnerDbModel> partners { get; set; }
        public virtual DbSet<PartnerTypeDbModel> partnertype { get; set; }
        public virtual DbSet<SwitchDbModel> _switch { get; set; }
        public virtual DbSet<UserDbModel> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactDbModel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ContactDbModel>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<ContactDbModel>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<ContactDbModel>()
                .HasMany(e => e.partners)
                .WithRequired(e => e.contacts)
                .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<ContactDbModel>()
                .HasMany(e => e._switch)
                .WithRequired(e => e.contacts)
                .HasForeignKey(e => e.ContactId);

            modelBuilder.Entity<CountyDbModel>()
                .Property(e => e.CountyName)
                .IsUnicode(false);

            modelBuilder.Entity<CountyDbModel>()
                .HasMany(e => e.partners)
                .WithOptional(e => e.county)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ParameterDbModel>()
                .Property(e => e.ParameterName)
                .IsUnicode(false);

            modelBuilder.Entity<ParameterDbModel>()
                .Property(e => e.ParameterValue)
                .IsUnicode(false);

            modelBuilder.Entity<ParameterDbModel>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<PartnerDbModel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PartnerDbModel>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<PartnerDbModel>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<PartnerDbModel>()
                .HasMany(e => e._switch)
                .WithRequired(e => e.partners)
                .HasForeignKey(e => e.PartnerId);

            modelBuilder.Entity<PartnerTypeDbModel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PartnerTypeDbModel>()
                .HasMany(e => e.partners)
                .WithOptional(e => e.partnertype)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<UserDbModel>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserDbModel>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
