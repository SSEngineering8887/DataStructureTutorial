using DataStruct.Models;
using DataStruct.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Web.Helpers;
using DataStruct.Utility;

namespace DataStruct.Controllers
{
    public class ExamController : Controller
    {
        
        
        private string pw = "password";

        public int totalCorrectAnswers = 0;
        //Db object
        public ApplicationDbContext context = new ApplicationDbContext();
        //List of 
        public List<ExamViewModel> examViewModels = new List<ExamViewModel>();
        public static ExamStats examStats = new ExamStats();

        // GET: Exam
        public ActionResult Index(int? id = 1)
        {

            
            //Retrieve all the questions
            var questionQuery = from question in context.Questions
                                where question.ExamsId == id
                                orderby question.QuestionsId
                                select question;

            var test = questionQuery.ToList();
            test.Shuffle();
           
            //Retrieve all the answers for each question
            var answerQuery = from answer in context.Answers
                              //orderby answer.QuestionModelId, answer.AnswerModelId
                              select answer;
            var answerTest = answerQuery.ToList();
            answerTest.Shuffle();




            //For every question add all of its answers and whether theyre correct
            foreach (var q in test)
            {

                examViewModels.Add(new ExamViewModel()
                {
                    Question = q.Question,
                    Answers = answerTest.Where(m => m.QuestionsId == q.QuestionsId).Select(m => m.Answer).ToList(),
                    IsCorrect = answerTest.Where(m => m.QuestionsId == q.QuestionsId).Select(m => m.IsCorrect).ToList(),
                    
                });

              

            }
            return View("Index", examViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection fc)
        {
           
            int routeId = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            int? userId = int.Parse(User.Identity.GetUserId());
            int amountCorrect = 0;
            int totalQuestions = 0;
            var hasBoolValue = true;

            //Checking whether answer is correct
            for (int i = 0; i < fc.Keys.Count; i++)
            {
                var valueProviderResult = fc.ToValueProvider().GetValue(fc.AllKeys[i]);
                //Test whether the value is a bool

                var result = bool.TryParse(valueProviderResult.AttemptedValue, out hasBoolValue);

                if (result == true)
                {
                    totalQuestions++;
                }

                if (valueProviderResult.AttemptedValue == "True")
                {
                    amountCorrect++;
                }
                hasBoolValue = true;
            }




            ExamStats e = context.ExamStats.Add(new ExamStats()
            {
                ExamDate = DateTime.Now,
                ExamScore = ((double)amountCorrect / totalQuestions * 100),
                CorrectAnswers = amountCorrect,
                ExamStatsId = userId.Value,
                UserId = 1,
                ExamsId = routeId



            });

            if (ModelState.IsValid)
            {
                context.SaveChanges();
                return RedirectToAction("Results", "Exam", null);

            }
            return View();



        }

        public ActionResult Results()
        {
            var id = User.Identity.GetUserId<int>();
            ViewBag.UserName = context.Users.Where(x => x.Id == id).First().UserName;

            var statsQuery = from c in context.ExamStats
                             where c.UserId == id
                             select c;

            examStats = new ExamStats()
            {
                ExamScore = statsQuery.OrderByDescending(y => y.ExamDate).Select(x => x.ExamScore).FirstOrDefault(),
                CorrectAnswers = statsQuery.OrderByDescending(y => y.ExamDate).Select(x => x.CorrectAnswers).FirstOrDefault(),
                ExamDate = statsQuery.OrderByDescending(y => y.ExamDate).Select(x => x.ExamDate).FirstOrDefault(),
            };


            return View(examStats);
        }

        public ActionResult SendEmail()
        {

         string pw = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\password.txt";

            string email = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\email.txt";


            var id = User.Identity.GetUserId<int>();


            ViewBag.UserName = context.Users.Where(x => x.Id == id).First().UserName;
            try
            {
                //Configuring webMail class to send emails  
                //gmail smtp server  
                WebMail.SmtpServer = "smtp.gmail.com";
                //gmail port to send emails  
                WebMail.SmtpPort = 587;
                WebMail.SmtpUseDefaultCredentials = true;
                //sending emails with secure protocol  
                WebMail.EnableSsl = true;
                //EmailId used to send emails from application  
                WebMail.UserName = email;
                WebMail.Password = pw;

                //Sender email address.  
                WebMail.From = email;

                //Send email  
                WebMail.Send(to: email, subject: "Results: " + DateTime.Now, body: $"User Name: {ViewBag.UserName}<br>Score:{examStats.ExamScore}<br>Date:{examStats.ExamDate}", isBodyHtml: true);
                ViewBag.Status = "Email Sent Successfully.";
            }
            catch (Exception)
            {
                ViewBag.Status = "Problem while sending email, Please check details.";

            }

            return View();
        }

    }
}


