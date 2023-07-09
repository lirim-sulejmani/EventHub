using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class AgendaTypeConfiguration : IEntityTypeConfiguration<AgendaType>
{
    public void Configure(EntityTypeBuilder<AgendaType> builder)
    {

        builder.ToTable(nameof(AgendaType)).IsMultiTenant();

        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();



        builder.Property(x => x.Title)
          .HasColumnName("Title")
          .HasColumnType("nvarchar(100)")
          .HasComment("Location of the event.");

        builder.Property(x => x.SpeakerId)
            .HasColumnName("SpeakerId")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("References to the Speaker table.");


        builder.Property(x => x.BreakType)
           .HasColumnName("BreakType")
           .HasColumnType("nvarchar(50)")
           .IsRequired()
           .HasComment("Types of the breaks.");


        builder.HasOne(x => x.Speakers)
                .WithMany(x => x.AgendaTypes)
                .HasForeignKey(x => x.SpeakerId)
                .OnDelete(DeleteBehavior.NoAction);


        
    }
}