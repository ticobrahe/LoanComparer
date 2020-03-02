﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
        // GET: Admin
        public async Task<ActionResult> Index()
        {
           var visit = await _adminRepository.LoanProviderVisitDetails();
            return View(visit);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateProvider()
        {
            return View();
        }

    }
}