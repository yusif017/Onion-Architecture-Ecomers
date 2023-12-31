﻿using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using File = Ecommerce.Domain.Entities.File;


namespace Ecommerce.Persistence.Contexts
{
    public class EcommerceAPIDbContext : DbContext
    {
        public EcommerceAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        #region DbSet
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        #endregion


        #region CreateDate & UpdateDate
        public override int SaveChanges()
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // ChandeTracker => Entryiler  uzerinde  edilen    Deyisiklikleri  ya da yeni  elave  olunmus  obyektleri  tapmaq  ucun  istifade  olunur
            ChangeTracker.DetectChanges();
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion


    }
}