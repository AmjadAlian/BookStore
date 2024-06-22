using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
       

        public UserController(ApplicationDbContext context , RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task <IActionResult> Index()
        {
            var users =await userManager.Users.ToListAsync();
            var userVm = new List<ApplicationUserVM>();
          
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);

                var uservm = new ApplicationUserVM
                {
                    UserName = user.UserName,
                    PhoneNumber= user.PhoneNumber,
                    Email = user.Email,
                    Address = user.Address,
                    Roles = roles.ToList(),
                };
                userVm.Add(uservm);
            }

            return View(userVm);
        }

        [HttpGet]
        [Authorize]
        public async Task <IActionResult> Create()
        {
            var roles =await roleManager.Roles.ToListAsync();

            var viewModel = new ApplicationCreateUserVM {

                Roles = roles.Select(role=> new SelectListItem
                {
                    Value = role.Name,
                    Text = role.Name
                }).ToList(),
            };

            
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationCreateUserVM userVM)
        {
            if(!ModelState.IsValid)
            {
                return View (userVM);
            }

            var user = new ApplicationUser {
            
                UserName = userVM.UserName,
                PhoneNumber = userVM.PhoneNumber,
                Address = userVM.Address,
                Email = userVM.Email,
            };

         var result = await userManager.CreateAsync(user,userVM.PasswordHash);

            if(!result.Succeeded)
            {
                return View(userVM);
            }

           await userManager.AddToRolesAsync(user, userVM.SelectedRoles);

            return RedirectToAction("Index");

        }
    }
}
