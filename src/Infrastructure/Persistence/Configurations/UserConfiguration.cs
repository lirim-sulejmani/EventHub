using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User)).IsMultiTenant();
        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();

        builder.Property(x => x.FirstName)
          .HasColumnName("FirstName")
          .HasColumnType("nvarchar(255)")
            .IsRequired()
          .HasComment("First name of the user.");


        builder.Property(x => x.LastName)
       .HasColumnName("LastName")
       .HasColumnType("nvarchar(255)")
            .IsRequired()
       .HasComment("Last name of the user.");

        builder.Property(x => x.Email)
       .HasColumnName("Email")
       .HasColumnType("nvarchar(150)")
            .IsRequired()
       .HasComment("Email of the user.");

        builder.Property(x => x.Address)
            .HasColumnName("Address")
            .HasColumnType("nvarchar(255)")
            .IsRequired()
            .HasComment("Status id of the guest.");

        builder.Property(x => x.City)
            .HasColumnName("City")
            .HasColumnType("nvarchar(100)")
            .IsRequired()
            .HasComment("City of the user.");

        builder.Property(x => x.CreatedOn)
            .HasColumnName("CreatedOn")
            .HasColumnType("datetime")
            .IsRequired()
            .HasComment("Date when user is create.");


        builder.Property(x => x.StatusId)
           .HasColumnName("StatusId")
           .HasColumnType("int")
           .IsRequired()
           .HasComment("Status id of the user.");

        builder.Property(x => x.RefreshToken)
           .HasColumnName("RefreshToken")
           .HasColumnType("nvarchar(MAX)")
           .HasComment("Refresh Token.");


        builder.Property(x => x.RefreshTokenExpiryTime)
        .HasColumnName("RefreshTokenExpiryTime")
        .HasColumnType("datetime")
        .HasComment("Refresh Token Expiry Time.");

        builder.Property(x => x.RoleId)
        .HasColumnName("RoleId")
        .HasColumnType("int")
        .IsRequired()
        .HasComment("Role id.");


        builder.Property(x => x.Password)
       .HasColumnName("Password")
       .HasColumnType("varbinary(max)")
       .IsRequired()
       .HasComment("Password of user.");

        builder.Property(x => x.ConfirmPassword)
      .HasColumnName("ConfirmPassword")
      .HasColumnType("varbinary(max)")
      .IsRequired()
      .HasComment("Confirm Password of user.");

        builder.Property(x => x.Salt)
      .HasColumnName("Salt")
      .HasColumnType("varbinary(max)")
      .IsRequired()
      .HasComment("Salt value.");

        builder.Property(x => x.MaxOpenBids)
      .HasColumnName("MaxOpenBids")
      .HasColumnType("int")
      .HasComment("Max of open bids.");

        builder.Property(x => x.PhoneNumber)
         .HasColumnName("PhoneNumber")
         .HasColumnType("nvarchar(255)")
           .IsRequired()
         .HasComment("Phone Number of the user.");

        builder.Property(x => x.ForgotPaswordTokenExpire)
    .HasColumnName("ForgotPaswordTokenExpire")
    .HasColumnType("datetime")
    .HasComment("Time when forgot password token will expire.");

        builder.Property(x => x.ForgotPasswordToken)
       .HasColumnName("ForgotPasswordToken")
       .HasColumnType("nvarchar(MAX)")
       .HasComment("Token of forgot password.");

        builder.Property(x => x.TenantName)
      .HasColumnName("TenantName")
      .HasColumnType("nvarchar(MAX)")
      .HasComment("Name of the EventHub.");

    }
}