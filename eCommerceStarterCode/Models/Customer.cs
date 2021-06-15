using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
