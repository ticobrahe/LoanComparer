using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanComparer.App.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateProvider()
        {
            return View();
        }

    }
}