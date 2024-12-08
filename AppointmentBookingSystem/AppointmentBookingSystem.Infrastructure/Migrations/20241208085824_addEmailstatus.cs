using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEmailstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSendReminderNoti",
                table: "Booking",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 15, 58, 23, 811, DateTimeKind.Local).AddTicks(4944));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 15, 58, 23, 812, DateTimeKind.Local).AddTicks(3482));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 15, 58, 23, 812, DateTimeKind.Local).AddTicks(3494));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSendReminderNoti",
                table: "Booking");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 14, 59, 20, 620, DateTimeKind.Local).AddTicks(3634));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 14, 59, 20, 621, DateTimeKind.Local).AddTicks(1060));

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 8, 14, 59, 20, 621, DateTimeKind.Local).AddTicks(1068));
        }
    }
}
