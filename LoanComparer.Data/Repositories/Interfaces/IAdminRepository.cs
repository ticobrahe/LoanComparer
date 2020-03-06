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
        Task<IEnumerable<AdminHomeModel>> LoanProviderVisitDetails();
        Task<IEnumerable<LoanRequest>> GetAllLoanRequest();
        Task<AppUser> GetUserByEmail(string email);
        
    }
}
