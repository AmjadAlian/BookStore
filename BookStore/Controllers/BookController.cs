using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BookController (ApplicationDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var bookVM = context.Books.Include(book => book.Author).Include(book => book.Categories).ThenInclude(book => book.category).ToList().Select(book=> new BookVM
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author.Name,
                    publisher = book.publisher,
                    PublishDate = book.PublishDate,
                    ImgUrl = book.ImgUrl,
                    Categories = book.Categories.Select(book=>book.category.Name).ToList(),

                }).ToList();
                

            /*var bookVMs = bookVM.Select(book => new BookVM
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author.Name,
                publisher = book.publisher,
                PublishDate = book.PublishDate,
                ImgUrl = book.ImgUrl,
                Categories = book.Categories.Select(book=> book.category.Name ).ToList(),

            }).ToList();
            */
          

            return View(bookVM);
            
        }

        [HttpGet]
        public IActionResult Create() {


            var authors = context.Authors.OrderBy(author => author.Name).ToList();
            var categories = context.categories.OrderBy(category => category.Name).ToList();
            var authorVMList = authors.Select(author=> new SelectListItem
            {
                Value = author.Id.ToString(),
                Text = author.Name,
            }).ToList();
            var categoryList = categories.Select(category=> new SelectListItem
            {
                Value = category.Id.ToString(),
                Text = category.Name,
            }).ToList();

            var VM = new BookFormVM()
            {
                Authors = authorVMList,
                Categories = categoryList,
            };

            return View("Create", VM);
        
        }

        [HttpPost]
        public IActionResult Create(BookFormVM DataVM)
        {

           

            if (!ModelState.IsValid)
            {

            return View("Create", DataVM);
            }
            string ImgName = null;
            if (DataVM.ImgUrl != null)
            {
                 ImgName = Path.GetFileName(DataVM.ImgUrl.FileName);
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/img/books/", ImgName);
                var stream = System.IO.File.Create(path);
                DataVM.ImgUrl.CopyTo(stream);
            }

            var bookData = new Book
            {
                Title = DataVM.Title ,
                Description = DataVM.Description,
                AuthorId = DataVM.AuthorId,
                publisher = DataVM.publisher,
                PublishDate = DataVM.PublishDate,
                ImgUrl = ImgName,
                Categories = DataVM.SelectedCategories.Select(id =>new BookCategory 
                
                {
                  CategoryId = id,
                }).ToList(),

            };

            context.Books.Add(bookData);
            context.SaveChanges();
            return RedirectToAction("Index");



        }


        public IActionResult Delete (int id)
        {

            var book = context.Books.Find(id);
            if(book == null)
            {
                return NotFound();
            }

           
            var path = Path.Combine( webHostEnvironment.WebRootPath ,"img/books",book.ImgUrl);
            if (System.IO.File.Exists(path))
            {

                System.IO.File.Delete(path);
            }

            context.Books.Remove(book);
            context.SaveChanges ();

            return RedirectToAction("Index");
        }
    }
}
