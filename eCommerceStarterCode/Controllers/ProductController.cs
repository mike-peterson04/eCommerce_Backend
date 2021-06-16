using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using eCommerceStarterCode.Models;
using eCommerceStarterCode.DataTransferObjects;
using eCommerceStarterCode.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerceStarterCode.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var products = _context.Products;
            return products;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}"), Authorize]
        public IActionResult Get(int id=-1 , [FromBody] string name="noName")
        {
            try
            {
                if (id < 0 && name == "noName")
                {
                    return StatusCode(400, "no match found"); 
                }
                else if (id < 0)
                {
                    name = name.ToUpper();
                    var product = _context.Products.Where(p => p.Name.ToUpper() == name).SingleOrDefault();
                    return StatusCode(200,product);
                }
                else
                {
                    var product = _context.Products.Where(p => p.Category == id).SingleOrDefault();
                    return StatusCode(200, product);
                }
            }
            catch
            {
                return StatusCode(400, "no match found");
            }
            
            
        }

        // POST api/<ProductController>/new
        [HttpPost("new"), Authorize]
        public IActionResult Post([FromBody] Product newproduct)
        {

            _context.Products.Update(newproduct);
            _context.SaveChanges();
            return StatusCode(201, newproduct);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}"), Authorize]
        public IActionResult Put(int id, [FromBody] Product changedProduct)
        {

            var original = _context.Products.Where(p => p.Id == id).SingleOrDefault();

            original.Name = changedProduct.Name;
            original.Price = changedProduct.Price;
            original.Description = changedProduct.Description;
            original.Category = changedProduct.Category;
            original.VendorId = changedProduct.VendorId;
            _context.Products.Update(original);
            _context.SaveChanges();
            return StatusCode(200, original);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Where(p => p.Id == id).SingleOrDefault();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return StatusCode(200, product);
        }
    }
}
