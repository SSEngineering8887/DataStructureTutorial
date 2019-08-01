using Microsoft.AspNet.WebHooks;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DataStruct.Controllers
{
    [Route("api/[controller]")]
    public class StripeWebHookController : Controller
    {
        [HttpPost]
        public ActionResult Index()
        {
            // MVC3/4: Since Content-Type is application/json in HTTP POST from Stripe
            // we need to pull POST body from request stream directly
            Stream req = Request.InputStream;
            req.Seek(0, System.IO.SeekOrigin.Begin);

            string json = new StreamReader(req).ReadToEnd();
            StripeEvent stripeEvent = null;
            try
            {
                // as in header, you need https://github.com/jaymedavis/stripe.net
                // it's a great library that should have been offered by Stripe directly

                stripeEvent = new WebHookHandlerContext(new List<string>() { "Action" }).GetDataOrDefault<StripeEvent>();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Unable to parse incoming event");
            }

            if (stripeEvent == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Incoming event empty");

            switch (stripeEvent.EventType)
            {
                case "charge.refunded":
                    // do work
                    break;

                case "customer.subscription.updated":
                case "customer.subscription.deleted":
                case "customer.subscription.created":
                    // do work
                    break;
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
    
