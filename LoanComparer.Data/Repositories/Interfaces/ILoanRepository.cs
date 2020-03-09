using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanComparer.Data.Entities;
using LoanComparer.Data.Models;

namespace LoanComparer.Data.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<LoanerDto>> FindLoaner(LoanRequestInfo model);
        Task<LoanerDetailDto> GetLoanDetail(int id);
        decimal TotalAmountToPay(decimal rate, decimal amount, int duration);
        IEnumerable<RepaymentDetails> LoanRepayment(decimal totalAmount,int duration);
        Task<bool> Save();
        Task<bool> IsSubscribe(string userId);
        void LoanProviderCount(string userId, int loanerId);
        void CreateSubscription(string userId, int amount);
        void CreateLoanRequest(LoanRequest requestInfo);
    }
}
