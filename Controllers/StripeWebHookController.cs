using Microsoft.AspNet.WebHooks;
using Stripe;
using Stripe.Checkout;
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
    public class StripeWebHook : Controller
    {
        // You can find your endpoint's secret in your webhook settings
        const string secret = "whsec_ZUhmAu0e62SlkK5Sc7XtFSGgIxXuh7lZ";

        [HttpPost]
        public void Index()
        {
            var json = new StreamReader(HttpContext.Request.InputStream).ReadToEnd();
            
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], secret);

                // Handle the checkout.session.completed event
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;

                    // Fulfill the purchase...
                    HandleCheckoutSession(session);
                }

            }
            catch (StripeException e)
            {
                
                //To do - Log the exception
            }
        }

       

        private void HandleCheckoutSession(Session session)
        {
            var tester = "I was Called";
            tester += tester;
        }
    }
}



