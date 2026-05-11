using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATNSD54.DAO.Migrations
{
    /// <inheritdoc />
    public partial class updateshipDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quanly",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phiShip = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quanly", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(3125));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2905));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2907));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(3018));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(3151));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2992));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2995));

            migrationBuilder.UpdateData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2873));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2932));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2934));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2936));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 10, 37, 439, DateTimeKind.Local).AddTicks(2646));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quanly");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4569));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4264));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4471));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4620));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4624));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4427));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4433));

            migrationBuilder.UpdateData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4210));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4319));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4324));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 3, 29, 13, 6, 53, 420, DateTimeKind.Local).AddTicks(3791));
        }
    }
}
