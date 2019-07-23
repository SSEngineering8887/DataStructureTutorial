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
    //[Authorize]
    public class CheckoutController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();


        public ActionResult Success()
        {
            

            return View();
        }

        public ActionResult Failure()
        {
           

            return View();
        }

    }
}