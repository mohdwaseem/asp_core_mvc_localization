using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CultureDemo.Models
{
    public partial class CultureDemoContext : DbContext
    {
        public CultureDemoContext()
        {
        }

        public CultureDemoContext(DbContextOptions<CultureDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authority> Authority { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<ContractService> ContractService { get; set; }
        public virtual DbSet<Service> Service { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Authority>(entity =>
            {
                entity.Property(e => e.AuthorityName).HasMaxLength(50);
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.Property(e => e.BankName).HasMaxLength(50);
            });

            modelBuilder.Entity<ContractService>(entity =>
            {
                entity.HasKey(e => e.ContracServiceId);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Authority)
                    .WithMany(p => p.ContractService)
                    .HasForeignKey(d => d.AuthorityId)
                    .HasConstraintName("FK_ContractService_Authority");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.ContractService)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("FK_ContractService_Bank");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ContractService)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ContractService_Service");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.ServiceName).HasMaxLength(50);
            });
        }
    }
}
