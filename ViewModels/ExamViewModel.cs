using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataStruct.Models;

namespace DataStruct.ViewModels
{
    public class ExamViewModel
    {
      
        public ExamType ExamType { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public List<bool> IsCorrect { get; set; }
        public int QuestionModelId { get; set; }
        public int AnswerQuestionModelId { get; set; }
       
    }
}