using DATNSD54.DAO.Data;
using DATNSD54.DAO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATNSD54.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly DbContextApp _context;

        public VouchersController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/Vouchers
        [HttpGet]
        public async Task<IActionResult> GetVouchers()
        {
            // Sử dụng .OrderByDescending để Voucher mới nhất luôn hiện lên đầu danh sách Index
            var vouchers = await _context.Vourcher
                .OrderByDescending(v => v.Ngay_Tao)
                .ToListAsync();
            return Ok(vouchers);
        }

        // GET: api/Vouchers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucher(string id)
        {
            var voucher = await _context.Vourcher.FirstOrDefaultAsync(m => m.ID == id);

            if (voucher == null)
            {
                return NotFound(new { Success = false, Message = "Không tìm thấy Voucher" });
            }

            return Ok(voucher);
        }

        // POST: api/Vouchers
        [HttpPost]
        public async Task<IActionResult> CreateVoucher([FromBody] Voucher voucher)
        {
            if (!ModelState.IsValid)
            {
                // Gom tất cả lỗi validation thành một chuỗi tin nhắn gửi về cho View
                var errorList = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                string allErrors = string.Join(", ", errorList);
                return BadRequest(new { Success = false, Message = "Dữ liệu không hợp lệ: " + allErrors });
            }

            // 1. Chuẩn hóa ID: Viết hoa mã Voucher để đồng bộ (Ví dụ: km50 -> KM50)
            voucher.ID = voucher.ID.Trim().ToUpper();

            // 2. Kiểm tra trùng mã ID
            if (await _context.Vourcher.AnyAsync(v => v.ID == voucher.ID))
            {
                return BadRequest(new { Success = false, Message = "Mã Voucher này đã tồn tại trong hệ thống!" });
            }

            // 3. Logic ngày tháng: Đảm bảo ngày kết thúc sau ngày bắt đầu
            if (voucher.Ngay_Ket_Thuc <= voucher.Ngay_Bat_Dau)
            {
                return BadRequest(new { Success = false, Message = "Ngày hết hạn phải sau ngày bắt đầu!" });
            }

            if (voucher.Ngay_Tao == default)
                voucher.Ngay_Tao = DateTime.Now;

            try
            {
                _context.Vourcher.Add(voucher);
                await _context.SaveChangesAsync();
                return Ok(new { Success = true, Message = "Tạo Voucher thành công!", Data = voucher });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Lỗi Database: " + ex.Message });
            }
        }

        // PUT: api/Vouchers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVoucher(string id, [FromBody] Voucher voucher)
        {
            if (id != voucher.ID)
            {
                return BadRequest(new { Success = false, Message = "Mã Voucher không khớp giữa URL và dữ liệu!" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(voucher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { Success = true, Message = "Cập nhật thành công!" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherExists(id))
                {
                    return NotFound(new { Success = false, Message = "Voucher không còn tồn tại trên hệ thống" });
                }
                throw;
            }
        }

        // DELETE: api/Vouchers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher(string id)
        {
            var voucher = await _context.Vourcher.FindAsync(id);
            if (voucher == null)
            {
                return NotFound(new { Success = false, Message = "Voucher này không tồn tại hoặc đã bị xóa trước đó" });
            }

            _context.Vourcher.Remove(voucher);
            await _context.SaveChangesAsync();

            return Ok(new { Success = true, Message = "Đã xóa Voucher thành công!" });
        }

        private bool VoucherExists(string id)
        {
            return _context.Vourcher.Any(e => e.ID == id);
        }
    }
}
