using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using eCommerceStarterCode.Models;
using eCommerceStarterCode.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
