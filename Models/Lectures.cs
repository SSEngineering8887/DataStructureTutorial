using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataStruct.Models
{
    public class Lectures
    {
        [Key]
        public int LecturesId { get; set; }
        public string Lecture { get; set; }
        public string LectureName { get; set; }

        public virtual List<Exams> Exams { get; set; }
    }
}