using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DATNSD54.DAO.Migrations
{
    /// <inheritdoc />
    public partial class s1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gioi_Tinh = table.Column<bool>(type: "bit", nullable: true),
                    Ngay_Sinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mat_Khau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trang_Thai = table.Column<bool>(type: "bit", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trang_Thai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "quanly",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phiShip = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quanly", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dia_Chi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trang_Thai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<bool>(type: "bit", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Anhdaidien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gioi_Tinh = table.Column<bool>(type: "bit", nullable: true),
                    Ngay_Sinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dia_Chi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mat_Khau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trang_Thai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VoucherShip",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ngay_Bat_Dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngay_Ket_Thuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Don_Hang_Toi_Thieu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    So_Luong = table.Column<int>(type: "int", nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trang_Thai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherShip", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vourcher",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ngay_Bat_Dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngay_Ket_Thuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ty_Le_Giam = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Don_Hang_Toi_Thieu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Giam_Toi_Da = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    So_Luong = table.Column<int>(type: "int", nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trang_Thai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vourcher", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChiChitiet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Customer_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_ID = table.Column<int>(type: "int", nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trang_Thai = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Carts_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Carts_Customer_Customer_ID",
                        column: x => x.Customer_ID,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nam_SX = table.Column<int>(type: "int", nullable: true),
                    Mo_Ta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_Type_ID = table.Column<int>(type: "int", nullable: false),
                    Supplier_ID = table.Column<int>(type: "int", nullable: false),
                    Brand_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand_Brand_ID",
                        column: x => x.Brand_ID,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_Product_Type_ID",
                        column: x => x.Product_Type_ID,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Supplier_Supplier_ID",
                        column: x => x.Supplier_ID,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: true),
                    Voucher_ID = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Discount_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShipCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VoucherShip_ID = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gia_Goc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Phuong_Thuc_Thanh_Toan = table.Column<int>(type: "int", nullable: false),
                    Thanh_Tien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Address_Id = table.Column<int>(type: "int", nullable: false),
                    Trang_Thai = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bill_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bill_Address_Address_Id",
                        column: x => x.Address_Id,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bill_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bill_Customer_Customer_ID",
                        column: x => x.Customer_ID,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bill_User_User_ID",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Bill_VoucherShip_VoucherShip_ID",
                        column: x => x.VoucherShip_ID,
                        principalTable: "VoucherShip",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Bill_Vourcher_Voucher_ID",
                        column: x => x.Voucher_ID,
                        principalTable: "Vourcher",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    IMG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trang_Thai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Image_Product_Product_ID",
                        column: x => x.Product_ID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Don_Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SL = table.Column<int>(type: "int", nullable: false),
                    Sale = table.Column<int>(type: "int", nullable: false),
                    Trang_Thai = table.Column<bool>(type: "bit", nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Color_Color",
                        column: x => x.Color,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Product_Product_ID",
                        column: x => x.Product_ID,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetail_Size_Size",
                        column: x => x.Size,
                        principalTable: "Size",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bill_ID = table.Column<int>(type: "int", nullable: false),
                    Product_Detail_ID = table.Column<int>(type: "int", nullable: false),
                    Don_Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Gia_Ban = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    So_Luong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillItem_Bill_Bill_ID",
                        column: x => x.Bill_ID,
                        principalTable: "Bill",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillItem_ProductDetail_Product_Detail_ID",
                        column: x => x.Product_Detail_ID,
                        principalTable: "ProductDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cart_ID = table.Column<int>(type: "int", nullable: false),
                    Product_Detail_ID = table.Column<int>(type: "int", nullable: false),
                    So_Luong = table.Column<int>(type: "int", nullable: false),
                    Ngay_Tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trangthai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_Cart_ID",
                        column: x => x.Cart_ID,
                        principalTable: "Carts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_ProductDetail_Product_Detail_ID",
                        column: x => x.Product_Detail_ID,
                        principalTable: "ProductDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brand",
                columns: new[] { "Id", "LogoUrl", "NgayTao", "Ten", "TrangThai" },
                values: new object[] { 1, "logo.png", null, "FourStars Shoes", true });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "Ma", "Ngay_Tao", "Ten" },
                values: new object[,]
                {
                    { 1, "#000000", new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2412), "Đen" },
                    { 2, "#FFFFFF", new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2415), "Trắng" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "AnhDaiDien", "Email", "Gioi_Tinh", "Mat_Khau", "Ngay_Sinh", "Ngay_Tao", "ResetToken", "SDT", "Ten", "Trang_Thai" },
                values: new object[] { 1, null, "admin@fourstars.com", true, "$2a$11$KiUJ/sCWTSqMSRKPkddVOOm96dqF54Dk50YiIYghacO7P9BZlQkDC", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2561), null, "0912345678", "Nguyễn Văn Admin", true });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Ma", "Ngay_Tao", "Ten", "Trang_Thai" },
                values: new object[] { 1, "SNK", new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2363), "Giày Sneaker", true });

            migrationBuilder.InsertData(
                table: "Size",
                columns: new[] { "Id", "Ma", "Ngay_Tao", "Ten" },
                values: new object[,]
                {
                    { 1, 37, new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2452), "37" },
                    { 2, 38, new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2455), "38" },
                    { 3, 39, new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2457), "39" }
                });

            migrationBuilder.InsertData(
                table: "Supplier",
                columns: new[] { "Id", "Dia_Chi", "Email", "Ngay_Tao", "SDT", "Ten", "Trang_Thai" },
                values: new object[] { 1, "Hà Nội", "khohn@gmail.com", new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(1494), "0987654321", "Tổng kho Unisex HN", true });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Anhdaidien", "CCCD", "Dia_Chi", "Email", "Gioi_Tinh", "Ma", "Mat_Khau", "Ngay_Sinh", "Ngay_Tao", "Role", "SDT", "Ten", "Trang_Thai" },
                values: new object[,]
                {
                    { 1, "noavatar.png", "012345678901", "Hà Nội, Việt Nam", "admin@fourstars.com", true, "ADMIN001", "$2a$11$KiUJ/sCWTSqMSRKPkddVOOm96dqF54Dk50YiIYghacO7P9BZlQkDC", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), true, "0912345678", "Quản Trị Viên Hệ Thống", true },
                    { 2, "noavatar.png", "012345678902", "Hải Phòng, Việt Nam", "nhanvien1@gmail.com", false, "NV001", "$2a$11$KiUJ/sCWTSqMSRKPkddVOOm96dqF54Dk50YiIYghacO7P9BZlQkDC", new DateTime(2002, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), false, "0334567890", "Nguyễn Nhân Viên", true }
                });

            migrationBuilder.InsertData(
                table: "quanly",
                columns: new[] { "ID", "phiShip" },
                values: new object[] { 1, 50000m });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "DiaChiChitiet", "HoTen", "IdCustomer", "SDT" },
                values: new object[,]
                {
                    { 1, "Số 123, Đường Trịnh Văn Bô, Phương Canh, Nam Từ Liêm, Hà Nội", "Nguyễn Văn Admin", 1, "0912345678" },
                    { 2, "Tòa nhà FPT Poly, Cầu Giấy, Hà Nội", "Văn Admin (Cơ quan)", 1, "0243123456" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "ID", "CustomerId", "Customer_ID", "Ngay_Tao", "Trang_Thai" },
                values: new object[] { 1, null, 1, new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2786), 1 });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Brand_ID", "Ma", "Mo_Ta", "Nam_SX", "Product_Type_ID", "Supplier_ID", "Ten" },
                values: new object[] { 1, 1, "PROD001", "Mẫu giày bán chạy nhất 2026", 2026, 1, 1, "Giày Unisex FourStars Classic" });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "ID", "IMG", "Ngay_Tao", "Product_ID", "Trang_Thai" },
                values: new object[,]
                {
                    { 1, "~/img/product/giayxah2.jpg", new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2841), 1, true },
                    { 2, "~/img/product/addidas1.jpg", new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2843), 1, true }
                });

            migrationBuilder.InsertData(
                table: "ProductDetail",
                columns: new[] { "Id", "Color", "Don_Gia", "Image", "Ngay_Tao", "Product_ID", "SL", "Sale", "Size", "Trang_Thai" },
                values: new object[,]
                {
                    { 1, 1, 500000m, "~/img/product/nikeairforce11.jpg", new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2533), 1, 100, 0, 1, true },
                    { 2, 2, 550000m, "~/img/product/nikeairzoom1.jpg", new DateTime(2026, 5, 16, 20, 52, 8, 467, DateTimeKind.Local).AddTicks(2536), 1, 50, 5, 2, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_IdCustomer",
                table: "Address",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_Address_Id",
                table: "Bill",
                column: "Address_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_AddressId",
                table: "Bill",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_Customer_ID",
                table: "Bill",
                column: "Customer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_CustomerId",
                table: "Bill",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_User_ID",
                table: "Bill",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_Voucher_ID",
                table: "Bill",
                column: "Voucher_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_VoucherShip_ID",
                table: "Bill",
                column: "VoucherShip_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BillItem_Bill_ID",
                table: "BillItem",
                column: "Bill_ID");

            migrationBuilder.CreateIndex(
                name: "IX_BillItem_Product_Detail_ID",
                table: "BillItem",
                column: "Product_Detail_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_Cart_ID",
                table: "CartItems",
                column: "Cart_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_Product_Detail_ID",
                table: "CartItems",
                column: "Product_Detail_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_Customer_ID",
                table: "Carts",
                column: "Customer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_Product_ID",
                table: "Image",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Brand_ID",
                table: "Product",
                column: "Brand_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Product_Type_ID",
                table: "Product",
                column: "Product_Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Supplier_ID",
                table: "Product",
                column: "Supplier_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_Color",
                table: "ProductDetail",
                column: "Color");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_Product_ID",
                table: "ProductDetail",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetail_Size",
                table: "ProductDetail",
                column: "Size");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillItem");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "quanly");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ProductDetail");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "VoucherShip");

            migrationBuilder.DropTable(
                name: "Vourcher");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
