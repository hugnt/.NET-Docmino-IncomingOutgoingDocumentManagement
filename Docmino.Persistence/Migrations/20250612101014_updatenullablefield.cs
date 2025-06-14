using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docmino.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatenullablefield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ArrivalDate",
                table: "Document",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5054), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5059) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5081), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5082) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5103), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5104) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5111), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5112) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5118), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5119) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5129), new DateTime(2025, 6, 12, 10, 10, 12, 283, DateTimeKind.Utc).AddTicks(5130) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ArrivalDate",
                table: "Document",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1688), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1691) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1699), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1718), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1719) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1723), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1724) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1728), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1729) });

            migrationBuilder.UpdateData(
                table: "Group",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1736), new DateTime(2025, 6, 12, 8, 28, 28, 599, DateTimeKind.Utc).AddTicks(1736) });
        }
    }
}
