using DATNSD54.DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace DATNSD54.DAO.Data
{
    public class DbContextApp : DbContext
    {
        public DbContextApp(DbContextOptions<DbContextApp> options) : base(options) { }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }



        public DbSet<Models.Address> Address { get; set; }
        public DbSet<Models.CartItem> CartItems { get; set; }
        public DbSet<Models.Bill> Bill { get; set; }
        public DbSet<Models.BillItem> BillItem { get; set; }
        public DbSet<Models.Brand> Brand { get; set; }
        public DbSet<Models.Cart> Carts { get; set; }
        public DbSet<Models.Color> Color { get; set; }
        public DbSet<Models.Customer> Customer { get; set; }
        public DbSet<Models.Image> Image { get; set; }
        public DbSet<Models.Product> Product { get; set; }
        public DbSet<Models.ProductDetail> ProductDetail { get; set; }
        public DbSet<Models.ProductType> ProductType { get; set; }
        public DbSet<Models.Size> Size { get; set; }
        public DbSet<Models.Supplier> Supplier { get; set; }
        public DbSet<Models.User> User { get; set; }
        public DbSet<Models.Voucher> Vourcher { get; set; }
        public DbSet<Models.VoucherShip> VoucherShip { get; set; }
        public DbSet<Models.QuanLy> quanly { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Chặn xóa dây chuyền từ Customer -> Bill
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.Customer_ID)
                .OnDelete(DeleteBehavior.NoAction);

            // 2. Chặn xóa dây chuyền từ Address -> Bill
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Address)
                .WithMany()
                .HasForeignKey(b => b.Address_Id)
                .OnDelete(DeleteBehavior.NoAction);

            // 3. Nếu Cart cũng bị báo lỗi tương tự thì thêm dòng này
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.Customer_ID)
                .OnDelete(DeleteBehavior.NoAction);
            // 1. Seed dữ liệu Supplier
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { Id = 1, Ten = "Tổng kho Unisex HN", SDT = "0987654321", Email = "khohn@gmail.com", Dia_Chi = "Hà Nội", Trang_Thai = true }
            );

            // 2. Seed dữ liệu Brand
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Ten = "FourStars Shoes", LogoUrl = "logo.png", TrangThai = true }
            );

            // 3. Seed dữ liệu ProductType
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType { Id = 1, Ma = "SNK", Ten = "Giày Sneaker", Trang_Thai = true }
            );

            // 4. Seed dữ liệu Color (Đen - Trắng)
            modelBuilder.Entity<Color>().HasData(
                new Color { Id = 1, Ma = "#000000", Ten = "Đen", Ngay_Tao = DateTime.Now },
                new Color { Id = 2, Ma = "#FFFFFF", Ten = "Trắng", Ngay_Tao = DateTime.Now }
            );

            // 5. Seed dữ liệu Size (37, 38, 39)
            modelBuilder.Entity<Size>().HasData(
                new Size { Id = 1, Ma = 37, Ten = "37", Ngay_Tao = DateTime.Now },
                new Size { Id = 2, Ma = 38, Ten = "38", Ngay_Tao = DateTime.Now },
                new Size { Id = 3, Ma = 39, Ten = "39", Ngay_Tao = DateTime.Now }
            );

            // 6. Seed dữ liệu Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Ma = "PROD001",
                    Ten = "Giày Unisex FourStars Classic",
                    Nam_SX = 2026,
                    Mo_Ta = "Mẫu giày bán chạy nhất 2026",
                    Product_Type_ID = 1,
                    Supplier_ID = 1,
                    Brand_ID = 1
                }
            );

            // 7. Seed dữ liệu ProductDetail (2 chi tiết cho Product Id = 1)
            modelBuilder.Entity<ProductDetail>().HasData(
                new ProductDetail
                {
                    Id = 1,
                    Product_ID = 1,
                    Size = 1, // Cỡ 37
                    Color = 1, // Màu Đen
                    Don_Gia = 500000,
                    SL = 100,
                    Sale = 0,
                    Trang_Thai = true,
                    Ngay_Tao = DateTime.Now
                },
                new ProductDetail
                {
                    Id = 2,
                    Product_ID = 1,
                    Size = 2, // Cỡ 38
                    Color = 2, // Màu Trắng
                    Don_Gia = 550000,
                    SL = 50,
                    Sale = 5,
                    Trang_Thai = true,
                    Ngay_Tao = DateTime.Now
                }


            );

            modelBuilder.Entity<Customer>().HasData(
    new Customer
    {
        Id = 1,
        Ten = "Nguyễn Văn Admin",
        SDT = "0912345678",
        Email = "admin@fourstars.com",
        Mat_Khau = "$2a$11$KiUJ/sCWTSqMSRKPkddVOOm96dqF54Dk50YiIYghacO7P9BZlQkDC",
        Ngay_Tao = DateTime.Now,
        Trang_Thai = true,
        Gioi_Tinh = true,
        Ngay_Sinh = new DateTime(2000, 1, 1)
    }
);
            modelBuilder.Entity<Address>().HasData(
    new Address
    {
        Id = 1,
        IdCustomer = 1, // Khớp với Id của Nguyễn Văn Admin ở trên
        HoTen = "Nguyễn Văn Admin",
        SDT = "0912345678",
        DiaChiChitiet = "Số 123, Đường Trịnh Văn Bô, Phương Canh, Nam Từ Liêm, Hà Nội",
        // Bills và Customer không được đưa vào HasData vì là Navigation Property
    },
    new Address
    {
        Id = 2,
        IdCustomer = 1, // Vẫn là ông Admin nhưng địa chỉ khác
        HoTen = "Văn Admin (Cơ quan)",
        SDT = "0243123456",
        DiaChiChitiet = "Tòa nhà FPT Poly, Cầu Giấy, Hà Nội",
    }
);

            // 9. Seed dữ liệu Cart (Giỏ hàng trống ban đầu cho khách hàng trên)
            modelBuilder.Entity<Cart>().HasData(
                new Cart
                {
                    ID = 1,
                    Customer_ID = 1, // Link tới Id của Customer vừa tạo ở trên
                    Ngay_Tao = DateTime.Now,
                    Trang_Thai = 1 // 1: Đang hoạt động
                }
            );
            modelBuilder.Entity<QuanLy>().HasData(
                 new QuanLy
                 {
                     ID = 1,
                     phiShip = 50000
                 }
                 );

            // Giả sử ní đã có Product với ID là 1 và 2 rồi nhé
            modelBuilder.Entity<Image>().HasData(
                new Image
                {
                    ID = 1, // Key bắt buộc phải tự điền khi Seed Data
                    Product_ID = 1,
                    IMG = "~/img/product/ambush1.jpg",
                    Ngay_Tao = DateTime.Now,
                    Trang_Thai = true
                },
                new Image
                {
                    ID = 2,
                    Product_ID = 1, // Hoặc Product_ID khác tùy ní
                    IMG = "~/img/product/addidas1.jpg",
                    Ngay_Tao = DateTime.Now,
                    Trang_Thai = true
                }
            );
            modelBuilder.Entity<User>().HasData(
    new User
    {
        ID = 1,
        Ma = "ADMIN001",
        Ten = "Quản Trị Viên Hệ Thống",
        Role = true, // true là Admin như ní chú thích trong Model
        Email = "admin@fourstars.com",
        SDT = "0912345678", // Đúng 10 số, bắt đầu bằng 0
        CCCD = "012345678901", // Đúng 12 số
        Mat_Khau = "$2a$11$KiUJ/sCWTSqMSRKPkddVOOm96dqF54Dk50YiIYghacO7P9BZlQkDC", // Thỏa mãn: Hoa, thường, số, không cách
        Gioi_Tinh = true, // Nam
        Ngay_Sinh = new DateTime(2000, 1, 1),
        Dia_Chi = "Hà Nội, Việt Nam",
        Anhdaidien = "noavatar.png",
        Ngay_Tao = new DateTime(2024, 1, 1, 8, 0, 0), // Nên để ngày cố định thay vì DateTime.Now để tránh Migration bị đổi liên tục
        Trang_Thai = true
    },
    new User
    {
        ID = 2,
        Ma = "NV001",
        Ten = "Nguyễn Nhân Viên",
        Role = false, // false là Nhân viên
        Email = "nhanvien1@gmail.com",
        SDT = "0334567890",
        CCCD = "012345678902",
        Mat_Khau = "$2a$11$KiUJ/sCWTSqMSRKPkddVOOm96dqF54Dk50YiIYghacO7P9BZlQkDC",
        Gioi_Tinh = false, // Nữ
        Ngay_Sinh = new DateTime(2002, 5, 20),
        Dia_Chi = "Hải Phòng, Việt Nam",
        Anhdaidien = "noavatar.png",
        Ngay_Tao = new DateTime(2024, 1, 2, 9, 0, 0),
        Trang_Thai = true
    }
);
        }

    }
}
