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
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Get()
        {
            try
            {
                var products = _context.Products.Include(p=>p.Category);
                return StatusCode(200,products);
            }
            catch 
            {
                return StatusCode(400, "No Products exist");

            }
        }

        // GET api/<ProductController>/5
        [HttpGet("search/"+"{searchTerm}")]
        public IActionResult Get(string searchTerm)
        {
            try
            {

                searchTerm = searchTerm.ToUpper();
                var products = _context.Products.Include(p => p.Category).Where(p => p.Name.ToUpper().Contains(searchTerm)|| p.Category.Name.ToUpper().Contains(searchTerm));
                    return StatusCode(200,products);
                
            }
            catch
            {
                return StatusCode(400, "no match found");
            }
            
            
        }
        [HttpGet("{id}"), Authorize]
        public IActionResult Get(int id)
        {
            try
            {
              
                    var product = _context.Products.Where(p => p.Id == id).SingleOrDefault();
                    return StatusCode(200, product);
                
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
            try
            {
                _context.Products.Update(newproduct);
                _context.SaveChanges();
                return StatusCode(201, newproduct);
            }
            catch
            {
                return StatusCode(400, "no match found");
            }
            
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}"), Authorize]
        public IActionResult Put(int id, [FromBody] Product changedProduct)
        {
            try
            {
                var original = _context.Products.Where(p => p.Id == id).SingleOrDefault();

                original.Name = changedProduct.Name;
                original.Price = changedProduct.Price;
                original.Description = changedProduct.Description;
                original.CategoryId = changedProduct.CategoryId;
                original.VendorId = changedProduct.VendorId;
                _context.Products.Update(original);
                _context.SaveChanges();
                return StatusCode(200, original);
            }
            catch
            {
                return StatusCode(400, "no match found");
            }

            
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.Where(p => p.Id == id).SingleOrDefault();
                _context.Products.Remove(product);
                _context.SaveChanges();
                return StatusCode(200, product);
            }
            catch
            {
                return StatusCode(400, "no match found");
            }
            
        }
    }
}
