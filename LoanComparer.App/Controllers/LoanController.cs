using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LoanComparer.Data.Models.ViewModels;
using LoanComparer.Data.Repositories.Interfaces;
using Microsoft.AspNet.Identity;

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
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            decimal amount = Convert.ToDecimal(Session["amount"]);
            int duration = Convert.ToInt16(Session["duration"]);
            var loanerDetail = await _loanRepository.GetLoanDetail(id);
            ViewBag.principalAmount = Convert.ToDecimal(Session["amount"]);
            var totalAmount = _loanRepository.TotalAmountToPay(loanerDetail.Rate, amount, duration);
            var repayment = _loanRepository.LoanRepayment(totalAmount, duration);
            ViewBag.totalAmount = totalAmount;
            ViewBag.repayment = repayment;
            return View(loanerDetail);
        }

        [Authorize]
        public async Task<ActionResult> AccessLoan(int id)
        {
            var loaner = await _loanRepository.GetLoanDetail(id);
            var userId = User.Identity.GetUserId();
            var isSubscribe = await _loanRepository.IsSubscribe(userId);
            if (!isSubscribe)
            {
                TempData["active"] = false;
                Session["providerId"] = id;
                return RedirectToAction("Index", "Subscription");
            } 
            _loanRepository.LoanProviderCount(userId, loaner.Id);
           await _loanRepository.Save();
            return Redirect(loaner.SiteName);
        }
    }
}