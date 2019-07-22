using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataStruct.Models
{

    public class Products
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Images { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        

        [ForeignKey("OrderDetails")]
        public int OrderDetailsId { get; set; }
        public OrderDetails OrderDetails { get; set; }

    }
}
