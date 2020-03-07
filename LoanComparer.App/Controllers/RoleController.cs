using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LoanComparer.App.Models;
using LoanComparer.Data.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LoanComparer.App.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;

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
        public async Task<ActionResult> CreateUserRole(ManageUserViewModel model)
        {
            var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
           
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    ViewBag.Error = "User not found";
                    ViewBag.Roles = list;
                    return View();
                }
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
                if (await manager.IsInRoleAsync(user.Id, model.RoleName))
                {
                    ViewBag.Error = $"User already in {model.RoleName} role";
                    ViewBag.Roles = list;
                    return View();
                }
                manager.AddToRole(user.Id, model.RoleName);
                ViewBag.Message = "Role created successfully !";
                ViewBag.Roles = list;
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetUserRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetUserRole(ManageUserViewModel model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    ViewBag.Error = "User not found";
                    return View();
                }
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
                var userRoles = manager.GetRoles(user.Id);
                if (userRoles.Count == 0)
                {
                    ViewBag.Error = "User does not belong to any role";
                    return View();
                }

                ViewBag.Data = userRoles;
                return View();
            }
            catch
            {
                return View();
            }
           
        }

        public ActionResult DeleteUserRole()
        {
            var list = _context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
                new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> DeleteUserRole(ManageUserViewModel model)
        {
            var list = _context.Roles.OrderBy(r => r.Name).ToList()
                .Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                ViewBag.Error = "User not found";
                ViewBag.Roles = list;
                return View();
            }
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            if (await manager.IsInRoleAsync(user.Id, model.RoleName))
            {
                manager.RemoveFromRole(user.Id, model.RoleName);
                ViewBag.Message = "User removed from this role successfully !";
            }
            else
            {
                ViewBag.Error = "This user doesn't belong to selected role.";
            }
            ViewBag.Roles = list;

            return View();
        }

    }
}