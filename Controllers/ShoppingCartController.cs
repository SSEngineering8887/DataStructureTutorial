using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataStruct.Models;
using DataStruct.ViewModels;
using Stripe;
using Stripe.Checkout;

namespace DataStruct.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
      
        // GET: /ShoppingCart/
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


            //var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            ViewBag.session = session;
            // Return the view
            return View(viewModel);
        }

      
        public ActionResult AddToCart(int[] id)
        {
            
            var query = from p in dbContext.Products
                        select p;

            List<Products> temp = query.ToList();

            List<Products> products = new List<Products>();
           if (id == null)
            {
                id = new int[temp.Count];
            }

            for (int i = 0; i < id.Length; i++)
            {
                for (int j = 0; j < temp.Count; j++)
                {
                    if(temp[j].Id == id[i])
                    {
                        products.Add(temp[j]);
                    }
                }
            }


           
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            foreach (var item in products)
            {
                cart.AddToCart(item);
            }
            

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
       
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the product to display confirmation
            string product = dbContext.Carts
                .Single(item => item.Id == id).Product.Name;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(product) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}