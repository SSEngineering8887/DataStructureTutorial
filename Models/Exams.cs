using System;
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
    public enum ExamType
    {
        placeHolder,
        Array,
        BinaryTree,
        Graph,
        HashTable,
        Heap,
        LinkedList,
        Queue,
        Set,
        Stack,
        Tree,
        
       
       
       
    }
    public class Exams
    {
        [Key]
        public int ExamsId { get; set; }
        public ExamType ExamType  { get; set; }
        public string ExamName { get; set; }

        [ForeignKey("Lectures")]
        public int LectureId { get; set; }
        public virtual Lectures Lectures { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
        public virtual ICollection<ExamStats> ExamStats { get; set; }

    }
}