using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanComparer.Data.Models;
using LoanComparer.Data.Models.ViewModels;

namespace LoanComparer.Data.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<LoanerViewModel>> FindLoaner(HomeViewModel model);
        Task<LoanerDetailViewModel> GetLoanDetail(int id);
        decimal TotalAmountToPay(decimal rate, decimal amount, int duration);
        IEnumerable<RepaymentDetails> LoanRepayment(decimal totalAmount,int duration);
        Task<bool> Save();
        Task<bool> IsSubscribe(string userId);
        void LoanProviderCount(string userId, int loanerId);
    }
}
