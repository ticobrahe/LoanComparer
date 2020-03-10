using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanComparer.Data.Entities;
using LoanComparer.Data.Models;

namespace LoanComparer.Data.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        void AddProvider(LoanerWebsite provider);
        void AddLoanProvider(Loaner provider);
        Task<bool> Save();
        Task<IEnumerable<AdminHomeModel>> LoanProviderVisitDetails();
        Task<IEnumerable<LoanRequest>> GetAllLoanRequest();
        Task<IEnumerable<LoanerWebsite>> ProviderSiteName();
    }
}
