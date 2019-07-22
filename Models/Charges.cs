using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataStruct.Models
{
    public class Charges
    {
        public Charges()
        {
           
        }

        [Key]
        public int ChargesId { get; set; }
        public long Amount { get; set; }
        public string AuthorizationCode { get; set; }
        public DateTime ChargeDate { get; set; }
        public string Currency { get; set; }
        public string ReceiptEmail { get; set; }
        public string ReceiptNumber{ get; set; }

        //Foreign Keys/Navigation Properties
       
        [ForeignKey("Orders")]
        public int OrderId { get; set; }
        public Orders Orders { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int PaymentIntentId { get; set; }
       


    }
}