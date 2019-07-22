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
using Stripe;
using Stripe.Checkout;

namespace DataStruct.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();
        public Session session = new Session();


        public ActionResult UIndex(string sessionId)
        {
            sessionId = session.Id;

            return View();
        }

       
        public ActionResult Index()
        {
            StripeConfiguration.ApiKey = "sk_test_2LmLOaJqH0omNzjuF1u3Yl5a00FSZT9CV2";
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var options = new SessionCreateOptions
            {
                SuccessUrl = "https://localhost:44376/Checkout/StripeSuccess",
                CancelUrl = "https://localhost:44376/Checkout/StripeFailure",
                CustomerEmail = "customer@example.com",
                BillingAddressCollection = "required",
                PaymentMethodTypes = new List<string>
                {
                   "card",
                },

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                    Name = cart.GetCartItems().ElementAt(1).Product.Name,
                    Description = "Comfortable cotton t-shirt",
                    Amount = 1500,
                    Currency = "usd",
                    Quantity = 2,
                    },

                    new SessionLineItemOptions
                    {
                    Name = "Lesson 1",
                    Description = "Arrays",
                    Amount = 25,
                    Currency = "usd",
                    Quantity = 1,
                    Images = new List<string>(){ @"https://www.google.com/imgres?imgurl=https%3A%2F%2Fwww.geeksforgeeks.org%2Fwp-content%2Fuploads%2FArray-In-C.png&imgrefurl=https%3A%2F%2Fwww.geeksforgeeks.org%2Fintroduction-to-arrays%2F&docid=J0FChqgdAYrNUM&tbnid=TfHp12IrH0DYTM%3A&vet=10ahUKEwivnamV74jjAhVlhOAKHVU1CPUQMwh-KAAwAA..i&w=800&h=378&bih=657&biw=1366&q=array&ved=0ahUKEwivnamV74jjAhVlhOAKHVU1CPUQMwh-KAAwAA&iact=mrc&uact=8"
                    }
                }
            }
        };

           
            var service = new SessionService();
            Session session = new Session();
            session = service.Create(options);



            return View(session);


        }
        public ActionResult Success()
        {
            

            return View("StripeSuccess");
        }
        public ActionResult Failure()
        {
           

            return View("StripeFailure");
        }

    }
}