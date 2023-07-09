using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Carmax.Domain.Entities;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.ToTable(nameof(Invitation)).IsMultiTenant();
        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();

        builder.Property(x => x.FullName)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(255)")
            .HasComment("Name of the guest");


        builder.Property(x => x.Job)
            .HasColumnName("Job")
            .HasColumnType("nvarchar(255)")
            .HasComment("User's invented job.");

        builder.Property(x => x.Institution)
            .HasColumnName("Institution")
            .HasColumnType("nvarchar(255)")
            .HasComment("Institution of work.");


        builder.Property(x => x.NominatedBy)
       .HasColumnName("NominatedBy")
       .HasColumnType("nvarchar(255)")
       .HasComment("Person who has nomited to be the guest.");

        builder.Property(x => x.Vip)
            .HasColumnName("Vip")
            .HasColumnType("bit")
            .HasComment("Is VIP true/false");


        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("nvarchar(100)")
            .HasComment("Email of the guest.");


        builder.Property(x => x.PhoneNumber)
       .HasColumnName("PhoneNumber")
       .HasColumnType("nvarchar(20)")
       .HasComment("Phone number of the guest.");

        builder.Property(x => x.Website)
       .HasColumnName("Website")
       .HasColumnType("nvarchar(255)")
       .HasComment("Website, it can be null.");

        builder.Property(x => x.StatusId)
       .HasColumnName("StatusId")
       .HasColumnType("int")
       .HasComment("Status id of the guests.");

        builder.Property(x => x.QRCode)
       .HasColumnName("QRCode")
       .HasColumnType("varbinary(MAX)")
       .HasComment("QR code is a unique string that is generated for every guest.");

        builder.Property(x => x.NoGuests)
     .HasColumnName("NoGuests")
     .HasColumnType("int")
     .HasComment("Number of guests invented.");


        builder.Property(x => x.GeneratedCode)
     .HasColumnName("GeneratedCode")
     .HasColumnType("nvarchar(255)")
     .HasComment("A random code generated.");


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

        builder.Property(x => x.TemplateId)
       .HasColumnName("TemplateId")
       .HasColumnType("uniqueidentifier")
       .HasComment("this Id redirect to the template table in database.");


        builder.Property(x => x.SendEmailError)
       .HasColumnName("SendEmailError")
       .HasColumnType("nvarchar(255)")
       .HasComment("This message send when an error occurs.");


        builder.Property(x => x.BarcodeScanned)
       .HasColumnName("BarcodeScanned")
       .HasColumnType("bit")
       .HasComment("This bar code is scanned when user comes in event.");

        builder.Property(x => x.DateScanned)
      .HasColumnName("DateScanned")
      .HasColumnType("datetime")
      .HasComment("Time when QRcode is scann in event.");

      


        builder.HasOne(x => x.User)
                        .WithMany(x => x.Invitations)
                        .HasForeignKey(x => x.CreatedBy)
                        .OnDelete(DeleteBehavior.NoAction);

       


        builder.HasOne(x => x.Template)
                       .WithMany(x => x.Invitations)
                       .HasForeignKey(x => x.TemplateId)
                       .OnDelete(DeleteBehavior.NoAction);
    }
}