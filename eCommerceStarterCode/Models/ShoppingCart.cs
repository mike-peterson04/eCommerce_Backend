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
        public int ProductId { get; set; }

        [Key, Column(Order = 1)]
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer User { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
