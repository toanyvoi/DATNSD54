using DATNSD54.DAO.DTO;
using DATNSD54.DAO.DTO.Request;
using DATNSD54.DAO.Models;
using DATNSD54.View.IService;
using DATNSD54.View.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace DATNSD54.View.Controllers
{
    
    public class AuthController : Controller
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        // GET: AuthController
        public async Task<IActionResult> index()
        {
            var customers = await _authService.Show();
            return View(customers);
        }

        

        // GET: Trang chọn loại đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            var user = HttpContext.Session.GetString("JwtToken");
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.IsNullOrEmpty(user))
            {
                // Kiểm tra quyền để điều hướng về đúng trang
                if (role == "Customer")
                {
                    return RedirectToAction("Index", "Home");
                }
                else // Là Admin hoặc Staff (User)
                {

                    return RedirectToAction("Privacy", "Home");
                }
            }
            
            return View();
        }
        [HttpGet]
        
        public IActionResult RegisterCustomer()
        {
            var user = HttpContext.Session.GetString("JwtToken");
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.IsNullOrEmpty(user))
            {
                // Kiểm tra quyền để điều hướng về đúng trang
                if (role == "Customer")
                {
                    return RedirectToAction("Index", "Home");
                }
                else // Là Admin hoặc Staff (User)
                {

                    return RedirectToAction("", "Home");
                }
            }

            return View(new CustomerRegisterDTO());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(CustomerRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.CustomerRegister(model.Name,model.SDT,model.Email,model.Password,model.ConfirmPassword);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }

            TempData["ErrorMessage"] = result.Message;
            return View(model);
        }

        // Trang đăng nhập khách hàng
        [HttpGet]
        public IActionResult LoginCustomer()
        {
            var user = HttpContext.Session.GetString("JwtToken");
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.IsNullOrEmpty(user))
            {
                // Kiểm tra quyền để điều hướng về đúng trang
                if (role == "Customer")
                {
                    return RedirectToAction("Index", "Home");
                }
                else // Là Admin hoặc Staff (User)
                {
                   
                    return RedirectToAction("Index", "Admin");
                }
            }
            return View(new LoginViewModels()); // Trả về Views/Auth/LoginCustomer.cshtml
        }

        // POST: Đăng nhập Customer
        [HttpPost]
        public async Task<IActionResult> LoginCustomer(LoginViewModels model)
        {
            if (!ModelState.IsValid) return View("LoginCustomer", model);

            var result = await _authService.LoginCustomer(model.Username, model.Password);

            if (result.Success)
            {
                // 1. Tạo Claims 
                var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(ClaimTypes.Name, result.CustomerInfo.Ten),
            new System.Security.Claims.Claim(ClaimTypes.Role, "Customer"),
            new System.Security.Claims.Claim("UserId", result.CustomerInfo.Id.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

                // 2. PHÁT COOKIE 
                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));

                // 3. Lưu Session 
                HttpContext.Session.SetString("JwtToken", result.Token);
                HttpContext.Session.SetInt32("userId", result.CustomerInfo.Id);
                HttpContext.Session.SetString("UserRole", "Customer");
                HttpContext.Session.SetString("UserName", result.CustomerInfo.Ten);
                HttpContext.Session.SetString("UserEmail", result.CustomerInfo.Email);
                HttpContext.Session.SetString("ImageUrl", result.CustomerInfo.Image);
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = result.Message;
            return View("LoginCustomer", model);
        }

        // Trang đăng nhập nhân viên
        [HttpGet]
        public IActionResult LoginUser()
        {
            var user = HttpContext.Session.GetString("JwtToken");
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.IsNullOrEmpty(user))
            {
                // Kiểm tra quyền để điều hướng về đúng trang
                if (role == "Customer")
                {
                    return RedirectToAction("Index", "Home");
                }
                else // Là Admin hoặc Staff (User)
                {

                    return RedirectToAction("Index", "Admin");
                }
            }
            return View(new LoginViewModels()); 
        }

        // POST: Đăng nhập User (Nhân viên/Admin)
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginViewModels model)
        {
            if (!ModelState.IsValid) return View("LoginUser", model);

            var result = await _authService.LoginUser(model.Username, model.Password);

            if (result.Success)
            {
                // 1. Phải lấy userInfo RA TRƯỚC khi dùng
                var userInfo = (UserInfoDTO)result.UserInfo;

                // 2. Tạo Claims
                var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(ClaimTypes.Name, userInfo.Ten),
            new System.Security.Claims.Claim(ClaimTypes.Role, userInfo.RoleName),
            new System.Security.Claims.Claim("UserId", userInfo.ID.ToString())
        };

                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

                // 3. PHÁT COOKIE (Quan trọng nhất!)
                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));
                
                // 4. Lưu Session
                HttpContext.Session.SetString("JwtToken", result.Token);
                HttpContext.Session.SetInt32("userId", result.UserInfo.ID);
                HttpContext.Session.SetString("UserRole", userInfo.RoleName);
                HttpContext.Session.SetString("UserName", result.UserInfo.Ten);
                HttpContext.Session.SetString("ImageUrl", userInfo.Image.ToString());

                return RedirectToAction("Index", "Admin");
            }

            TempData["ErrorMessage"] = result.Message;
            return View("LoginUser", model);
        }

        // Đăng xuất

        public async Task<IActionResult> accountManagement()
        {
            var user = HttpContext.Session.GetString("JwtToken");
            var role = HttpContext.Session.GetString("UserRole");
            if (!string.IsNullOrEmpty(user))
            {
                // Kiểm tra quyền để điều hướng về đúng trang
                if (role == "Customer")
                {
                    var accManager = await _authService.GetAccManager();
                    ViewBag.CustomerLogin = accManager;

                    return View(accManager);
                }
                else // Là Admin hoặc Staff (User)
                {

                    return RedirectToAction("Privacy", "Home");
                }
            }
            return NotFound();

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Đã đăng xuất!";
            return RedirectToAction("Login");
        }



        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> UpdateProfile(UpdateCustomerRequest model, IFormFile avatar)
        {
            // 🔹 1. Lấy dữ liệu cũ (để lấy ảnh cũ)
            var oldData = await _authService.GetAccManager();
            string oldImage = oldData?.AccCustomer?.AnhDaiDien;

            string newImage = oldImage; // mặc định giữ ảnh cũ

            // 🔹 2. Nếu có upload ảnh mới
            if (avatar != null && avatar.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(avatar.FileName);

                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Avatar");

                // nếu chưa có folder thì tạo
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fullPath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await avatar.CopyToAsync(stream);
                }

                newImage = "~/img/Avatar/" + fileName;
            }

            // 🔹 3. Gọi API update
            var result = await _authService.UpdateProfile(
                model.Ten,
                model.Email,
                model.SDT,

                newImage
            );


            // 🔹 4. Nếu thành công → xoá ảnh cũ
            if (result)
            {
                if (!string.IsNullOrEmpty(oldImage) && avatar != null)
                {
                    string oldPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        oldImage.TrimStart('~','/')
                    );

                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                TempData["Success"] = "Cập nhật thành công!";
            }
            else
            {
                TempData["Error"] = "Cập nhật thất bại!";
            }

            return RedirectToAction("accountManagement");
        }

        [HttpPost]
        public async Task<IActionResult> CancelOrder(int id)
        {
            // Gọi Service xử lý
            var result = await _authService.CancelBillAsync(id);

            if (result)
            {
                TempData["Success"] = "Hủy đơn hàng thành công!";
            }
            else
            {
                TempData["Error"] = "Không thể hủy đơn hàng (Đơn đã giao hoặc đang giao).";
            }

            // Quay lại trang quản lý tài khoản
            return RedirectToAction("accountManagement");
        }
    }

    
}

