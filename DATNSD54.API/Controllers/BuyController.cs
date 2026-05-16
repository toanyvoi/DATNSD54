using DATNSD54.DAO.Data;
using DATNSD54.DAO.DTO;
using DATNSD54.DAO.DTO.Request;
using DATNSD54.DAO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DATNSD54.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BuyController : ControllerBase
    {
        private readonly DbContextApp _context;

        public BuyController(DbContextApp context)
        {
            _context = context;
        }

        [HttpPost("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartReq req)
        {
            // 1. Tìm CartItem và kèm theo ProductDetail để check tồn kho
            var item = await _context.CartItems
                .Include(i => i.ProductDetail)
                .FirstOrDefaultAsync(i => i.ID == req.id);

            if (item == null) return Ok(new { success = false, message = "Không tìm thấy sản phẩm" });

            int targetQty = item.So_Luong;

            // 2. Xử lý theo Type của ní
            if (req.type == "+") targetQty += 1;
            else if (req.type == "-") targetQty -= 1;
            else if (req.type == "=") targetQty = req.sl;

            // 3. Kiểm tra tính hợp lệ
            if (targetQty < 1) return Ok(new { success = false, message = "Số lượng tối thiểu là 1", currentQty = item.So_Luong });

            if (item.ProductDetail != null && targetQty > item.ProductDetail.SL)
            {
                return Ok(new
                {
                    success = false,
                    message = $"Chỉ còn {item.ProductDetail.SL} sản phẩm trong kho",
                    currentQty = item.So_Luong
                });
            }

            // 4. Cập nhật và lưu
            item.So_Luong = targetQty;
            item.trangthai = true; // Bật lại trạng thái nếu số lượng đã hợp lệ
            await _context.SaveChangesAsync();

            // 5. Tính toán lại tiền để trả về cho View cập nhật (Optional)
            var finalPrice = item.ProductDetail.Don_Gia - (item.ProductDetail.Don_Gia * (item.ProductDetail.Sale) / 100m);
            var grandTotal = await _context.CartItems
    .Where(ci => ci.Cart_ID == item.Cart_ID && ci.trangthai == true)
    .SumAsync(ci => (ci.ProductDetail.Don_Gia - (ci.ProductDetail.Don_Gia * (ci.ProductDetail.Sale) / 100m)) * ci.So_Luong);

            return Ok(new
            {
                success = true,
                newQty = item.So_Luong,
                newLineTotal = (finalPrice * item.So_Luong).ToString("N0"),
                newGrandTotal = grandTotal.ToString("N0")
            });
        }
        [HttpDelete("DeleteCartItem/{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return NotFound(new { success = false, message = "Không tìm thấy sản phẩm" });

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            // Tính toán lại tổng tiền sau khi xóa
            var grandTotal = await _context.CartItems
    .Where(ci => ci.Cart_ID == item.Cart_ID && ci.trangthai == true)
    .SumAsync(ci => (ci.ProductDetail.Don_Gia - (ci.ProductDetail.Don_Gia * (ci.ProductDetail.Sale) / 100m)) * ci.So_Luong);

            return Ok(new
            {
                success = true,
                message = "Xóa sản phẩm thành công",
                newGrandTotal = grandTotal.ToString("N0")
            });
        }

        private string ValidateVoucher(Voucher v, decimal billTotal)
        {
            if (v == null) return "Voucher không tồn tại!";
            if (v.Trang_Thai != 1) return "Voucher không hợp lệ!";
            if (v.So_Luong <= 0) return "Voucher đã hết lượt sử dụng!";
            if (v.Ngay_Bat_Dau > DateTime.Now) return "Voucher chưa đến ngày sử dụng!";
            if (v.Ngay_Ket_Thuc < DateTime.Now) return "Voucher đã hết hạn sử dụng!";
            if (billTotal < v.Don_Hang_Toi_Thieu) return $"Đơn hàng tối thiểu phải từ {v.Don_Hang_Toi_Thieu:N0}đ";
            return null; // Trả về null nếu mọi thứ đều ổn
        }

        private string ValidateVoucherShip(VoucherShip v, decimal billTotal)
        {
            if (v == null) return "Voucher không tồn tại!";
            if (v.Trang_Thai != 1) return "Voucher không hợp lệ!";
            if (v.So_Luong <= 0) return "Voucher đã hết lượt sử dụng!";
            if (v.Ngay_Bat_Dau > DateTime.Now) return "Voucher chưa đến ngày sử dụng!";
            if (v.Ngay_Ket_Thuc < DateTime.Now) return "Voucher đã hết hạn sử dụng!";
            if (billTotal < v.Don_Hang_Toi_Thieu) return $"Đơn hàng tối thiểu phải từ {v.Don_Hang_Toi_Thieu:N0}đ";
            return null; // Trả về null nếu mọi thứ đều ổn
        }

        //lấy hóa đơn chưa xác nhận để hiển thị lại thông tin trước khi xác nhận
        [HttpGet("{Billid}")]
        public async Task<ActionResult<BillDTO>> GetBillUnVerified(int Billid)
        {
            // Lấy ID từ Claim "NameIdentifier" hoặc claim tùy chỉnh ní đặt lúc tạo Token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            var userTypeClaim =  User.FindFirstValue("UserType");

            if (userIdClaim == null) return Unauthorized();

            int customerId = int.Parse(userIdClaim.Value);
            string userType = userTypeClaim ?? "Customer"; // Mặc định là Customer nếu claim không tồn tại
            
            var ListAddress = await _context.Address.Where(a => a.IdCustomer == customerId).ToListAsync();
            // Dùng Include để lấy kèm danh sách BillItems và Address
            var bill = await _context.Bill
                .Include(b => b.BillItems)
                .Include(b => b.Address)
                .Include(b => b.Voucher)
                .Include(b => b.VoucherShip)
                .FirstOrDefaultAsync(b => b.ID == Billid);
            var cartItems = await _context.CartItems
                .Include(h => h.ProductDetail).ThenInclude( p => p.Product).ThenInclude(p => p.images)
                .Include(p => p.ProductDetail).ThenInclude(p => p.SizeNavigation)
                .Include(p => p.ProductDetail).ThenInclude(p => p.ColorNavigation)
                .Where(c => c.Cart_ID == bill.Customer_ID)
                .ToListAsync();
            decimal GiaGoc = cartItems.Sum(c => ((c.ProductDetail.Don_Gia * (100 - c.ProductDetail.Sale)) / 100.0m) * c.So_Luong);
            if (bill == null || bill.Customer_ID != customerId && userType == "Customer")
            {

                return BadRequest("Không có quyền xem hóa đơn này");
            }
            if (bill.Trang_Thai != 0)
            {
                return BadRequest("Hóa đơn đã được xác nhận trước đó.");
            }
            if (bill == null)
            {
                return NotFound();
            }
            decimal phiship = _context.quanly.FirstOrDefault()?.phiShip ?? 0;
            // Map dữ liệu từ Model sang DTO
            var billDto = new BillDTO
            {
                ID = bill.ID,
                Customer_ID = bill.Customer_ID,
                User_ID = bill.User_ID,
                Voucher_ID = bill.Voucher_ID,
                voucher = bill.Voucher,
                VoucherShip_ID = bill.VoucherShip_ID,
                voucherShip = bill.VoucherShip,
                Ngay_Tao = bill.Ngay_Tao,
                Gia_Goc = GiaGoc,
                Phuong_Thuc_Thanh_Toan = bill.Phuong_Thuc_Thanh_Toan,
                Thanh_Tien = GiaGoc+phiship,
                Dia_Chi_Id = bill.Address_Id,
                Shipcost = phiship,
                // Lấy thông tin từ bảng Address thông qua Navigation Property
                SDT_Nguoi_Nhan = bill.Address?.SDT ?? "N/A",
                Ten_Nguoi_Nhan = bill.Address?.HoTen ?? "N/A",
                Dia_Chi_Chi_tiet = bill.Address?.DiaChiChitiet ??"N/A",
                Trang_Thai = bill.Trang_Thai,
                listAddress = ListAddress,
                // Map danh sách Items
                CartItems = cartItems?.Select(ci => new CartItemDTO
                {
                    id = ci.ID,
                    cart_id = ci.Cart_ID,
                    nameProduct = ci.ProductDetail?.Product?.Ten,
                    Size = ci.ProductDetail?.SizeNavigation?.Ma.ToString() ?? "0",
                    Color = ci.ProductDetail?.ColorNavigation?.Ma ?? "",
                    price = ci.ProductDetail?.Don_Gia ?? 0,
                    sale = ci.ProductDetail?.Sale ?? 0,
                    product_detail_id = ci.Product_Detail_ID,
                    so_luong = ci.So_Luong,
                    UrlIMG = ci.ProductDetail?.Product?.images?.FirstOrDefault()?.IMG ?? ""
                }).ToList() ?? new List<CartItemDTO>()
            };

            return Ok(billDto);
        }

        

        //Tạo hóa đơn mới khi khách hàng bấm "Đặt Hàng" từ giỏ hàng, trạng thái ban đầu là chưa xác nhận (Trang_Thai = 0)
        [HttpPost("PostBillUnVerified")]
        public async Task<ActionResult<Bill>> PostBillUnVerified()
        {


            //var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("nameid");
            if (userIdClaim == null)
            {
                
                var status = User.Identity.IsAuthenticated ? "Đã Login" : "Chưa Login";
                return Unauthorized($"Lỗi: {status}. Không tìm thấy NameIdentifier.");
            }    

            int CustomerId = int.Parse(userIdClaim.Value);

            var diachi = await _context.Address.FirstOrDefaultAsync(a => a.IdCustomer == CustomerId);
            if (diachi == null) { return BadRequest("Vui lòng Tạo địa chỉ"); }
            List<CartItem> CartCheck = await _context.CartItems.Include(h => h.ProductDetail).Where(c => c.Cart_ID == CustomerId).ToListAsync();
            if (!CartCheck.Any()) { return BadRequest("Giỏ hàng trống"); }
            var price = CartCheck.Sum(c => (c.ProductDetail.Don_Gia * (100 - c.ProductDetail.Sale)) / 100 * c.So_Luong);


            Bill bill = new Bill()
            {
                Customer_ID = CustomerId,
                Ngay_Tao = DateTime.Now,
                Gia_Goc = price,
                Phuong_Thuc_Thanh_Toan = 0,
                Thanh_Tien = price,
                Address_Id = diachi.Id,

                Trang_Thai = 0

            };
            await _context.Bill.Where(b => b.Trang_Thai == 0 && b.Customer_ID == CustomerId).ExecuteDeleteAsync();
            _context.Bill.Add(bill);
            await _context.SaveChangesAsync();

            return Ok(bill);
        }

        [HttpPost("PickAddress")]
        public async Task<ActionResult<Address>> PickAddress([FromQuery] int BillId, int AddressId)
        {
            var bill = await _context.Bill.FindAsync(BillId);
            if (bill == null) return NotFound("Hóa đơn không tồn tại!");

            var address = await _context.Address.FindAsync(AddressId);
            if (address == null) return NotFound("Địa chỉ không tồn tại!");

            bill.Address_Id = AddressId;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                DịachiChitiest = address.DiaChiChitiet,
                SDT = address.SDT,
                Ten = address.HoTen
            });
        }


        [HttpPost("AddVoucher")]
        public async Task<ActionResult<Bill>> AddVoucher([FromBody] VoucherRequest request)
        {
            // 1. Check đầu vào cơ bản (Lúc này lấy dữ liệu từ request. ...)
            if (string.IsNullOrEmpty(request.VoucherId) && string.IsNullOrEmpty(request.VoucherShipId))
                return BadRequest("Vui lòng cung cấp ít nhất một loại voucher!");

            var bill = await _context.Bill.FindAsync(request.BillId);
            if (bill == null) return NotFound("Hóa đơn không tồn tại!");

            // 2. Xử lý Voucher giảm giá
            if (!string.IsNullOrEmpty(request.VoucherId))
            {
                var voucher = await _context.Vourcher.FindAsync(request.VoucherId);
                var vError = ValidateVoucher(voucher, bill.Gia_Goc);
                if (vError != null) return BadRequest($"Voucher: {vError}");

                bill.Voucher_ID = request.VoucherId;
            }

            // 3. Xử lý Voucher Ship
            if (!string.IsNullOrEmpty(request.VoucherShipId))
            {
                var vShip = await _context.VoucherShip.FindAsync(request.VoucherShipId);
                var vsError = ValidateVoucherShip(vShip, bill.Gia_Goc);
                if (vsError != null) return BadRequest($"Voucher Ship: {vsError}");

                bill.VoucherShip_ID = request.VoucherShipId;
            }

            await _context.SaveChangesAsync();
            return Ok(bill);
        }

        // Xác nhận hóa đơn chưa xác nhận, cập nhật trạng thái thành đã xác nhận (Trang_Thai = 1), hóa vàng giỏ hàng và trừ kho tương ứng
        [HttpPost("ConfirmUnVerified")]
        public async Task<ActionResult<Bill>> ConfirmUnVerified([FromQuery] int ID, int pttt)
        {
            // Dùng Transaction để bảo vệ dữ liệu
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var bill = await _context.Bill.FirstOrDefaultAsync(b => b.ID == ID);
                if (bill == null) return NotFound("Hóa đơn không tồn tại!");

                var cartCheck = await _context.CartItems
                    .Include(h => h.ProductDetail)
                    .Where(c => c.Cart_ID == bill.Customer_ID && c.trangthai == true)
                    .ToListAsync();

                if (!cartCheck.Any()) return BadRequest("Giỏ hàng trống");

                bill.Phuong_Thuc_Thanh_Toan = pttt;
                bill.Trang_Thai = 1;
                decimal tonggia = 0;
                foreach (var item in cartCheck)
                {
                    // Kiểm tra hàng tồn trước khi trừ (Tránh số lượng âm)
                    if (item.ProductDetail.SL < item.So_Luong)
                        throw new Exception($"Sản phẩm {item.ProductDetail.Id} không đủ hàng!");

                    var bItem = new BillItem
                    {
                        Bill_ID = bill.ID,
                        Product_Detail_ID = item.Product_Detail_ID,
                        So_Luong = item.So_Luong,
                        Don_Gia = item.ProductDetail.Don_Gia,
                        Gia_Ban = (item.ProductDetail.Don_Gia * (100m - item.ProductDetail.Sale)) / 100m
                    };
                    tonggia += bItem.Gia_Ban * bItem.So_Luong;
                    // Trừ kho
                    _context.ProductDetail.Where(p => p.Id == item.Product_Detail_ID)
                        .ExecuteUpdate(p => p.SetProperty(pd => pd.SL, pd => pd.SL - item.So_Luong));

                    _context.BillItem.Add(bItem);
                }
                bill.Gia_Goc = tonggia;

                decimal shipCost = _context.quanly.FirstOrDefault()?.phiShip ?? 0;
                bill.ShipCost = _context.quanly.FirstOrDefault()?.phiShip ?? 0;
                bill.Thanh_Tien = tonggia + shipCost - 0;
                _context.CartItems.RemoveRange(cartCheck);

                // Lưu 
                await _context.SaveChangesAsync();

                // Nếu mọi thứ ngon lành, xác nhận giao dịch (Commit)
                await transaction.CommitAsync();

                return Ok(new { message = "Thanh toán thành công!" });
            }
            catch (Exception ex)
            {
                // Nếu có bất kỳ lỗi nào, hùy bỏ mọi thay đổi (bao gồm cả việc trừ kho của ExecuteUpdate)
                await transaction.RollbackAsync();
                return BadRequest("Có lỗi xảy ra: " + ex.Message);
            }
        }
        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.ID == id);
        }

    }
}
