﻿using DataStruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.Entity.Migrations;
using PayPal.Api;
using DataStruct.Services;
using Stripe;
using System.IO;

namespace DataStruct.Controllers
{
    [RequireHttps]
    [Authorize]
    public class HomeController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();
        public List<string> lectureList = new List<string>();
        public ExamType ExamType = new ExamType();
       
        public ActionResult Index(int? id = 1)
        {
            if (!id.HasValue)
            {
                RedirectToAction("Cards", "Home");
            }
            else
            {
                //Model to receive lectures
                Lectures lectureModel = context.Lectures.Find(id);
                lectureList.Add(lectureModel.Lecture);

                return View("index", lectureList.ToList());
            }

            return View();
            
        }
        public ActionResult About()
        {
           
            ViewBag.Message = "Your application description page.";
            StripeConfiguration.ApiKey = "sk_test_2LmLOaJqH0omNzjuF1u3Yl5a00FSZT9CV2";

            var options = new ChargeCreateOptions
            {
                Amount = 999,
                Currency = "usd",
                Source = "tok_visa",
                ReceiptEmail = "jenny.rosen@example.com",
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            return View();
        }
        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Cards()
        {
            
           //Get lectures from the db
            var query = context.Lectures.Select(d => new { d.Lecture });

            //ToList() the results
            var lectureDescription = query.ToList();
            var tempList = new List<string>();

            foreach (var item in lectureDescription)
            {
                tempList.Add(item.Lecture);
            }
          
            ViewBag.Lecture = tempList;
            ViewBag.lectureQuestionNames = Utility.Utility.GetLectureNames();

            return View("Cards", Utility.Utility.GetLecturePhotoAddresses());
        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<PayPal.Api.Item>()
            };
            //Adding Item Details like name, currency, price etc  
            itemList.items.Add(new PayPal.Api.Item()
            {
                name = "Item Name comes here",
                currency = "USD",
                price = "1",
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = "3", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }


    }
}



