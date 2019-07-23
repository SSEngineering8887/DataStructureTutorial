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
            StripeConfiguration.ApiKey = Utility.Utility.GetFileData("Stripe_Key.txt");

            var cart = ShoppingCart.GetCart(this.HttpContext);
            var tempList = new List<SessionLineItemOptions>();
            
 

            for (int i = 0; i < cart.GetCartItems().Count; i++)
            {
                

                tempList.Add (new SessionLineItemOptions()
                {
                    Amount = (long)(cart.GetCartItems().ElementAt(i).Product.Price * 100),
                    Currency = "USD",
                    Description = $"Written by: {cart.GetCartItems().ElementAt(i).Product.Description}",
                    Quantity = cart.GetCartItems().ElementAt(i).Count,
                    Name = cart.GetCartItems().ElementAt(i).Product.Name,
                    
                });
               
                    
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                "card",
                },

                LineItems = tempList,
                SuccessUrl = "https://localhost:44321/Checkout/Success",
                CancelUrl = "https://localhost:44321/Checkout/Failure",
                 };



            var service = new SessionService();
            Session session = service.Create(options);


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
                    if (temp[j].Id == id[i])
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