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
            try
            {
                var userId = User.FindFirstValue("id");
                if (userId == newVendor.UserId)
                {
                    return StatusCode(301, "You are already a registered vendor.");
                }
                _context.Vendors.Update(newVendor);
                _context.SaveChanges();
                return StatusCode(201, newVendor);
            }
            catch
            {
                return StatusCode(404, "Something went wrong!");
            }
        }

        [HttpGet, Authorize]
        public IActionResult Get()
        {
            var userid = User.FindFirstValue("id");
            Vendor vendor = _context.Vendors.Where(v => v.UserId == userid).SingleOrDefault();
            return StatusCode(201, vendor);
        }


    }
}
