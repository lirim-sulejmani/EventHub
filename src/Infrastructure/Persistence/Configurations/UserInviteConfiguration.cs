using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Carmax.Infrastructure.Persistence.Configurations;
internal class UserInviteConfiguration : IEntityTypeConfiguration<UserInvite>
{
    public void Configure(EntityTypeBuilder<UserInvite> builder)
    {
        builder.ToTable(nameof(UserInvite)).IsMultiTenant();
        builder.HasIndex(x => x.Id).IsUnique(false).HasDatabaseName("EventHub1");
        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasComment("The unique identifier of a record. It is generated automatically by the database when the record is added.")
            .ValueGeneratedNever();

        builder.Property(x => x.UserId)
          .HasColumnName("UserId")
          .HasColumnType("uniqueidentifier")
          .HasComment("Id of the guest.");


        builder.Property(x => x.CreatedOn)
       .HasColumnName("CreatedOn")
       .HasColumnType("datetime")
       .IsRequired()
       .HasComment("Date when event is created.");



        builder.Property(x => x.StatusId)
            .HasColumnName("StatusId")
            .HasColumnType("int")
            .IsRequired()
            .HasComment("Status id of the guest.");

        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("nvarchar(150)")
            .IsRequired()
            .HasComment("Status id of the guest.");

       

        builder.HasOne(x => x.User)
                        .WithMany(x => x.UserInvite)
                        .HasForeignKey(x => x.UserId)
                        .OnDelete(DeleteBehavior.NoAction);

    }
}