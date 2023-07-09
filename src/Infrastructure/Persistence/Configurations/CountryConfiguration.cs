using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Carmax.Domain.Entities;

namespace Carmax.Infrastructure.Persistence.Configurations;
internal class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {

        builder.ToTable(nameof(Country)).IsMultiTenant();

        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();

        builder.Property(x => x.CountryName)
            .HasColumnName("CountryName")
            .HasColumnType("nvarchar(255)")
            .IsRequired()
            .HasComment("Name of the event.");

        builder.Property(x => x.CountryCode)
            .HasColumnName("CountryCode")
            .HasColumnType("int")
            .IsRequired()
            .HasComment("Country code.");

        builder.Property(x => x.Continent)
            .HasColumnName("Continent")
            .HasColumnType("nvarchar(100)")
            .IsRequired()
            .HasComment("Continent where the country is.");

        builder.Property(x => x.Capital)
           .HasColumnName("Capital")
           .HasColumnType("nvarchar(150)")
           .HasComment("Capital city of the country.");

      

    }
}