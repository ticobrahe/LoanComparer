using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoanComparer.App.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LoanComparer.App.Controllers
{
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
        //private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Roles
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
        //public ActionResult Edit(string roleName)
        //{
        //    var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase))
        //        .FirstOrDefault();

        //    return View(thisRole);
        //}

        //
        // POST: /Roles/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(IdentityRole role)
        //{
        //    try
        //    {
        //        context.Entry(role).State = System.Data.Entity.EntityState.Modified;
        //        context.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //#endregion



        //public ActionResult ManageUsers()
        //{
        //    // prepopulat roles for the view dropdown
        //    var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
        //        new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name}).ToList();
        //    ViewBag.Roles = list;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult RoleAddToUser(string UserName, string RoleName)
        //{
        //    var user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase))
        //        .FirstOrDefault();
        //    var manager = new ApplicationUserManager(new UserStore<User>(context));
        //    manager.AddToRole(user.Id, RoleName);

        //    ViewBag.ResultMessage = "Role created successfully !";

        //    // populate roles for the view dropdown
        //    var list = context.Roles.OrderBy(r => r.Name).ToList()
        //        .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name}).ToList();
        //    ViewBag.Roles = list;

        //    return View("ManageUsers");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GetRoles(string UserName)
        //{
        //    if (!string.IsNullOrWhiteSpace(UserName))
        //    {
        //        User user = context.Users
        //            .Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase))
        //            .FirstOrDefault();
        //        var manager = new ApplicationUserManager(new UserStore<User>(context));

        //        ViewBag.RolesForThisUser = manager.GetRoles(user.Id);

        //        // prepopulat roles for the view dropdown
        //        var list = context.Roles.OrderBy(r => r.Name).ToList()
        //            .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name}).ToList();
        //        ViewBag.Roles = list;
        //    }

        //    return View("ManageUsers");
        //}

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