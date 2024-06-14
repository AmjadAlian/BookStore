using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        public ApplicationDbContext context;

        public AuthorController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var Authors = context.Authors.ToList().Select(author=> new AuthorVM
            {
                Id = author.Id,
                Name = author.Name,
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now,
            }).ToList();
           
            
            return View(Authors);
        }

        [HttpGet]
        public IActionResult Create() {
        


            return View("Form");
        }

        [HttpPost]
        public IActionResult Create(AuthorVM authorVM) {

            if(!ModelState.IsValid)
            {
                return View("Form",authorVM);
            }
            var author = new Author()
            {
                Name = authorVM.Name,
            };
            try
            {
                context.Authors.Add(author);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Author Name Already Exists");
                return View("Form",authorVM);
            }
            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = context.Authors.Find(id);
            var authorVm = new AuthorVM()
            {
                Id = author.Id,
                Name=author.Name,
            };

            return View("Form", authorVm);

        }

        [HttpPost]
        public IActionResult edit(AuthorVM authorVM)
        {
            if(!ModelState.IsValid)
            {
                return View("Form",authorVM) ;
            }
            var author = context.Authors.Find(authorVM.Id);
            
            if(author == null)
            {
                return NotFound();
            }
            author.Name = authorVM.Name;
            author.LastUpdatedDate = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details (int id)
        {
            var author = context.Authors.Find(id);

            var authorVm = new AuthorVM()
            {
                Name = author.Name, LastUpdatedDate = DateTime.Now,CreatedDate = author.CreatedDate,Id = author.Id

            };
            return View("Details",authorVm);
        }

        public IActionResult Delete (int id)
        {

            var author = context.Authors.Find(id);
            context.Authors.Remove(author);
            context.SaveChanges();

            return Ok();

        }
    }

}
