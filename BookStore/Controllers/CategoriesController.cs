using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public IActionResult Create(CategoryVM CategoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", CategoryVM);
            }
            var category = new Category()
            {
                Name = CategoryVM.Name,
            };
            try
            {
                context.categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name","Category Name Already Exists");
                return View(CategoryVM);
            }
           
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = context.categories.Find(id);

            var ViewModel = new CategoryVM
            {
                Id = id,
                Name = category.Name,
            };

            return View("Create", ViewModel);

        }
        [HttpPost]
        public IActionResult Edit(CategoryVM CategoryVM) {

            if (!ModelState.IsValid)
            {
                return  View("Create", CategoryVM);
            }
           
          var category = context.categories.Find(CategoryVM.Id);
               
            if (category is null)
            {
                return NotFound();
            }
            category.Name = CategoryVM.Name;
            category.LastUpdatedDate = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Details (int id)
        {
            var category = context.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            var categoryVM = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
                LastUpdatedDate = category.LastUpdatedDate,

            };

            return View("Details", categoryVM);

        }

        public IActionResult Delete(int id)
        {
            var category = context.categories.Find(id);
            if(category is null)
            {
                return NotFound();
            }
            context.categories.Remove(category);
            context.SaveChanges();

            return Ok();
        }
       
        
    }

   
}
   
