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

        // POST REQUEST TO CREATE NEW CUSTOMER
        [HttpPost("new_customer"), Authorize]
        public IActionResult Post([FromBody] Customer NewCustomer)
        {
            _context.Customers.Add(NewCustomer);
            _context.SaveChanges();
            return StatusCode(201, NewCustomer);
        }

        // GET REQUEST TO RETRIEVE ALL CUSTOMERS via IEnumerable action
        [HttpGet("all-customers"), Authorize]
        public IEnumerable<Customer> GetCustomers()
        {
            var Customers = _context.Customers;
            return Customers;
        }

        // GET REQUEST TO RETRIEVE CUSTOMER BY ID
        [HttpGet("{id}"), Authorize]
        public IActionResult Get(string id)
        {
            // Query db for customer with matching id 
            var customer = _context.Customers.Where(c => c.UserId == id).SingleOrDefault();

            // Return customer with 'Ok' 200 status code
            return Ok(customer);
        }

        // PUT REQUEST TO UPDATE CUSTOMER PROPERTIES
        [HttpPut("update/{id}")]
        public IActionResult Put(int id, [FromBody] Customer UpdatedCustomer)
        {
            // Query db for customer that matches id from param
            var customer = _context.Customers.Where(c => c.Id == id).SingleOrDefault();

            // Update returned customer properties with UpdatedCustomer changes
            customer.FirstName = UpdatedCustomer.FirstName;

            // Save Changes to db
            _context.Customers.Update(customer);
            _context.SaveChanges();

            // Return Ok status code and updated customer object
            return StatusCode(200, customer); 
        }
    }
}
