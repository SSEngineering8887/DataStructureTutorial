using DataStruct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DataStruct.ViewModels
{
    public class Books
    {
        public Dictionary<string, string> books;
        public Books()
        {
            books = new Dictionary<string, string>()
            {

         { "The Pragmatic Programmer: From Journeyman to Master","Andy Hunt" },
         { "Clean Code: A Handbook of Agile Software Craftsmanship","Robert C. Martin" },
         { "Code Complete", "Steve McConnel"},
         {"Design Patterns: Elements of Reusable Object-Oriented Software","Erich Gamma  " },
         {"Refactoring: Improving the Design of Existing Code","Martin Fowler" },
         {"The Mythical Man-Month: Essays on Software Engineering","Frederick P. Brooks Jr." },
         {"JavaScript: The Good Parts","Douglas Crockford"},
         {"Structure and Interpretation of Computer Programs (MIT Electrical Engineering and Computer Science)","Harold Abelson" },
         {"The C Programming Language","Brian W. Kernighan"},
         { "Head First Design Patterns", "Eric Freeman" }

            };
        }
    }

    public class ProductViewModel
    {
        public List<Products> products = new List<Products>();
        public ProductViewModel()
        {
            Books b = new Books();

           

            for (int i = 1; i <= b.books.Count; i++)
            {
                products.Add(new Products()
                {
                    Id = i,
                    Price = Utility.Utility.GenerateRandomPrice(),
                    Images = $@"PhotosBooks/Book{i}.png",
                    Name = b.books.Keys.ElementAt(i-1),
                    Description = b.books.Values.ElementAt(i-1)

                });
            }
        }

        

    }
}