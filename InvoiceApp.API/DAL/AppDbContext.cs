using InvoiceApp.API.Entities;
using InvoiceApp.API.Entities.Commons;
using InvoiceApp.API.Entities.Indenties;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.API.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var userId = "95246197-eac5-4fe5-9fd6-105e5826e677";

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = userId;
                        entry.Entity.RecordDate = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
