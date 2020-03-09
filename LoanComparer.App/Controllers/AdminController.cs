using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LoanComparer.App.Models;
using LoanComparer.Data;
using LoanComparer.Data.Entities;
using LoanComparer.Data.Repositories.Interfaces;

namespace LoanComparer.App.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<ActionResult> Index()
        {
            var loanRequests = await _adminRepository.GetAllLoanRequest();
            var stat = loanRequests.Aggregate(new Statistic(), (acc, req) => acc.Accumulate(req), acc => acc.Compute());
            ViewBag.stat = stat;
           var visit = await _adminRepository.LoanProviderVisitDetails();
            return View(visit);
        }

        public ActionResult ProviderLink()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProviderLink(ProviderLink model)
        {
            if (ModelState.IsValid)
            {
                var loanProvider = new LoanerWebsite
                {
                    siteName = model.SiteName
                };
                _adminRepository.AddProvider(loanProvider);
               await _adminRepository.Save();
               ViewBag.Message = "Proider link created succesfully";
               return View();
            }

            return View();
        }

    }
}