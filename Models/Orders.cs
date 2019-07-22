using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataStruct.Models
{
    public class Orders
    {

        [Key]
        [ForeignKey("Charges")]
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderCreated { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }

       
        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public List<Charges> Charges { get; set; }
    }
}



