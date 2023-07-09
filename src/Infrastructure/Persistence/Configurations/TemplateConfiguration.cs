using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable(nameof(Template)).IsMultiTenant();
        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();

        builder.Property(x => x.Subject)
            .HasColumnName("Subject")
            .HasColumnType("nvarchar(255)")
            .IsRequired()
            .HasComment("Subject of the email.");

        builder.Property(x => x.Body)
            .HasColumnName("Body")
            .HasColumnType("nvarchar(MAX)")
            .HasComment("Body of the email.");

        builder.Property(x => x.CreatedBy)
        .HasColumnName("CreatedBy")
        .HasColumnType("uniqueidentifier")
        .IsRequired()
        .HasComment("User that creates an event.");

        builder.Property(x => x.CreatedOn)
       .HasColumnName("CreatedOn")
       .HasColumnType("datetime")
       .IsRequired()
       .HasComment("Date when event is created.");

        builder.Property(x => x.StatusId)
         .HasColumnName("StatusId")
         .HasColumnType("int")
         .IsRequired()
         .HasComment("Status of the template.");



        builder.HasOne(x => x.User)
                      .WithMany(x => x.Templates)
                      .HasForeignKey(x => x.CreatedBy)
                      .OnDelete(DeleteBehavior.NoAction);





    }
}