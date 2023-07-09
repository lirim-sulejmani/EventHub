using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class SMTPConfigConfiguration : IEntityTypeConfiguration<SMTPConfig>
{
    public void Configure(EntityTypeBuilder<SMTPConfig> builder)
    {
        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();

        builder.Property(x => x.SMTPHost)
            .HasColumnName("SMTPHost")
            .HasColumnType("nvarchar(255)")
            .IsRequired()
            .HasComment("SMTP Host.");

        builder.Property(x => x.SMTPPort)
            .HasColumnName("SMTPPort")
            .HasColumnType("int")
            .HasComment("SMTP Port.");

        builder.Property(x => x.SMTPAuthentication)
         .HasColumnName("SMTPAuthentication")
         .HasColumnType("nvarchar(255)")
         .IsRequired()
         .HasComment("Authentication of SMTP.");

        builder.Property(x => x.Username)
        .HasColumnName("Username")
        .HasColumnType("nvarchar(255)")
        .IsRequired()
        .HasComment("Username.");


        builder.Property(x => x.Password)
        .HasColumnName("Password")
        .HasColumnType("nvarchar(255)")
        .IsRequired()
        .HasComment("Password.");

        builder.Property(x => x.IsTLS)
        .HasColumnName("IsTLS")
        .HasColumnType("bit")
        .IsRequired()
        .HasComment("If is TLS will be true otherwise false.");


        builder.Property(x => x.MessageFrom)
        .HasColumnName("MessageFrom")
        .HasColumnType("nvarchar(255)")
        .IsRequired()
        .HasComment("This field shows who sends message.");



        builder.Property(x => x.Description)
       .HasColumnName("Description")
       .HasColumnType("nvarchar(MAX)")
       .HasComment("A short description for smtp.");

        builder.Property(x => x.CreatedOn)
       .HasColumnName("CreatedOn")
       .HasColumnType("datetime")
       .IsRequired()
       .HasComment("Date when SMTP is create.");
    }
}