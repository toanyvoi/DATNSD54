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
using Microsoft.AspNetCore.Authorization;

namespace DATNSD54.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public CartsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDTO>> GetCart(int id)
        {
            var cart = await _context.Carts
    .Include(h => h.CartItems)
        .ThenInclude(i => i.ProductDetail)
            .ThenInclude(pd => pd.Product).ThenInclude(i => i.images)         // Nhánh 1: Lấy Sản phẩm cha
    .Include(h => h.CartItems)
        .ThenInclude(i => i.ProductDetail)
            .ThenInclude(pd => pd.SizeNavigation)  // Nhánh 2: Lấy Size (Dùng đúng tên thuộc tính Navigation)
    .Include(h => h.CartItems)
        .ThenInclude(i => i.ProductDetail)
            .ThenInclude(pd => pd.ColorNavigation) // Nhánh 3: Lấy Màu (Dùng đúng tên thuộc tính Navigation)
    .FirstOrDefaultAsync(h => h.ID == id);

            if (cart == null)
            {
                return NotFound();
            }
            bool hasChanges = false; // Biến đánh dấu xem có cần SaveChanges không

            // 2. Duyệt qua từng món trong giỏ để kiểm tra tồn kho
            foreach (var item in cart.CartItems)
            {
                // Kiểm tra: Nếu số lượng đặt > số lượng tồn trong kho
                if (item.ProductDetail != null && item.So_Luong > item.ProductDetail.SL)
                {
                    // Nếu trạng thái cũ đang là true thì mới cần cập nhật và đánh dấu thay đổi
                    if (item.trangthai)
                    {
                        item.trangthai = false; // "Vô hiệu hóa" món hàng này trong giỏ
                        hasChanges = true;
                    }
                }
                else
                {
                    //  Nếu sau kho nhập thêm hàng, có thể tự động bật lại true
                    if (!item.trangthai && item.So_Luong <= item.ProductDetail.SL) { item.trangthai = true; hasChanges = true; }
                }
            }

            // 3. Nếu có món nào bị đổi trạng thái, lưu ngay vào DB
            if (hasChanges)
            {
                await _context.SaveChangesAsync();
            }
            var cartDTO = new CartDTO
            {
                Id = cart.ID,

                cartItems = cart.CartItems?.Select(ci => new CartItemDTO
                {
                    id = ci.ID,
                    cart_id = ci.Cart_ID,
                    product_id = ci.ProductDetail?.Product_ID ?? 0,
                    nameProduct = ci.ProductDetail?.Product?.Ten,
                    Size = ci.ProductDetail?.SizeNavigation?.Ma.ToString() ?? "0",
                    Color = ci.ProductDetail?.ColorNavigation?.Ma ?? "",
                    price = ci.ProductDetail?.Don_Gia ?? 0,
                    sale = ci.ProductDetail?.Sale ?? 0,
                    product_detail_id = ci.Product_Detail_ID,
                    so_luong = ci.So_Luong,
                    UrlIMG = ci.ProductDetail?.Product?.images?.FirstOrDefault()?.IMG ?? "",
                    trangthai = ci.trangthai

                }).ToList() ?? new List<CartItemDTO>()
            };
            return Ok(cartDTO);
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.ID)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.ID }, cart);
        }

        // DELETE: api/Carts/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCart(int id)
        //{
        //    var cart = await _context.Carts.FindAsync(id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Carts.Remove(cart);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.ID == id);
        }
    }
}
