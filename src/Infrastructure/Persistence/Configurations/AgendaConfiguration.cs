using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class AgendaConfiguration : IEntityTypeConfiguration<Agenda>
{
    public void Configure(EntityTypeBuilder<Agenda> builder)
    {

        builder.ToTable(nameof(Agenda)).IsMultiTenant();

        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();



        builder.Property(x => x.AgendaTypeId)
            .HasColumnName("AgendaTypeId")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("References to the Agendatype table.");


        builder.Property(x => x.EventId)
            .HasColumnName("EventId")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("References to the Agendatype table.");

        builder.Property(x => x.StartTime)
            .HasColumnName("StartTime")
            .HasColumnType("datetime")
            .HasComment("Time when the agenda start.");


        builder.Property(x => x.EndTime)
            .HasColumnName("EndTime")
            .HasColumnType("datetime")
            .HasComment("Time when the event in agenda end.");


        builder.Property(x => x.Room)
           .HasColumnName("Room")
           .HasColumnType("nvarchar(255)")
           .IsRequired()
           .HasComment("Location of the event.");

        builder.Property(x => x.CreatedBy)
           .HasColumnName("CreatedBy")
           .HasColumnType("uniqueidentifier")
           .IsRequired()
           .HasComment("References to the User table.");

        builder.Property(x => x.CreatedBy)
          .HasColumnName("CreatedBy")
          .HasColumnType("uniqueidentifier")
          .IsRequired()
          .HasComment("Who create of the agenda.");


        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .IsRequired()
            .HasComment("Time when the agenda is created.");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime")
            .IsRequired()
            .HasComment("Time when the agenda is updated.");


        builder.Property(x => x.StatusId)
           .HasColumnName("StatusId")
           .HasColumnType("int")
           .IsRequired()
           .HasComment("Status of the agenda.");


        builder.HasOne(x => x.AgendaTypes)
                .WithMany(x => x.Agenda)
                .HasForeignKey(x => x.AgendaTypeId)
                .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(x => x.Users)
                .WithMany(x => x.Agenda)
                .HasForeignKey(x => x.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Speakers)
               .WithMany(x => x.Agenda)
               .HasForeignKey(x => x.SpeakerId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}