using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LoanComparer.Data.Models;
using LoanComparer.Data.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Paystack.Net.SDK.Transactions;

namespace LoanComparer.App.Controllers
{
    
    public class SubscriptionController : Controller
    {
        private readonly ILoanRepository _loanRepository;

        public SubscriptionController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        // GET: Subscription
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var plan = new SubscriptionPlan
            {
                Basic = 1000,
                Premium = 2500,
                Ultimate = 4300
            };
            return View(plan);
        }

        [Authorize]
        [HttpPost]
        public  async Task<ActionResult> Subscribe(int amount)
        {
            string secretKey = ConfigurationManager.AppSettings["PayStackSec"];
            var paystackTransactionAPI = new PaystackTransaction(secretKey);
            string email = User.Identity.GetUserName();
            TempData["amount"] = amount;
            var response = await paystackTransactionAPI.InitializeTransaction(email, amount*100, callbackUrl: "https://localhost:44329/Subscription/VerifyPayment");
            if (response.status)
            {
                TempData["paymentRef"] = response.data.reference;
                Response.AddHeader("Access-Control-Allow-Origin", "*");
                Response.AppendHeader("Access-Control-Allow-Origin", "*");
                return Redirect(response.data.authorization_url);
            }

            ViewBag.Error = "Payment was not successful, Kindly try again";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> VerifyPayment(string reference)
        {
            string secretKey = ConfigurationManager.AppSettings["PayStackSec"];
            var paystackTransactionAPI = new PaystackTransaction(secretKey);
            var response = await paystackTransactionAPI.VerifyTransaction(reference);

            if (response.status && (response.data.status == "success"))
            {
                string userId = User.Identity.GetUserId();
                int amount = Convert.ToInt32(TempData["amount"]);
                _loanRepository.CreateSubscription(userId, amount);
                await _loanRepository.Save();
                int providerId = (int)Session["providerId"];
                TempData["Message"] = "Payment was successful";
                return RedirectToAction("Details", "Loan", new { id = providerId });
            }
            ViewBag.Error = "Payment was not successful, Kindly try again";
            return RedirectToAction("Index");
        }
    }

}