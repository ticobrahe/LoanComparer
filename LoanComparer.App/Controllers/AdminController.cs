using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using LoanComparer.App.Models;
using LoanComparer.Data;
using LoanComparer.Data.Entities;
using LoanComparer.Data.Repositories.Interfaces;

namespace LoanComparer.App.Controllers
{
   
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public async Task<ActionResult> Index()
        {
            var loanRequests = await _adminRepository.GetAllLoanRequest();
            var stat = loanRequests.Aggregate(new Statistic(), (acc, req) => acc.Accumulate(req), acc => acc.Compute());
            ViewBag.stat = stat;
           var visit = await _adminRepository.LoanProviderVisitDetails();
            return View(visit);
        }

        public  ActionResult ProviderLink()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProviderLink(ProviderLinkViewModel model)
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

        public async Task<ActionResult> CreateProvider()
        {
            var provderSitemName = await _adminRepository.ProviderSiteName();
            var list = provderSitemName.Select(p => new SelectListItem { Value = p.SiteId.ToString(), Text = p.siteName }).ToList();
            ViewBag.ProviderLink = list;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateProvider(CreateProviderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loaner = _mapper.Map<Loaner>(model);
                loaner.SiteId = int.Parse(model.ProviderLink);
                _adminRepository.AddLoanProvider(loaner);
                await _adminRepository.Save();
                var provderSitemName = await _adminRepository.ProviderSiteName();
                var list = provderSitemName.Select(p => new SelectListItem { Value = p.SiteId.ToString(), Text = p.siteName }).ToList();
                ViewBag.ProviderLink = list;
                ViewBag.Message = "Provider created successfully";
            }
            
            return View();
        }

    }
}