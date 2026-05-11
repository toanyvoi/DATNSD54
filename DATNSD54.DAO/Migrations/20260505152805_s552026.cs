using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATNSD54.DAO.Migrations
{
    /// <inheritdoc />
    public partial class s552026 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItem_ProductDetail_ProductDetailId",
                table: "BillItem");

            migrationBuilder.DropIndex(
                name: "IX_BillItem_ProductDetailId",
                table: "BillItem");

            migrationBuilder.DropColumn(
                name: "ProductDetailId",
                table: "BillItem");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(3121));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(2926));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(2928));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(3062));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(3181));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(3183));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(3031));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(3036));

            migrationBuilder.UpdateData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(2891));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(2960));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(2962));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 5, 5, 22, 28, 4, 115, DateTimeKind.Local).AddTicks(2605));

            migrationBuilder.CreateIndex(
                name: "IX_BillItem_Product_Detail_ID",
                table: "BillItem",
                column: "Product_Detail_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItem_ProductDetail_Product_Detail_ID",
                table: "BillItem",
                column: "Product_Detail_ID",
                principalTable: "ProductDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItem_ProductDetail_Product_Detail_ID",
                table: "BillItem");

            migrationBuilder.DropIndex(
                name: "IX_BillItem_Product_Detail_ID",
                table: "BillItem");

            migrationBuilder.AddColumn<int>(
                name: "ProductDetailId",
                table: "BillItem",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3948));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3520));

            migrationBuilder.UpdateData(
                table: "Color",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3523));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3841));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(4032));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "ID",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(4036));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3659));

            migrationBuilder.UpdateData(
                table: "ProductDetail",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3665));

            migrationBuilder.UpdateData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3468));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3562));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3566));

            migrationBuilder.UpdateData(
                table: "Size",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(3568));

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ngay_Tao",
                value: new DateTime(2026, 4, 11, 18, 3, 10, 404, DateTimeKind.Local).AddTicks(2847));

            migrationBuilder.CreateIndex(
                name: "IX_BillItem_ProductDetailId",
                table: "BillItem",
                column: "ProductDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItem_ProductDetail_ProductDetailId",
                table: "BillItem",
                column: "ProductDetailId",
                principalTable: "ProductDetail",
                principalColumn: "Id");
        }
    }
}
