using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppointmentBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEmailTemplateForCancle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 14, 59, 20, 620, DateTimeKind.Local).AddTicks(3634));

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Body", "CreatedAt", "Name", "Subject" },
                values: new object[,]
                {
                    { 2, "Dear {{CustomerName}},<br>Your appointment on {{AppointmentDate}} at {{SlotStartTime}} - {{SlotEndTime}} with Dr. {{DoctorName}} has been cancelled. We apologize for any inconvenience.", new DateTime(2024, 12, 8, 14, 59, 20, 621, DateTimeKind.Local).AddTicks(1060), "Booking Cancellation", "Booking Cancellation Notification" },
                    { 3, "Dear {{CustomerName}},<br>This is a reminder that you have an appointment scheduled for {{AppointmentDate}} at {{SlotStartTime}} - {{SlotEndTime}} with Dr. {{DoctorName}}.<br>We look forward to seeing you.", new DateTime(2024, 12, 8, 14, 59, 20, 621, DateTimeKind.Local).AddTicks(1068), "Appointment Reminder", "Reminder: Your Appointment with Dr. {{DoctorName}}" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 14, 4, 11, 839, DateTimeKind.Local).AddTicks(9253));
        }
    }
}
