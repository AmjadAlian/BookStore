using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoriesController : Controller
    {
        ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var Categories = context.categories.ToList();
            return View(Categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category CategoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", CategoryVM);
            }
            var category = new Category()
            {
                Name = CategoryVM.Name,
            };
            context.categories.Add(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            

            return View("Create");

        }
        [HttpPost]
        public IActionResult Edit(Category CategoryVM) {


            var category = context.categories.Find(CategoryVM.Id);
            category.Name = CategoryVM.Name;
            context.SaveChanges();

            return RedirectToAction("Index");
        
        }

        public IActionResult Delete(int id)
        {
            var category = context.categories.Find(id);

            context.Remove(category);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        
    }

   
}
   
