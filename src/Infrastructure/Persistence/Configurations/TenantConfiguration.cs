using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using static IdentityModel.OidcConstants;
using Carmax.Domain.Entities;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(255)")
            .IsRequired()
            .HasComment("Name of the event hub.");

        builder.Property(x => x.StatusId)
            .HasColumnName("StatusId")
            .HasColumnType("int")
            .HasComment("Status of the event.");


        builder.Property(x => x.CreatedOn)
       .HasColumnName("CreatedOn")
       .HasColumnType("datetime")
       .HasComment("Venue of the event.");


    }
}