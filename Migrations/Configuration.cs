using DataStruct.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using DataStruct.ViewModels;

namespace DataStruct.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataStruct.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //Get Questions
            //Names of the Question files
            using (context = new ApplicationDbContext())
            {
                //Add a default user
                context.Users.AddOrUpdate(new ApplicationUser()
                {
                    Id = 1,
                    FirstName = "D",
                    LastName = "Parker",
                    Address = "1 Address Ave",
                    City = "Test City",
                    State = "New York",
                    PostalCode = "33333",
                    Country = "United States",
                    Email = "Tester@gmail.com",
                    EmailConfirmed = true,
                    UserName = "Tester@gmail.com",
                    PhoneNumber = "212-333-3333",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = true,
                    AccessFailedCount = 3,
                    LockoutEnabled = true

                });

                context.SaveChanges();
            }

            //Seed Lectures
            List<string> lectureNames = new List<string>()
                    {
                      "Array",
                      "BinaryTree",
                      "Graph",
                      "HashTable",
                      "Heap",
                      "LinkedList",
                      "Queue",
                      "Set",
                      "Stack",
                      "Tree"

                    };

            string lecture = null;

            List<string> lectureFiles = new List<string>();

            foreach (var name in lectureNames)
            {
                lecture = System.IO.File.ReadAllText(AppContext.BaseDirectory + $@"\Lectures\{name}Lecture.html");
                lectureFiles.Add(lecture);
            }

            using (context = new ApplicationDbContext())
            {

                for (int i = 0; i < lectureFiles.Count(); i++)
                {

                    context.Lectures.AddOrUpdate(new Lectures()
                    {
                        LecturesId = i + 1,
                        Lecture = lectureFiles[i],
                        LectureName = lectureNames[i]

                    });

                }

                context.SaveChanges();
            }



            //Add Exam type data
            #region
            for (int i = 1; i < 11; i++)
            {
                using (context = new ApplicationDbContext())
                {
                    context.Exams.AddOrUpdate(new Exams()
                    {
                        ExamsId = i,
                        LectureId = i,
                        ExamType = ExamType.Array + i - 1,
                        ExamName = Enum.GetName(typeof(ExamType), (ExamType.Array + i - 1))


                    });

                    context.SaveChanges();
                }

            }
            #endregion

            //Get Questions
            //Names of the Question files
            #region
            List<string> lectureQuestionNames = new List<string>()
            {
                    "Array",
                    "BinaryTree",
                    "Graph",
                    "HashTable",
                    "Heap",
                    "LinkedList",
                    "Queue",
                    "Set",
                    "Stack",
                    "Tree"
             };

            int counter = 1;
            int nextExam = 1;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(AppContext.BaseDirectory + $@"\Questions\Array.txt");

            //Go through each file and read it line by line
            foreach (var name in lectureQuestionNames)
            {
                file = new System.IO.StreamReader(AppContext.BaseDirectory + $@"\Questions\{name}.txt");

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Equals("~"))
                    {
                        counter++;
                    }

                    else
                    {
                        using (context = new ApplicationDbContext())
                        {
                            context.Questions.AddOrUpdate(new Questions()
                            {
                                Question = line,
                                QuestionsId = counter,
                                ExamsId = nextExam


                            });

                            context.SaveChanges();
                        }
                    }

                }
                nextExam++;
            }
            #endregion

            //Names of the Answer files
            List<string> lectureAnswerNames = new List<string>()
                 {
                    "Array",
                    "BinaryTree",
                    "Graph",
                    "HashTable",
                    "Heap",
                    "LinkedList",
                    "Queue", 
                    "Set",
                    "Stack",
                    "Tree"
             };

            int questionCounter = 1;
            int answerIdCounter = 1;
            string answerLine;


            System.IO.StreamReader answerFile = new System.IO.StreamReader(AppContext.BaseDirectory + $@"\Answers\ArrayAnswers.txt");
            bool isCorrect = false;

            //Go through each file and read it line by line
            foreach (var name in lectureAnswerNames)
            {
                answerFile = new System.IO.StreamReader(AppContext.BaseDirectory + $@"\Answers\{name}Answers.txt");

                while ((answerLine = answerFile.ReadLine()) != null)
                {

                    //Next Question        
                    if (answerLine.Equals("~"))
                    {
                        questionCounter++;
                    }
                    else if (String.IsNullOrWhiteSpace(answerLine))
                    {

                    }
                    //If its the correct answer
                    else if (answerLine.StartsWith("*"))
                    {
                        answerLine = answerLine.Remove(0, 1);
                        isCorrect = true;
                        using (context = new ApplicationDbContext())
                        {
                            context.Answers.AddOrUpdate(new Answers()
                            {
                                AnswersId = answerIdCounter,
                                Answer = answerLine,
                                QuestionsId = questionCounter,
                                IsCorrect = isCorrect,

                            });

                            answerIdCounter++;
                            context.SaveChanges();
                        }

                    }
                    else
                    {
                        isCorrect = false;
                        using (context = new ApplicationDbContext())
                        {
                            context.Answers.AddOrUpdate(new Answers()
                            {
                                AnswersId = answerIdCounter,
                                Answer = answerLine,
                                QuestionsId = questionCounter,
                                IsCorrect = isCorrect,

                            });

                            answerIdCounter++;
                            context.SaveChanges();
                        }


                    }


                }

                file.Close();
            }

            using (context = new ApplicationDbContext())
            {
                context.Orders.AddOrUpdate(new Orders()
                {
                    OrderId = 1,
                    UserId = 1,
                    OrderCreated = DateTime.Now,
                    Status = "Shipped",
                    Total = 99.99m,
                    Email = "Tester@gmail.com",
                    Address ="123 Test Street"
                    
                    
                    
                });

                context.SaveChanges();
            }

            using (context = new ApplicationDbContext())
            {
                context.OrderDetails.AddOrUpdate(new OrderDetails()
                {
                    Amount = 1,
                    Currency = "USD",
                    Description ="Book",
                    OrderId = 1,
                    Quantity = 1,
                    UnitPrice = 99.99m,
                    OrderDetailsId = 1,
                    ProductId =1



                });

                context.SaveChanges();
            }
            ProductViewModel productViewModel = new ProductViewModel();

            using (context = new ApplicationDbContext())
            {
               
               foreach (var item in productViewModel.products)
                {
                    context.Products.AddOrUpdate(new Products()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = item.Price,
                        Images = item.Images,
                        Description = item.Description,
                        OrderDetailsId = 1
                    });
                }

                context.SaveChanges();
            }


        }
    }
}