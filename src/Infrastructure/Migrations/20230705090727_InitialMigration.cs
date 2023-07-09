using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carmax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    CountryName = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Name of the event."),
                    CountryCode = table.Column<int>(type: "int", nullable: false, comment: "Country code."),
                    Continent = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "Continent where the country is."),
                    Capital = table.Column<string>(type: "nvarchar(150)", nullable: false, comment: "Capital city of the country."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SMTPConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    SMTPHost = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "SMTP Host."),
                    SMTPPort = table.Column<int>(type: "int", nullable: false, comment: "SMTP Port."),
                    SMTPAuthentication = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Authentication of SMTP."),
                    Username = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Username."),
                    Password = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Password."),
                    IsTLS = table.Column<bool>(type: "bit", nullable: false, comment: "If is TLS will be true otherwise false."),
                    MessageFrom = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "This field shows who sends message."),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: false, comment: "A short description for smtp."),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date when SMTP is create.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTPConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Name of the event hub."),
                    StatusId = table.Column<int>(type: "int", nullable: false, comment: "Status of the event."),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Venue of the event.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    FirstName = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "First name of the user."),
                    LastName = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Last name of the user."),
                    Email = table.Column<string>(type: "nvarchar(150)", nullable: false, comment: "Email of the user."),
                    Address = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Status id of the guest."),
                    City = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "City of the user."),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date when user is create."),
                    StatusId = table.Column<int>(type: "int", nullable: false, comment: "Status id of the user."),
                    RefreshToken = table.Column<string>(type: "nvarchar(MAX)", nullable: true, comment: "Refresh Token."),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Refresh Token Expiry Time."),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Password of user."),
                    ConfirmPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Confirm Password of user."),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false, comment: "Salt value."),
                    MaxOpenBids = table.Column<int>(type: "int", nullable: true, comment: "Max of open bids."),
                    PhoneNumber = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Phone Number of the user."),
                    ForgotPaswordTokenExpire = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Time when forgot password token will expire."),
                    ForgotPasswordToken = table.Column<string>(type: "nvarchar(MAX)", nullable: true, comment: "Token of forgot password."),
                    RoleId = table.Column<int>(type: "int", nullable: false, comment: "Role id."),
                    TenantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    EventName = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Name of the event."),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Start time of the event."),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "End time of the event."),
                    StatusId = table.Column<int>(type: "int", nullable: false, comment: "Status id of the event."),
                    EventManager = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Name of the event manager."),
                    Organizer = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Name of the event organizer."),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "User that creates an event."),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date when event is created."),
                    EventVenue = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Venue of the event."),
                    City = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "City where the event is."),
                    Address = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Address where the event is."),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the country from Country table."),
                    ZipCode = table.Column<int>(type: "int", nullable: false, comment: "Zip Code of the country."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Event_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    Subject = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Subject of the email."),
                    Body = table.Column<string>(type: "nvarchar(MAX)", nullable: false, comment: "Body of the email."),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "User that creates an event."),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date when event is created."),
                    StatusId = table.Column<int>(type: "int", nullable: false, comment: "Status of the template."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Template_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Template_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserInvite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id of the guest."),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date when event is created."),
                    StatusId = table.Column<int>(type: "int", nullable: false, comment: "Status id of the guest."),
                    Email = table.Column<string>(type: "nvarchar(150)", nullable: false, comment: "Status id of the guest."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInvite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInvite_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInvite_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Name of the guest"),
                    Job = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "User's invented job."),
                    Institution = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Institution of work."),
                    NominatedBy = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Person who has nomited to be the guest."),
                    Vip = table.Column<bool>(type: "bit", nullable: false, comment: "Is VIP true/false"),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "Email of the guest."),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false, comment: "Phone number of the guest."),
                    Website = table.Column<string>(type: "nvarchar(255)", nullable: true, comment: "Website, it can be null."),
                    StatusId = table.Column<int>(type: "int", nullable: false, comment: "Status id of the guests."),
                    QRCode = table.Column<byte[]>(type: "varbinary(MAX)", nullable: false, comment: "QR code is a unique string that is generated for every guest."),
                    NoGuests = table.Column<int>(type: "int", nullable: true, comment: "Number of guests invented."),
                    GeneratedCode = table.Column<string>(type: "nvarchar(255)", nullable: true, comment: "A random code generated."),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "User that creates an event."),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Date when event is created."),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "this Id redirect to the template table in database."),
                    SendEmailError = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "This message send when an error occurs."),
                    BarcodeScanned = table.Column<bool>(type: "bit", nullable: false, comment: "This bar code is scanned when user comes in event."),
                    DateScanned = table.Column<DateTime>(type: "datetime", nullable: true, comment: "Time when QRcode is scann in event."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitation_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitation_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "Country",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Country_TenantId",
                table: "Country",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "Event",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CountryId",
                table: "Event",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CreatedBy",
                table: "Event",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Event_TenantId",
                table: "Event",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "Invitation",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_CreatedBy",
                table: "Invitation",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_TemplateId",
                table: "Invitation",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_TenantId",
                table: "Invitation",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "SMTPConfigs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "Template",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Template_CreatedBy",
                table: "Template",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Template_TenantId",
                table: "Template",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "Tenants",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId",
                table: "User",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "UserInvite",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvite_TenantId",
                table: "UserInvite",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvite_UserId",
                table: "UserInvite",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "SMTPConfigs");

            migrationBuilder.DropTable(
                name: "UserInvite");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
