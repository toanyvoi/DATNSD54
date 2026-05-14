using DATNSD54.DAO.DTO;
using DATNSD54.DAO.DTO.Request;
using DATNSD54.DAO.Data;
using DATNSD54.DAO.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DATNSD54.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbContextApp _context;
        private readonly IConfiguration _configuration;
        public AuthController(DbContextApp context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        // GET: api/<AuthController>
        [HttpPost("register-customer")]
        public async Task<IActionResult> Register([FromBody] CustomerRegisterDTO request)
        {
            // Kiểm tra SDT đăng nhập đã tồn tại chưa
            if (await _context.Customer.AnyAsync(c => c.SDT == request.SDT))
                return BadRequest(new RegisterResponseDTO { Message="Số điện thoại đã tồn tại",Success=false});
            if (await _context.Customer.AnyAsync(c => c.Email == request.Email))
                return BadRequest(new RegisterResponseDTO { Message = "Email đã tồn tại", Success = false });
            var customer = new Customer
            {
                Ten = request.Name,
                SDT = request.SDT,
                Email = request.Email,
                // Mã hóa mật khẩu bằng BCrypt
                Mat_Khau = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Ngay_Tao = DateTime.Now
            };

            _context.Customer.Add(customer);

            await _context.SaveChangesAsync();
            _context.Carts.Add(new Cart
            {
                Customer = customer,
                Ngay_Tao = DateTime.Now,
            });

            await _context.SaveChangesAsync();
            return Ok(new RegisterResponseDTO { Success = true, Message = "Đăng ký thành công!" });
        }


        [HttpPost("login-customer")]
        public async Task<IActionResult> LoginCustomer([FromBody] LoginDTO request)
        {
            // Tìm customer theo SDT hoặc Email
            var customer = await _context.Customer
                .FirstOrDefaultAsync(c => c.SDT == request.Username || c.Email == request.Username);

            if (customer == null)
                return BadRequest(new LoginResponseDTO
                {
                    Success = false,
                    Message = "Tài khoản không tồn tại!"
                });

            // Kiểm tra tài khoản có bị khóa không
            if (!customer.Trang_Thai)
                return BadRequest(new LoginResponseDTO
                {
                    Success = false,
                    Message = "Tài khoản đã bị khóa!"
                });

            // Verify mật khẩu
            if (!BCrypt.Net.BCrypt.Verify(request.Password, customer.Mat_Khau))
                return BadRequest(new LoginResponseDTO
                {
                    Success = false,
                    Message = "Mật khẩu không chính xác!"
                });

            // Tạo JWT token
            var token = GenerateJwtToken(customer.Id.ToString(), "Customer", customer.Ten, "Customer");

            var response = new LoginResponseDTO
            {
                Success = true,
                Message = "Đăng nhập thành công!",
                Token = token,
                UserType = "Customer",
                CustomerInfo = new CustomerInfoDTO
                {
                    Id = customer.Id,
                    Ten = customer.Ten,
                    Email = customer.Email,
                    SDT = customer.SDT
                }
            };

            return Ok(response);
        }

        // Đăng nhập cho USER (Nhân viên/Admin)
        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO request)
        {
            // Tìm user theo Ma, SDT hoặc Email
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Ma == request.Username ||
                                         u.SDT == request.Username ||
                                         u.Email == request.Username);

            if (user == null)
                return BadRequest(new LoginResponseDTO
                {
                    Success = false,
                    Message = "Tài khoản không tồn tại!"
                });

            // Kiểm tra tài khoản có bị khóa không
            if (!user.Trang_Thai)
                return BadRequest(new LoginResponseDTO
                {
                    Success = false,
                    Message = "Tài khoản đã bị khóa!"
                });

            // Verify mật khẩu
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Mat_Khau))
                return BadRequest(new LoginResponseDTO
                {
                    Success = false,
                    Message = "Mật khẩu không chính xác!"
                });

            // Xác định role
            string role = user.Role ? "Admin" : "Staff";

            // Tạo JWT token
            var token = GenerateJwtToken(user.ID.ToString(), "User", user.Ten, role);

            var response = new LoginResponseDTO
            {
                Success = true,
                Message = "Đăng nhập thành công!",
                Token = token,
                UserType = "User",
                UserInfo = new UserInfoDTO
                {
                    ID = user.ID,
                    Ma = user.Ma,
                    Ten = user.Ten,
                    Email = user.Email,
                    SDT = user.SDT,
                    Role = user.Role,
                    RoleName = role
                }
            };

            return Ok(response);
        }

        // Hàm tạo JWT Token
        private string GenerateJwtToken(string userId, string userType, string userName, string role)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName),
            new Claim("UserType", userType), // Customer hoặc User khác type
            new Claim(ClaimTypes.Role, role)
        };

            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLong!"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7), // Token có hiệu lực 7 ngày
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet("customer-Acc-View")]
        public async Task<IActionResult> AccountCustomerView()
        {
            try
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);// lấy id customer đăg đăng nhập 
                if (userIdClaim == null)
                {
                    return Unauthorized("Bạn chưa đăng nhập hoặc phiên làm việc hết hạn.");
                }
                int customerId = int.Parse(userIdClaim.Value);
                var CustomerLoggedIn = await _context.Customer.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == customerId);
                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role);
                if (roleClaim != null)
                {
                    string role = roleClaim.Value; // Lấy được "Admin" hoặc "Customer"

                    if (role != "Customer")
                    {
                        return BadRequest("vui lòng đăng nhập tài khoản customer");
                    }
                }

                if (CustomerLoggedIn == null)
                {
                    return NotFound("Không tìm thấy thông tin khách hàng.");
                }
                
                var listbill = await _context.Bill
    .AsNoTracking() // Thêm cái này để tăng tốc vì bạn chỉ đang lấy danh sách để xem
    .AsSplitQuery()
    .Where(b => b.Customer_ID == customerId && b.Trang_Thai != 0)
    .Include(b => b.Address)
    .Include(b => b.Customer)
    .Include(b => b.BillItems)
        .ThenInclude(bi => bi.ProductDetail)
            .ThenInclude(pd => pd.Product)
                .ThenInclude(p => p.images) // Lấy ảnh từ Product
    .Include(b => b.BillItems)
        .ThenInclude(bi => bi.ProductDetail)
            .ThenInclude(pd => pd.SizeNavigation) // Lấy Size từ ProDetail
    .Include(b => b.BillItems)
        .ThenInclude(bi => bi.ProductDetail)
            .ThenInclude(pd => pd.ColorNavigation) // Lấy Color từ ProDetail
    .ToListAsync();
                var Address = await _context.Address.Where(a => a.IdCustomer == customerId).ToListAsync();
                var listbillDTO = listbill
                    .Select(b => new BillDTO
                    {
                        ID = b.ID,
                        Customer_ID = b.Customer_ID,
                        Customer_Name = b.Customer.Ten,
                        User_ID = b.User_ID,
                        Voucher_ID = b.Voucher_ID,
                        Shipcost = b.ShipCost,
                        VoucherShip_ID = b.VoucherShip_ID,
                        Ngay_Tao = b.Ngay_Tao,
                        Gia_Goc = b.Gia_Goc,
                        Phuong_Thuc_Thanh_Toan = b.Phuong_Thuc_Thanh_Toan,
                        Thanh_Tien = b.Thanh_Tien,
                        Dia_Chi_Id = b.Address_Id,
                        SDT_Nguoi_Nhan = b.Address.SDT,
                        Ten_Nguoi_Nhan = b.Address.HoTen,
                        Dia_Chi_Chi_tiet = b.Address.DiaChiChitiet,
                        Trang_Thai = b.Trang_Thai,
                        listAddress = Address,
                        // Gán danh sách rỗng cho các List để tránh lỗi null ở Client nếu cần
                        BillItems = b.BillItems.Select(bi => new BillItemDTO
                        {
                            ID = bi.ID,
                            product_id = bi.ProductDetail.Product_ID,
                            nameProduct = bi.ProductDetail.Product.Ten,
                            UrlIMG = bi.ProductDetail.Product.images.FirstOrDefault()?.IMG,
                            Size = bi.ProductDetail.SizeNavigation.Ten,
                            Color = bi.ProductDetail.ColorNavigation.Ten,

                            sale = (int)((bi.Don_Gia - bi.Gia_Ban) / bi.Don_Gia * 100),
                            product_detail_id = bi.Product_Detail_ID,
                            so_luong = bi.So_Luong,
                            BasePrice = bi.Don_Gia,
                            price = bi.Gia_Ban,
                            trangthai = bi.ProductDetail.Trang_Thai,
                            
                        }).ToList(),
                        // Không gán listAddress theo yêu cầu B2
                    })
                    .ToList();
                    
                var accManagerDTO = new AccManagerDTO
                {
                    AccCustomer = CustomerLoggedIn,
                    Listbill = listbillDTO
                };


                return Ok(accManagerDTO);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi server: " + ex.Message);
            }

        }

        [HttpPut("update-profile")]
        //[Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateCustomerRequest request)
        {
            try
            {
                // 1. Lấy id từ token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                    return Unauthorized("Không xác định được user");

                int customerId = int.Parse(userIdClaim);
                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role);
                if (roleClaim != null)
                {
                    string role = roleClaim.Value; // Lấy được "Admin" hoặc "Customer"

                    if (role != "Customer")
                    {
                        return BadRequest("vui lòng đăng nhập tài khoản customer");
                    }
                }
                // 2. Tìm customer
                var customer = await _context.Customer
                    .Include(c => c.Address) // load address luôn
                    .FirstOrDefaultAsync(c => c.Id == customerId);

                if (customer == null)
                    return NotFound("Không tìm thấy khách hàng");

                // 3. Check SDT trùng (trừ chính nó)
                if (!string.IsNullOrEmpty(request.SDT))
                {
                    var checkSDT = await _context.Customer
                        .AnyAsync(x => x.SDT == request.SDT && x.Id != customerId);

                    if (checkSDT)
                        return BadRequest("Số điện thoại đã tồn tại");
                }

                // 4. Check Email trùng (trừ chính nó)
                if (!string.IsNullOrEmpty(request.Email))
                {
                    var checkEmail = await _context.Customer
                        .AnyAsync(x => x.Email == request.Email && x.Id != customerId);

                    if (checkEmail)
                        return BadRequest("Email đã tồn tại");
                }

                // 5. Update dữ liệu
                if (!string.IsNullOrEmpty(request.Ten))
                    customer.Ten = request.Ten;

                if (!string.IsNullOrEmpty(request.Email))
                    customer.Email = request.Email;

                if (!string.IsNullOrEmpty(request.SDT))
                    customer.SDT = request.SDT;

                if (!string.IsNullOrEmpty(request.AnhDaiDien))
                    customer.AnhDaiDien = request.AnhDaiDien;




                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Cập nhật thành công",
                    data = customer
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi server: " + ex.Message);
            }

        }
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            if (userId == null)
                return Unauthorized("Chưa đăng nhập");

            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.Id.ToString() == userId);

            if (customer == null)
                return NotFound("Không tìm thấy tài khoản");

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, customer.Mat_Khau))
                return BadRequest("Mật khẩu cũ không đúng");

            if (request.NewPassword != request.ConfirmPassword)
                return BadRequest("Mật khẩu không khớp");

            customer.Mat_Khau = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            await _context.SaveChangesAsync();

            return Ok("Đổi mật khẩu thành công");
        }
        [HttpPut("cancel-bill/{id}")]
        [Authorize]
        public async Task<IActionResult> CancelBill(int id)
        {
            try
            {
                // 1. Lấy ID người dùng từ Token để đảm bảo tính bảo mật
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                    return Unauthorized("Phiên làm việc hết hạn.");

                int customerId = int.Parse(userIdClaim);

                // 2. Tìm đơn hàng (Phải đúng của Customer đó và đang ở trạng thái 1)
                var bill = await _context.Bill.Include(b => b.BillItems).ThenInclude( i => i.ProductDetail)
                    .FirstOrDefaultAsync(b => b.ID == id && b.Customer_ID == customerId);
                // 1. Lấy ra danh sách ID cần thiết từ đơn hàng
                var productDetailIds = bill.BillItems.Select(p => p.Product_Detail_ID).ToList();

                // 2. Chỉ tải lên RAM những sản phẩm nằm trong danh sách ID đó
                var productList = await _context.ProductDetail
                    .Where(p => productDetailIds.Contains(p.Id))
                    .ToListAsync();
                foreach (var p in bill.BillItems )
                {
                    var product = productList.FirstOrDefault(i => i.Id == p.Product_Detail_ID);
                    if (product != null)
                    {
                        product.SL += p.So_Luong;
                        _context.Entry(product).State = EntityState.Modified;
                    }
                }

                if (bill == null)
                    return NotFound("Không tìm thấy đơn hàng.");

                // 3. Kiểm tra logic: Chỉ cho phép hủy khi Trang_Thai == 1 (Đang xử lý)
                if (bill.Trang_Thai != 1)
                {
                    return BadRequest("Đơn hàng đã được giao hoặc không ở trạng thái có thể hủy.");
                }



                // 4. Cập nhật trạng thái sang 5 (Đã hủy)
                bill.Trang_Thai = 5;

                _context.Entry(bill).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok("Hủy đơn hàng thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi server: " + ex.Message);
            }
        }
    }
}
