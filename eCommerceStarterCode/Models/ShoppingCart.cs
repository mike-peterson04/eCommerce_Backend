using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace eCommerceStarterCode.Models
{
    public class ShoppingCart
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public Product User { get; set; }
        public int Quantity { get; set; }
    }
}
