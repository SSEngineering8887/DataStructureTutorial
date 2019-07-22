using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataStruct.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public long Amount { get; set; }
        public long Quantity { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Orders Order { get; set; }
        public int ProductId { get; set; }
        public virtual List<Products> Product { get; set; }
    }
}