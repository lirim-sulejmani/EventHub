using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class AgendasSpeakerConfiguration : IEntityTypeConfiguration<AgendasSpeaker>
{
    public void Configure(EntityTypeBuilder<AgendasSpeaker> builder)
    {

        builder.ToTable(nameof(AgendasSpeaker)).IsMultiTenant();

        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();



       

        builder.Property(x => x.SpeakerId)
            .HasColumnName("SpeakerId")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("References to the Speaker table.");


        builder.Property(x => x.AgendaId)
            .HasColumnName("AgendaId")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("References to the Agenda table.");

        builder.HasOne(x => x.Speakers)
                .WithMany(x => x.AgendasSpeaker)
                .HasForeignKey(x => x.SpeakerId)
                .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Agenda)
                .WithMany(x => x.AgendasSpeakers)
                .HasForeignKey(x => x.AgendaId)
                .OnDelete(DeleteBehavior.NoAction);

    }
}