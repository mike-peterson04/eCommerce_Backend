using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using eCommerceStarterCode.Models;
using eCommerceStarterCode.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;

namespace eCommerceStarterCode.Controllers
{
    [Route("api/shoppingcart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add"), Authorize]
        public IActionResult Post([FromBody] ShoppingCart newItem)
        {
            try
            {
                newItem.Product = _context.Products.Where(p => p.Id == newItem.ProductId).SingleOrDefault();
                newItem.User = _context.Customers.Where(u => u.Id == newItem.CustomerId).SingleOrDefault();
                var exists = _context.ShoppingCarts.Where(sc => sc.CustomerId == newItem.CustomerId && sc.ProductId == newItem.ProductId).Select(sc => sc.Quantity).SingleOrDefault();
                Console.WriteLine(exists);
                if (exists > 0)
                {
                    newItem.Quantity += exists;
                    _context.ShoppingCarts.Update(newItem);
                }
                else
                {
                    _context.ShoppingCarts.Add(newItem);
                }
                _context.SaveChanges();
                return StatusCode(201, newItem);
            }
            catch
            {
                return StatusCode(404, "Something went wrong!");
            }
        }

        [HttpGet(), Authorize]
        public IEnumerable<ShoppingCart> Get()
        {
            var userid = User.FindFirstValue("id");
            var user = _context.Users.Find(userid);
            var ShoppingCart = _context.ShoppingCarts.Include(p=>p.Product).Where(p => p.User.UserId == user.Id);
            return ShoppingCart;
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var userId = User.FindFirstValue("id");
                var customerId = _context.Customers.Where(c => c.UserId == userId).SingleOrDefault();
                var ShoppingCartItem = _context.ShoppingCarts.Where(p => p.User.Id == customerId.Id && p.ProductId == id).SingleOrDefault();
                if (ShoppingCartItem.Quantity > 1)
                {
                    ShoppingCartItem.Quantity--;
                    _context.ShoppingCarts.Update(ShoppingCartItem);
                    _context.SaveChanges();
                }
                else
                {
                    _context.ShoppingCarts.Remove(ShoppingCartItem);
                    _context.SaveChanges();
                }
                
                return StatusCode(200, ShoppingCartItem);
            }
            catch
            {
                string error = "No shopping Cart Found.";
                return StatusCode(404, error);
            }
            
        }
    }
}
