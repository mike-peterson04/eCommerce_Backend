﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceStarterCode.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey ("UserId")]
        public User User { get; set; }
    }
}
