using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {

        builder.ToTable(nameof(Speaker)).IsMultiTenant();

        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();

        builder.Property(x => x.FirstName)
            .HasColumnName("FirstName")
            .HasColumnType("nvarchar(100)")
            .IsRequired()
            .HasComment("First name of the speaker.");

        builder.Property(x => x.LastName)
                    .HasColumnName("LastName")
                    .HasColumnType("nvarchar(100)")
                    .IsRequired()
                    .HasComment("Last name of the speaker.");

        builder.Property(x => x.Organization)
                    .HasColumnName("Organization")
                    .HasColumnType("nvarchar(255)")
                    .HasComment("Name of the organization.");


        builder.Property(x => x.Position)
                    .HasColumnName("Position")
                    .HasColumnType("nvarchar(100)")
                    .HasComment("Position of the speaker.");


        builder.Property(x => x.Email)
                    .HasColumnName("Email")
                    .HasColumnType("nvarchar(150)")
                    .IsRequired()
                    .HasComment("Email of the speaker.");


        builder.Property(x => x.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasColumnType("nvarchar(20)")
                    .IsRequired()
                    .HasComment("Phone number of the speaker.");

        builder.Property(x => x.Bio)
                    .HasColumnName("Bio")
                    .HasColumnType("nvarchar(MAX)")
                    .HasComment("Bio of the speaker.");

        builder.Property(x => x.ProfileImage)
                    .HasColumnName("ProfileImage")
                    .HasColumnType("Image")
                    .HasComment("Bio of the speaker.");

        builder.Property(x => x.WebsiteUrl)
                   .HasColumnName("WebsiteUrl")
                   .HasColumnType("nvarchar(MAX)")
                   .HasComment("Website of the speaker.");

        builder.Property(x => x.SocialMediaId)
                   .HasColumnName("SocialMediaId")
                   .HasColumnType("uniqueidentifier")
                   .IsRequired()
                   .HasComment("Social media of the speaker.");


        builder.Property(x => x.EventId)
            .HasColumnName("EventId")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("References to the Event table.");


        builder.Property(x => x.SpeakerTypeId)
            .HasColumnName("SpeakerTypeId")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("References to the Speaker type table.");

        builder.HasOne(x => x.Events)
               .WithMany(x => x.Speakers)
               .HasForeignKey(x => x.EventId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SocilaMedias)
               .WithMany(x => x.Speakers)
               .HasForeignKey(x => x.SocialMediaId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SpeakerTypes)
               .WithMany(x => x.Speakers)
               .HasForeignKey(x => x.SpeakerTypeId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}