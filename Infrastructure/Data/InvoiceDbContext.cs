using GestorFacturas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Infrastructure.Data
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options)
           : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<InvoicePayment> InvoicePayments { get; set; }
        public DbSet<InvoiceCreditNote> InvoiceCreditNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerRun);

            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.InvoiceNumber);

            modelBuilder.Entity<InvoicePayment>()
                .HasKey(ip => ip.InvoiceNumber);

            modelBuilder.Entity<InvoiceDetail>()
                .HasKey(id => id.InvoiceNumber);

            modelBuilder.Entity<InvoiceCreditNote>()
                .HasKey(id => id.InvoiceNumber);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerRun);

            modelBuilder.Entity<InvoiceDetail>()
                .HasOne(id => id.Invoice)
                .WithMany(i => i.InvoiceDetails)
                .HasForeignKey(id => id.InvoiceNumber);

            modelBuilder.Entity<InvoicePayment>()
                .HasOne(ip => ip.Invoice)
                .WithOne(i => i.InvoicePayment)
                .HasForeignKey<InvoicePayment>(ip => ip.InvoiceNumber);

            modelBuilder.Entity<InvoiceCreditNote>()
                .HasOne(icn => icn.Invoice)
                .WithMany(i => i.InvoiceCreditNotes)
                .HasForeignKey(icn => icn.InvoiceNumber);
        }
    }
}
