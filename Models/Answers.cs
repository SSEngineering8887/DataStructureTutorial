using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataStruct.Models
{
   public class Answers
    {
        [Key]
        public int AnswersId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

       
        //There is only one answer per question
        [ForeignKey("Questions")]
        [Required]
        public int QuestionsId { get; set; }
        public virtual Questions Questions { get; set; }

    }
}