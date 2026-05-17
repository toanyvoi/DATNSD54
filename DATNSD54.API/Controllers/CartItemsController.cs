using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATNSD54.DAO.Data;
using DATNSD54.DAO.Models;
using Microsoft.AspNetCore.Authorization;

namespace DATNSD54.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CartItemsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public CartItemsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/CartItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCartItems()
        {
            return await _context.CartItems.ToListAsync();
        }

        // GET: api/CartItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItem>> GetCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            return cartItem;
        }

        // PUT: api/CartItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItem cartItem)
        {
            if (id != cartItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(cartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(id))
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

        // POST: api/CartItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Customer/Add")]
        public async Task<ActionResult<CartItem>> PostCartItem([FromQuery] int IdPd, int sl)
        {
            var userid = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userid == null)
            {
                return Unauthorized();
            }

            await _context.Bill.Where(b => b.Trang_Thai == 0 && b.Customer_ID == int.Parse(userid)).ExecuteDeleteAsync();
            var checkProduct = _context.ProductDetail.FirstOrDefault(i => i.Id == IdPd);
            if (checkProduct == null) {
                return BadRequest("productDetail không tồn tại");
            }
            var checkCartItem = _context.CartItems.FirstOrDefault(i => i.Cart_ID == int.Parse(userid) && i.Product_Detail_ID == IdPd);
            int checksl = 0;
            if (checkCartItem != null)
            {
                checkCartItem.So_Luong += sl;
                checksl = checkCartItem.So_Luong;

                if (checksl <= 0 || checksl > checkProduct.SL)
                {
                    return BadRequest($"Số lượng còn lại là {checkProduct.SL}");
                }

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCartItem", new { id = checkCartItem.ID }, checkCartItem);
            }
            else
            {
                CartItem cartItem = new CartItem()
                {
                Cart_ID = int.Parse(userid),
                Product_Detail_ID = IdPd,
                So_Luong = sl,
                Ngay_Tao = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
                checksl = cartItem.So_Luong;
                if (checksl <= 0 || checksl > checkProduct.SL)
                {
                    return BadRequest($"Số lượng còn lại là {checkProduct.SL}");
                }

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCartItem", new { id = cartItem.ID }, cartItem);
            }

           

            
        }

        // DELETE: api/CartItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {

            var userid = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userid == null)
            {
                return Unauthorized();
            }

            await _context.Bill.Where(b => b.Trang_Thai == 0 && b.Customer_ID == int.Parse(userid)).ExecuteDeleteAsync();
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.ID == id);
        }



    }
}
