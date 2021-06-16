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

namespace eCommerceStarterCode.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("new_customer"), Authorize]
        public IActionResult Post([FromBody] Customer NewCustomer)
        {
            _context.Customers.Add(NewCustomer);
            _context.SaveChanges();
            return StatusCode(201, NewCustomer);
        }

        [HttpGet("all-customers"), Authorize]
        public IEnumerable<Customer> GetCustomers()
        {
            var Customers = _context.Customers;
            return Customers;
        }

        [HttpGet("{id}"), Authorize]
        public IActionResult Get(int id)
        {
            var customer = _context.Customers.Where(c => c.Id == id).SingleOrDefault();
            return Ok(customer);
        }
    }
}
