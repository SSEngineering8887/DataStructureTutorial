using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataStruct.Models
{
    public class ExamStats
    {
        [Key]
        public int ExamStatsId { get; set; }
        public double ExamScore { get; set; }
        public DateTime ExamDate { get; set; }
        public int CorrectAnswers { get; set; }


        [ForeignKey("Exams")]
        public int ExamsId { get; set; }
        public Exams Exams { get; set; }

        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


    }
    
}