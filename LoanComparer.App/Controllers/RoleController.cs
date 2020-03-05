using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LoanComparer.App.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LoanComparer.App.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public RoleController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View(_context.Roles.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                _context.Roles.Add(new IdentityRole()
                {
                    Name = collection["roleName"]
                });
                _context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return View("Create");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(string Id)
        {
            var role = _context.Roles.Find(Id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                _context.Entry(role).State = EntityState.Modified;
                _context.SaveChanges();
                TempData["ResultMessage"] = "Role Updated successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "Role update failed";
                return RedirectToAction("Index");
            }
        }
        
        public ActionResult Delete(string Id)
        {
            try
            {
                var role = _context.Roles.Find(Id);
                _context.Roles.Remove(role);
                _context.SaveChanges();
                TempData["ResultMessage"] = $"{role.Name} Role was Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch 
            {
                TempData["error"] = "Deletion failed";
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult CreateUserRole()
        {
            var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
                new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUserToRole(ManageUserViewModel model)
        {
            
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    TempData["Error"] = "User not found";
                }
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
                if (await manager.IsInRoleAsync(user.Id, model.RoleName))
                {
                    TempData["Error"] = $"User already in {model.RoleName} role";
                    return RedirectToAction("ManageUsers");
                }
                manager.AddToRole(user.Id, model.RoleName);

                TempData["Message"] = "Role created successfully !";
                return RedirectToAction("ManageUsers");
            }
            catch (Exception)
            {
                return RedirectToAction("ManageUsers");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRole(ManageUserViewModel model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    TempData["Error"] = "User not found";
                    return View("ManageUsers");
                }
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
                var userRoles = manager.GetRoles(user.Id);
                if (userRoles == null)
                {
                    TempData["Error"] = "User does not belong to any role";
                    return View("ManageUsers");
                }

                TempData["Data"] = userRoles;
                return View("ManageUsers");
            }
            catch
            {
                return View("ManageUsers");
            }
           
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        //{
        //    var manager = new ApplicationUserManager(new UserStore<User>(context));

        //    User user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase))
        //        .FirstOrDefault();

        //    if (manager.IsInRole(user.Id, RoleName))
        //    {
        //        manager.RemoveFromRole(user.Id, RoleName);
        //        ViewBag.ResultMessage = "Role removed from this user successfully !";
        //    }
        //    else
        //    {
        //        ViewBag.ResultMessage = "This user doesn't belong to selected role.";
        //    }

        //    // prepopulat roles for the view dropdown
        //    var list = context.Roles.OrderBy(r => r.Name).ToList()
        //        .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name}).ToList();
        //    ViewBag.Roles = list;

        //    return View("ManageUsers");
        //}

    }
}