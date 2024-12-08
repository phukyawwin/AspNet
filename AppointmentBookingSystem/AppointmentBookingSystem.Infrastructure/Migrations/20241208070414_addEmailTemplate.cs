using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEmailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Body", "CreatedAt", "Name", "Subject" },
                values: new object[] { 1, "Dear {{CustomerName}},<br>Your appointment is confirmed for {{AppointmentDate}} at {{SlotStartTime}} - {{SlotEndTime}} with Dr. {{DoctorName}}.", new DateTime(2024, 12, 8, 14, 4, 11, 839, DateTimeKind.Local).AddTicks(9253), "Appointment Confirmation", "Appointment Confirmation" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");
        }
    }
}
