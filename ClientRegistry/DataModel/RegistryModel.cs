namespace ClientRegistry
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

        public virtual DbSet<Contact> contacts { get; set; }
        public virtual DbSet<County> county { get; set; }
        public virtual DbSet<Parameter> parameters { get; set; }
        public virtual DbSet<Partner> partners { get; set; }
        public virtual DbSet<PartnerType> partnertype { get; set; }
        public virtual DbSet<Switch> _switch { get; set; }
        public virtual DbSet<User> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e.partners)
                .WithRequired(e => e.contacts)
                .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<Contact>()
                .HasMany(e => e._switch)
                .WithRequired(e => e.contacts)
                .HasForeignKey(e => e.ContactId);

            modelBuilder.Entity<County>()
                .Property(e => e.CountyName)
                .IsUnicode(false);

            modelBuilder.Entity<County>()
                .HasMany(e => e.partners)
                .WithOptional(e => e.county)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Parameter>()
                .Property(e => e.ParameterName)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.ParameterValue)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Partner>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Partner>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Partner>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Partner>()
                .HasMany(e => e._switch)
                .WithRequired(e => e.partners)
                .HasForeignKey(e => e.PartnerId);

            modelBuilder.Entity<PartnerType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PartnerType>()
                .HasMany(e => e.partners)
                .WithOptional(e => e.partnertype)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
