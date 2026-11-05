using HW_05_0C.Models;
using HW_05_0C.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_05_0C.Controllers
{
    public class HomeController : Controller
    {
        private static List<Author> authors = new List<Author>
        {
            new Author { Id = 1, Name = "J.K. Rowling", Country = "UK" },
            new Author { Id = 2, Name = "George R.R. Martin", Country = "USA" }
        };

        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Harry Potter", Price = 25.50m, AuthorId = 1 },
            new Book { Id = 2, Title = "Game of Thrones", Price = 30.00m, AuthorId = 2 },
            new Book { Id = 3, Title = "Chamber of Secrets", Price = 20.00m, AuthorId = 1 }
        };

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel vm = new HomeViewModel();
            vm.Authors = authors;
            vm.Books = new List<BookCardViewModel>();

            foreach (var b in books)
            {
                BookCardViewModel card = new BookCardViewModel();
                card.Id = b.Id;
                card.Title = b.Title;
                card.Price = b.Price;

                foreach (var a in authors)
                {
                    if (a.Id == b.AuthorId)
                    {
                        card.AuthorName = a.Name;
                        break;
                    }
                }
                vm.Books.Add(card);
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddBook(string Title, decimal Price, int AuthorId)
        {
            if (string.IsNullOrEmpty(Title))
            {
                return RedirectToAction("Index");
            }

            Book newBook = new Book();
            newBook.Title = Title;
            newBook.Price = Price;
            newBook.AuthorId = AuthorId;

            if (books.Count > 0)
            {
                newBook.Id = books[books.Count - 1].Id + 1;
            }
            else
            {
                newBook.Id = 1;
            }

            books.Add(newBook);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string Name, string Country)
        {
            Author newAuthor = new Author();
            newAuthor.Name = Name;
            newAuthor.Country = Country;

            if (authors.Count > 0)
            {
                newAuthor.Id = authors[authors.Count - 1].Id + 1;
            }
            else
            {
                newAuthor.Id = 1;
            }

            authors.Add(newAuthor);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            Book bookToRemove = null;
            foreach (var b in books)
            {
                if (b.Id == id)
                {
                    bookToRemove = b;
                    break;
                }
            }

            if (bookToRemove != null)
            {
                books.Remove(bookToRemove);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
