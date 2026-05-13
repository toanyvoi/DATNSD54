using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATNSD54.DAO.Data;
using DATNSD54.DAO.Models;
using DATNSD54.DAO.DTO;

namespace DATNSD54.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public ProductsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDisplayDTO>>> GetProduct()
        {
            var ListProduct =  await _context.Product.Include(h => h.images).Include(h => h.ProductDetails).ToListAsync();

            var listDTO = ListProduct.Select(p =>
            {
                var productDetails = p.ProductDetails?
    .OrderBy(pd => (pd.Don_Gia * (100 - pd.Sale)) / 100)
    .FirstOrDefault();


                return new ProductDisplayDTO
                {

                    Id = p.Id,
                    Name = p.Ten,
                    MinPrice = productDetails?.Don_Gia ??0,
                    URLImage = p.images.Select(i => i.IMG).FirstOrDefault(),
                    Sale = productDetails?.Sale ??0,
                    ProductTypeName = p.ProductType?.Ten ?? "N/A",
                    SupplierName = p.Supplier?.Ten ?? "N/A",
                    brandName = p.Brand?.Ten ?? "N/A",
                    trangThai = (p.ProductDetails != null && p.ProductDetails.Any(pd => pd.Trang_Thai == true))
                ? "Đang bán"
                : "Ngừng bán"
                };
            }).ToList();
            return Ok(listDTO);
        }

        [HttpGet("Search/{text}")]
        public async Task<ActionResult<SearchProductDTO>> SearchProduct(string? text)
        {
            var query = _context.Product
         .Include(h => h.images)
         .Include(p => p.ProductType)
         .Include(p => p.Brand)
         .Include(p => p.ProductDetails).ThenInclude(pd => pd.SizeNavigation)
         .Include(p => p.ProductDetails).ThenInclude(pd => pd.ColorNavigation)
         .AsQueryable(); // Chuyển sang Queryable để lọc
            
            // Chỉ lọc nếu text có giá trị
            if (!string.IsNullOrEmpty(text))
            {
                query = query.Where(p => p.Ten.Contains(text) || p.Ma.Contains(text) ||p.Brand.Ten.Contains(text)||p.ProductType.Ten.Contains(text));
            }

            var ListProduct = await query.ToListAsync(); // Lúc này mới thực thi tải dữ liệu


            var productDTOList = ListProduct.Select(product =>
            {
                // Lấy chi tiết sản phẩm đầu tiên hoặc chi tiết có giá thấp nhất để hiển thị đại diện
                var productDetails = product.ProductDetails?
    .OrderBy(pd => (pd.Don_Gia * (100 - pd.Sale)) / 100)
    .FirstOrDefault();

                // Xử lý danh sách ảnh (giả sử ní đã có logic stringlist cho từng product)
                var stringlist = product.images?.Select(img => img.IMG).ToList() ?? new List<string>();

                return new ProductDTO
                {
                    Id = product.Id,
                    Ma = product.Ma,
                    Ten = product.Ten,
                    Nam_SX = product.Nam_SX,
                    Mo_Ta = product.Mo_Ta,
                    GiaMin = productDetails?.Don_Gia ?? 0,
                    sale = productDetails?.Sale ?? 0,
                    type = product.ProductType?.Ten ?? "N/A",
                    brand = product.Brand?.Ten ?? "N/A",

                    salePriceMin = productDetails != null
                                    ? (productDetails.Don_Gia * (100 - productDetails.Sale)) / 100
                                    : 0,

                    ListIMG = stringlist,

                    ListProductDetail = product.ProductDetails?.Select(pd => new ProductDetailDTO
                    {
                        Id = pd.Id,
                        Product_ID = pd.Product_ID,
                        Size = pd.SizeNavigation?.Ma ?? 0,
                        Color = pd.ColorNavigation?.Ma ?? "#FFFFFF",
                        Image = pd.Image,
                        Don_Gia = pd.Don_Gia,
                        SL = pd.SL,
                        Sale = pd.Sale,
                        salePrice = (pd.Don_Gia * (100 - pd.Sale)) / 100,
                        Trang_Thai = pd.Trang_Thai,//bool
                        Ngay_Tao = pd.Ngay_Tao
                    }).ToList() ?? new List<ProductDetailDTO>()
                };
            }).ToList();

            SearchProductDTO searchProductDTO = new SearchProductDTO
            {
                Products = productDTOList,
                brands = await _context.Brand.ToListAsync(),
                sizes = await _context.Size.ToListAsync(),
                productTypes = await _context.ProductType.ToListAsync()
            };

            return Ok(searchProductDTO);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            // 1. Dùng Include để lấy đầy đủ các bảng liên quan trong 1 lần truy vấn
            var product = await _context.Product
                .Include(p => p.ProductType)
                .Include(p => p.Brand)
                .Include(p => p.ProductDetails).ThenInclude(pd => pd.SizeNavigation)
                .Include(p => p.ProductDetails).ThenInclude(pd => pd.ColorNavigation)
                .FirstOrDefaultAsync(p => p.Id == id);

            // 2. Kiểm tra null ngay lập tức
            if (product == null)
            {
                return NotFound();
            }

            var listimg = await _context.Image.Where(i => i.Product_ID == id).ToListAsync();
            var stringlist = listimg.Select(i => i.IMG).ToList();

            // 3. Tìm biến thể có giá sau sale rẻ nhất (Sửa công thức tính giá sale)
            var productDetails = product.ProductDetails?
                .OrderBy(pd => pd.Don_Gia * (100 - pd.Sale) / 100)
                .FirstOrDefault();

            var productdto = new ProductDTO
            {
                Id = product.Id,
                Ma = product.Ma,
                Ten = product.Ten,
                Nam_SX = product.Nam_SX,
                Mo_Ta = product.Mo_Ta,
                GiaMin = productDetails?.Don_Gia ?? 0,
                sale = productDetails?.Sale ?? 0,
                // Thêm kiểm tra null cho Type và Brand để tránh sập web
                type = product.ProductType?.Ten ?? "N/A",
                brand = product.Brand?.Ten ?? "N/A",

                // Giá đã giảm thấp nhất
                salePriceMin = productDetails != null
                               ? (productDetails.Don_Gia * (100 - productDetails.Sale)) / 100
                               : 0,

                ListIMG = stringlist,

                ListProductDetail = product.ProductDetails?.Select(pd => new ProductDetailDTO
                {
                    Id = pd.Id,
                    Product_ID = pd.Product_ID,
                    // Chuyển đổi Ma (string) sang int cho Size nếu Size trong DTO là int
                    Size = pd.SizeNavigation?.Ma ?? 0,
                    Color = pd.ColorNavigation?.Ma ?? "#FFFFFF",
                    Don_Gia = pd.Don_Gia,
                    SL = pd.SL,
                    Sale = pd.Sale,
                    // Giá sau khi giảm của từng biến thể
                    salePrice = (pd.Don_Gia * (100 - pd.Sale)) / 100,
                    Trang_Thai = pd.Trang_Thai,
                    Ngay_Tao = pd.Ngay_Tao
                }).ToList() ?? new List<ProductDetailDTO>(),
            };

            return productdto;
        }

        // Thêm API này để phục vụ riêng cho trang Edit
        [HttpGet("Raw/{id}")]
        public async Task<ActionResult<Product>> GetRawProduct(int id)
        {
            // Chỉ lấy dữ liệu gốc của bảng Product, không Include hay chuyển sang DTO
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
