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
   public class Questions
   {

        [Key]
        public int QuestionsId { get; set; }
        public string Question { get; set; }

        [ForeignKey("Exams")]
        public int ExamsId { get; set; }
        public virtual Exams Exams { get; set; }
        //A question can have many answers
        public virtual ICollection<Answers> Answers { get; set; }

   }
    
}