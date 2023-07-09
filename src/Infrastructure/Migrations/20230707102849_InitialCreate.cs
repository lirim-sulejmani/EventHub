using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carmax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "User",
                type: "nvarchar(MAX)",
                nullable: true,
                comment: "Name of the EventHub.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "Name of the social media."),
                    Website = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Website of the socila media."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "Name of the speakertype."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speaker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "First name of the speaker."),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "Last name of the speaker."),
                    Email = table.Column<string>(type: "nvarchar(150)", nullable: false, comment: "Email of the speaker."),
                    Organization = table.Column<string>(type: "nvarchar(255)", nullable: true, comment: "Name of the organization."),
                    Position = table.Column<string>(type: "nvarchar(100)", nullable: true, comment: "Position of the speaker."),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false, comment: "Phone number of the speaker."),
                    ProfileImage = table.Column<byte[]>(type: "Image", nullable: true, comment: "Bio of the speaker."),
                    Bio = table.Column<string>(type: "nvarchar(MAX)", nullable: true, comment: "Bio of the speaker."),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(MAX)", nullable: true, comment: "Website of the speaker."),
                    SocialMediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Social media of the speaker."),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "References to the Event table."),
                    SpeakerTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "References to the Speaker type table."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speaker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speaker_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Speaker_SocialMedia_SocialMediaId",
                        column: x => x.SocialMediaId,
                        principalTable: "SocialMedia",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Speaker_SpeakerType_SpeakerTypeId",
                        column: x => x.SpeakerTypeId,
                        principalTable: "SpeakerType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AgendaType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    Room = table.Column<string>(type: "nvarchar(100)", nullable: false, comment: "Location of the event."),
                    SpeakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "References to the Speaker table."),
                    BreakType = table.Column<string>(type: "nvarchar(50)", nullable: false, comment: "Types of the breaks."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaType_Speaker_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speaker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    AgendaTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "References to the Agendatype table."),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "References to the Agendatype table."),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Time when the agenda start."),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Time when the event in agenda end."),
                    Room = table.Column<string>(type: "nvarchar(255)", nullable: false, comment: "Location of the event."),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Who create of the agenda."),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Time when the agenda is created."),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, comment: "Time when the agenda is updated."),
                    SpeakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false, comment: "Status of the agenda."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agenda_AgendaType_AgendaTypeId",
                        column: x => x.AgendaTypeId,
                        principalTable: "AgendaType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Agenda_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agenda_Speaker_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speaker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Agenda_User_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AgendasSpeaker",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "The unique identifier of a record. It is generated automatically by the database when the record is added."),
                    SpeakerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "References to the Speaker table."),
                    AgendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "References to the Agenda table."),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendasSpeaker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendasSpeaker_Agenda_AgendaId",
                        column: x => x.AgendaId,
                        principalTable: "Agenda",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AgendasSpeaker_Speaker_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speaker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "Agenda",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_AgendaTypeId",
                table: "Agenda",
                column: "AgendaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_CreatedBy",
                table: "Agenda",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_EventId",
                table: "Agenda",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_SpeakerId",
                table: "Agenda",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_TenantId",
                table: "Agenda",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "AgendasSpeaker",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AgendasSpeaker_AgendaId",
                table: "AgendasSpeaker",
                column: "AgendaId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendasSpeaker_SpeakerId",
                table: "AgendasSpeaker",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendasSpeaker_TenantId",
                table: "AgendasSpeaker",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "AgendaType",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaType_SpeakerId",
                table: "AgendaType",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaType_TenantId",
                table: "AgendaType",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "SocialMedia",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_TenantId",
                table: "SocialMedia",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "Speaker",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Speaker_EventId",
                table: "Speaker",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Speaker_SocialMediaId",
                table: "Speaker",
                column: "SocialMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Speaker_SpeakerTypeId",
                table: "Speaker",
                column: "SpeakerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Speaker_TenantId",
                table: "Speaker",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "EventHub1",
                table: "SpeakerType",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerType_TenantId",
                table: "SpeakerType",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendasSpeaker");

            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropTable(
                name: "AgendaType");

            migrationBuilder.DropTable(
                name: "Speaker");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "SpeakerType");

            migrationBuilder.AlterColumn<string>(
                name: "TenantName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(MAX)",
                oldNullable: true,
                oldComment: "Name of the EventHub.");
        }
    }
}
