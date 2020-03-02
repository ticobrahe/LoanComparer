using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
            return View();
        }

        [Authorize]
        [HttpPost]
        public  async Task<ActionResult> Subscribe()
        {
            string secretKey = ConfigurationManager.AppSettings["PayStackSec"];
            var paystackTransactionAPI = new PaystackTransaction(secretKey);
            string email = User.Identity.GetUserName();
            var response = await paystackTransactionAPI.InitializeTransaction(email, 100000, callbackUrl: "https://localhost:44329/Subscription/VerifyPayment");
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
                _loanRepository.CreateSubscription(userId);
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