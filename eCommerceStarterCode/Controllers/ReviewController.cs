using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceStarterCode.Models;
using eCommerceStarterCode.Data;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerceStarterCode.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/<ReviewController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var productReviews = _context.Reviews.Where(r => r.ProductId == id);

                return StatusCode(200, productReviews);

            }
            catch
            {
                return StatusCode(400, "product with id "+id+" does not exist");
            }
            
        }

        // POST api/<ReviewController>
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Review value)
        {
            try { 
                _context.Reviews.Add(value);
                _context.SaveChanges();
                return StatusCode(201, _context.Reviews.Where(r => r.Id == value.Id).SingleOrDefault());
            }
            catch
            {
                return StatusCode(400, value);
            }
        }

        // PUT api/<ReviewController>/5
        //[HttpPut("{id}"), Authorize]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ReviewController>/5
        //[HttpDelete("{id}"), Authorize]
        //public void Delete(int id)
        //{

        //}
    }
}
