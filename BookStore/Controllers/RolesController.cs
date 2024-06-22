using BookStore.Data;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(ApplicationDbContext context,RoleManager<IdentityRole> RoleManager)
        {
            this.context = context;
            roleManager = RoleManager;
        }
        public async Task <IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();

            var rolesVM = roles.Select(role=> new RoleVM
            {
                Name =role.Name,
            }).ToList();
            return View(rolesVM);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


     [HttpPost]
     public async Task <IActionResult> Create(RoleVM rolevm)
        {
            if (!ModelState.IsValid)
            {
                return View(rolevm);
            }

            var role = await roleManager.CreateAsync(new IdentityRole(rolevm.Name));

            return RedirectToAction("Index");

        }

       
    }

   
}
