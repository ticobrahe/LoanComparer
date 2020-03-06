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
    public class AdminRepository: IAdminRepository
    {
        private readonly LoanDbContext _context;

        public AdminRepository(LoanDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AdminHomeModel>> LoanProviderVisitDetails()
        {
            var query = from loan in _context.Loaners
                join visit in _context.Visits
                    on loan.LoanerId equals visit.LoanerId into visitGroup
                from d in visitGroup.DefaultIfEmpty()
                select new AdminHomeModel
                {
                    CompanyName = loan.CompanyName,
                    LoanType = loan.LoanType,
                    UniqueVisit = d == null ? 0:  d.UniqueCount,
                    Visits = d == null ? 0 :  d.VisitCount
                };
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<LoanRequest>> GetAllLoanRequest()
        {
            return await _context.LoanRequests.ToListAsync();
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
