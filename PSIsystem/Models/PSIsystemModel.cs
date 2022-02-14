using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PSIsystem.Models
{
    public partial class PSIsystemModel : DbContext
    {
        public PSIsystemModel()
            : base("name=PSIsystemModel")
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .Property(e => e.Asset1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Purchase>()
                .Property(e => e.ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Purchase>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<Purchase>()
                .Property(e => e.Total)
                .HasPrecision(18, 0);

            modelBuilder.Entity<User>()
                .Property(e => e.Account)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.PWD)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsFixedLength();
        }
    }
}
