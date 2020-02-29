using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanComparer.Data.Models.ViewModels;

namespace LoanComparer.Data.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<LoanerViewModel>> FindLoaner(HomeViewModel model);
    }
}
