using InvoiceApp.API.Entities;
using InvoiceApp.API.Entities.Commons;
using InvoiceApp.API.Entities.Indenties;
using InvoiceApp.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.API.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IClaimService? _claimService;
        public AppDbContext(DbContextOptions<AppDbContext> options, IClaimService? claimService) : base(options)
        {
            _claimService = claimService;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var userId = _claimService.GetUserId() ?? "ByServer";

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
