using DataStruct.Models;
using DataStruct.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataStruct.Controllers
{
    public class StoreController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public string Index()
        {
            return "Hello from Store.Index()";
        }
        //
        // GET: /Store/Browse
        public string Browse()
        {
            return "Hello from Store.Browse()";
        }
      
        public ActionResult Details()
        {
            var query = from row in context.Products
                        select row;

            ProductViewModel productModel = new ProductViewModel();
            productModel.products = new List<Products>();

            foreach (var item in query)
            {
                productModel.products.Add(item);
            }
            
            return View(productModel.products.ToList());
        }
    }
}