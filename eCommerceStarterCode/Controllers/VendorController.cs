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
    [Route("api/vendor")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public VendorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create"), Authorize]
        public IActionResult Post([FromBody]Vendor newVendor)
        {
            _context.Vendors.Update(newVendor);
            _context.SaveChanges();
            return StatusCode(201, newVendor);
        }


    }
}
