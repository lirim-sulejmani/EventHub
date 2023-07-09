using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Interfaces;
using Carmax.Domain.Entities;
using Carmax.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;


namespace Carmax.Infrastructure.Persistence;
public class BaseDbContext : DbContext
{
    public readonly Tenant _tenant;
    public readonly Guid _tenantId;
    public readonly ITenantResolver _tenantResolver;
    public BaseDbContext(DbContextOptions<BaseDbContext> options, ITenantResolver tenantResolver) : base(options)
    {
        //_tenant = tenantResolver.GetCurrentTenant(Tenants);
        //if (_tenant != null)
        //    if (_tenant.ConnectionString is { } connectionString)
        //        Database.SetConnectionString(connectionString);
        _tenantResolver = tenantResolver;
    }
    public virtual DbSet<Tenant> Tenants { get; set; }
    public virtual int SaveChanges(int? userId = null)
    {
        OnBeforeSaveChanges(userId);
        return base.SaveChanges();
    }
    public virtual async Task<int> SaveChangesAsync(int? userId = null)
    {
        OnBeforeSaveChanges(userId);
        return await base.SaveChangesAsync();
    }

    public void OnBeforeSaveChanges(int? userId)
    {
        ChangeTracker.DetectChanges();
        var currentTenant = _tenantResolver.GetCurrentTenant(Tenants);
        if (currentTenant != null)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Tenant || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var tenantIdProp = entry.Property("TenantId");
                    if (tenantIdProp != null)
                    {
                        tenantIdProp.CurrentValue = currentTenant.Id;
                    }
                }
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyTenantQueryFilter(this);
    }
    public async Task SeedTenantData()
    {
        if (!Tenants.Any())
        {
            Tenants.Add(new Tenant()
            {
                Id = new Guid("1c8d5905-df62-4ad1-9730-eaa3fd80919a"),
                //ConnectionString = Database.GetConnectionString(),
                Name = "root",
                //Secret = "root"
            });
            await SaveChangesAsync();
        }
    }
}