using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanComparer.Data.Entities;
using LoanComparer.Data.Models;
using LoanComparer.Data.Repositories.Interfaces;

namespace LoanComparer.Data.Repositories
{
    public class LoanRepository: ILoanRepository
    {
        private readonly LoanDbContext _context;

        public LoanRepository(LoanDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<LoanerDto> >FindLoaner(LoanRequestInfo model)
        {
            var query = from loan in _context.Loaners
                where loan.MinimumAmount <= model.Amount &&
                      loan.Duration >= (int)model.Duration &&
                      loan.LoanType == model.LoanType
                select new LoanerDto
                    { CompanyName = loan.CompanyName, Id = loan.LoanerId, LoanType = loan.LoanType };
            return await query.ToListAsync();
        }

        public async Task<LoanerDetailDto> GetLoanDetail(int id)
        {
            var query = from loan in _context.Loaners
                join siteName in _context.LoanerWebsites
                    on loan.SiteId equals siteName.SiteId
                where loan.LoanerId == id
                select new LoanerDetailDto
                {
                    CompanyName = loan.CompanyName,
                    LoanType = loan.LoanType,
                    Rate = loan.Rate,
                    Id = loan.LoanerId,
                    SiteName = siteName.siteName,
                    Terms = loan.Terms
                };
            return await query.FirstOrDefaultAsync();
        }

        public decimal TotalAmountToPay(decimal rate, decimal amount, int duration)
        {
            decimal amountLeft = amount;
            decimal totalAmount = 0;
            for (int month = duration; month > 0; month--)
            {
                decimal currentTotalAmount = (amountLeft * rate / 100) + amountLeft;
                decimal amountToPay = currentTotalAmount / month;
                amountLeft = currentTotalAmount - amountToPay;
                totalAmount += amountToPay;
            }

            return totalAmount;
        }

        public IEnumerable<RepaymentDetails> LoanRepayment(decimal totalAmount, int duration)
        {
            var repayments = new List<RepaymentDetails>();
            decimal monthlyPayment = totalAmount / duration;
            for (int i = 1; i <= duration; i++)
            {
                var amountLeft = totalAmount - monthlyPayment * i;
                var paymentPercentage = (monthlyPayment * i) / totalAmount;
                var repaymentdetail = new RepaymentDetails
                {
                    AmountLeft = Math.Round(amountLeft, 2),
                    Duration = i,
                    MonthlyPayment = monthlyPayment,
                    PaymentPercentage = Math.Round(paymentPercentage, 2)
                };
                repayments.Add(repaymentdetail);
            }

            return repayments;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> IsSubscribe(string userId)
        {
            var subscribedUser = await _context.Subscribes.Where(s => s.UserId == userId && s.Active == true).FirstOrDefaultAsync();
            if (subscribedUser == null)
            {
                return false;
            }

            if (subscribedUser.EndDate < DateTime.Today)
            {
                subscribedUser.Active = false;
                _context.Entry(subscribedUser).State = EntityState.Modified;
                await Save();
                return false;
            }

            return true;
        }

        public void LoanProviderCount(string userId, int loanerId)
        {
            var visit = _context.Visits.Where(c => c.UserId == userId && c.LoanerId == loanerId).FirstOrDefault();
            if (visit == null)
            {
                var newVisit = new Visit
                {
                    LoanerId = loanerId,
                    VisitCount = 1,
                    UniqueCount = 1,
                    UserId = userId
                };
                _context.Visits.Add(newVisit);
            }
            else
            {
                visit.VisitCount += 1;
                _context.Entry(visit).State = EntityState.Modified;
            }
        }

        public void CreateSubscription(string userId, int amount)
        {
            var subscription = new Subscribe()
            {
                UserId = userId,
                Active = true,
                Amount = amount,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30)
            };
            _context.Subscribes.Add(subscription);
        }

        public void CreateLoanRequest(LoanRequest requestInfo)
        {
            _context.LoanRequests.Add(requestInfo);
        }
    }
}
