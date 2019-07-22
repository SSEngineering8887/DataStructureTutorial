using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataStruct.Models
{
    public class Logger
    { 
        [Key]
        public int Id { get; set; }
        public string  Exception { get; set; }
        public string  UserName { get; set; }
        public DateTime ExceptionDate { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}