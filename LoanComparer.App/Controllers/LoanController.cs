using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using LoanComparer.App.Models;
using LoanComparer.Data.Entities;
using LoanComparer.Data.Models;
using LoanComparer.Data.Repositories.Interfaces;
using Microsoft.AspNet.Identity;

namespace LoanComparer.App.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        // GET: Loan
        public LoanController(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
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
                var createLoanRequest = _mapper.Map<LoanRequest>(model);
                var loanRequest = _mapper.Map<LoanRequestInfo>(model);
                var loaners = await _loanRepository.FindLoaner(loanRequest);
                _loanRepository.CreateLoanRequest(createLoanRequest);
                await _loanRepository.Save();
                ViewBag.Count = loaners.Count();
                return View(loaners);
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var userId = User.Identity.GetUserId();
            var isSubscribe = await _loanRepository.IsSubscribe(userId);
            if (!isSubscribe)
            {
                
                TempData["active"] = false;
                Session["providerId"] = id;
                return RedirectToAction("Index", "Subscription");
            }

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
            _loanRepository.LoanProviderCount(userId, id);
           await _loanRepository.Save();
            return Redirect(loaner.SiteName);
        }
    }
}