using eCommerceStarterCode.Data;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerceStarterCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            
            try
            {
                var categories = _context.Categories;
                return StatusCode(200, categories);
            }
            catch
            {
                return StatusCode(400, "No Products exist");

            }
        }

        // POST api/<ValuesController>
        [HttpPost,Authorize]
        public IActionResult Post([FromBody] Category value)
        {
            try
            {
                _context.Categories.Update(value);
                _context.SaveChanges();
                return StatusCode(201, value);
            }
            catch
            {
                return StatusCode(400, "no match found");
            }
        }


        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}"),Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var category = _context.Categories.Where(c => c.Id == id).SingleOrDefault();
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return StatusCode(200, category);
            }
            catch
            {
                return StatusCode(400, "no match found");
            }
        }
    }
}
