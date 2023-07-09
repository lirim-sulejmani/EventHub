using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
public static class BaseEntityConfiguration
{
    public static ModelBuilder ApplyTenantQueryFilter(this ModelBuilder modelBuilder, BaseDbContext _context)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.GetProperties().Any(x => x.Name == "TenantId"))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "x");
                var contextExpr = Expression.Constant(null, typeof(BaseDbContext));
                var currentTenantIdMemberInfo = _context.GetType().GetMember(nameof(_context._tenantId)).FirstOrDefault();
                var tenantExpr = Expression.MakeMemberAccess(contextExpr, currentTenantIdMemberInfo);
                var body = Expression.Equal(Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(Guid) }, parameter, Expression.Constant("TenantId")), tenantExpr);
                var tenantFiletered = Expression.Lambda(Expression.OrElse(Expression.Equal(tenantExpr, Expression.Constant(Guid.Empty)), body), parameter);
                entityType.SetQueryFilter(tenantFiletered);
            }
        }
        return modelBuilder;
    }
    public static EntityTypeBuilder IsMultiTenant(this EntityTypeBuilder entityTypeBuilder)
    {
        entityTypeBuilder.Property<Guid>("TenantId").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnType("uniqueidentifier").IsRequired();
        var tableName = entityTypeBuilder.Metadata.GetTableName();
        entityTypeBuilder.HasIndex("TenantId").IsUnique(false).HasName($"IX_{tableName}_TenantId").HasDatabaseName($"IX_{tableName}_TenantId");
        return entityTypeBuilder;
    }
}