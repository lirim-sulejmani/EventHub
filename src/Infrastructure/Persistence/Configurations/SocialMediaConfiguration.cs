using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
{
    public void Configure(EntityTypeBuilder<SocialMedia> builder)
    {

        builder.ToTable(nameof(SocialMedia)).IsMultiTenant();

        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();


        builder.Property(x => x.Name)
           .HasColumnName("Name")
           .HasColumnType("nvarchar(50)")
           .IsRequired()
           .HasComment("Name of the social media.");

        builder.Property(x => x.Website)
           .HasColumnName("Website")
           .HasColumnType("nvarchar(255)")
           .IsRequired()
           .HasComment("Website of the socila media.");


        
    }
}