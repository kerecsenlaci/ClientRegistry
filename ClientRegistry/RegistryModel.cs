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

        public virtual DbSet<contacts> contacts { get; set; }
        public virtual DbSet<county> county { get; set; }
        public virtual DbSet<parameters> parameters { get; set; }
        public virtual DbSet<partners> partners { get; set; }
        public virtual DbSet<partnertype> partnertype { get; set; }
        public virtual DbSet<_switch> _switch { get; set; }
        public virtual DbSet<users> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<contacts>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<contacts>()
                .HasMany(e => e.partners)
                .WithRequired(e => e.contacts)
                .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<contacts>()
                .HasMany(e => e._switch)
                .WithRequired(e => e.contacts)
                .HasForeignKey(e => e.ContactId);

            modelBuilder.Entity<county>()
                .Property(e => e.CountyName)
                .IsUnicode(false);

            modelBuilder.Entity<county>()
                .HasMany(e => e.partners)
                .WithOptional(e => e.county)
                .WillCascadeOnDelete();

            modelBuilder.Entity<parameters>()
                .Property(e => e.ParameterName)
                .IsUnicode(false);

            modelBuilder.Entity<parameters>()
                .Property(e => e.ParameterValue)
                .IsUnicode(false);

            modelBuilder.Entity<parameters>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<partners>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<partners>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<partners>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<partners>()
                .HasMany(e => e._switch)
                .WithRequired(e => e.partners)
                .HasForeignKey(e => e.PartnerId);

            modelBuilder.Entity<partnertype>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<partnertype>()
                .HasMany(e => e.partners)
                .WithOptional(e => e.partnertype)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<users>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<users>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
