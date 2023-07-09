using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Carmax.Domain.Entities;
using static Duende.IdentityServer.Models.IdentityResources;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {

        builder.ToTable(nameof(Event)).IsMultiTenant();

        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();

        builder.Property(x => x.EventName)
            .HasColumnName("EventName")
            .HasColumnType("nvarchar(255)")
            .IsRequired()
            .HasComment("Name of the event.");

        builder.Property(x => x.StartTime)
            .HasColumnName("StartTime")
            .HasColumnType("datetime")
            .IsRequired()
            .HasComment("Start time of the event.");

        builder.Property(x => x.EndTime)
            .HasColumnName("EndTime")
            .HasColumnType("datetime")
            .IsRequired()
            .HasComment("End time of the event.");

        builder.Property(x => x.StatusId)
           .HasColumnName("StatusId")
           .HasColumnType("int")
           .HasComment("Status id of the event.");

        builder.Property(x => x.EventManager)
           .HasColumnName("EventManager")
           .HasColumnType("nvarchar(255)")
           .IsRequired()
           .HasComment("Name of the event manager.");

        builder.Property(x => x.EventVenue)
           .HasColumnName("EventVenue")
           .HasColumnType("nvarchar(255)")
           .IsRequired()
           .HasComment("Venue of the event.");


        builder.Property(x => x.Organizer)
        .HasColumnName("Organizer")
        .HasColumnType("nvarchar(255)")
        .IsRequired()
        .HasComment("Name of the event organizer.");

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

        builder.Property(x => x.City)
        .HasColumnName("City")
        .HasColumnType("nvarchar(255)")
        .IsRequired()
        .HasComment("City where the event is.");

        builder.Property(x => x.Address)
       .HasColumnName("Address")
       .HasColumnType("nvarchar(255)")
       .IsRequired()
       .HasComment("Address where the event is.");

        builder.Property(x => x.CountryId)
       .HasColumnName("CountryId")
       .HasColumnType("uniqueidentifier")
       .IsRequired()
       .HasComment("Id of the country from Country table.");

        builder.Property(x => x.ZipCode)
     .HasColumnName("ZipCode")
     .HasColumnType("int")
     .IsRequired()
     .HasComment("Zip Code of the country.");


        builder.HasOne(x => x.User)
                 .WithMany(x => x.Events)
                 .HasForeignKey(x => x.CreatedBy)
                 .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Country)
               .WithMany(x => x.Events)
               .HasForeignKey(x => x.CountryId)
               .OnDelete(DeleteBehavior.NoAction);

    }
}