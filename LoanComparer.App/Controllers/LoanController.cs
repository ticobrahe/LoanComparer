using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LoanComparer.Data.Models.ViewModels;
using LoanComparer.Data.Repositories.Interfaces;

namespace LoanComparer.App.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanRepository _loanRepository;

        // GET: Loan
        public LoanController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Loaner()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Loaner(HomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Session["amount"] = model.Amount;
                Session["duration"] = model.Duration;
                var loaners = await _loanRepository.FindLoaner(model);
                ViewBag.Count = loaners.Count();
                return View(loaners);
            }
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Details(int id)
        {
            var loaner = await _loanRepository.GetLoanDetail(id);
            return View();
        }
    }
}