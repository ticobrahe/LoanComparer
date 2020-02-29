using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanComparer.Data.Entities;
using LoanComparer.Data.Models.ViewModels;
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
        public async Task<IEnumerable<LoanerViewModel> >FindLoaner(HomeViewModel model)
        {
            var query = from loan in _context.Loaners
                where loan.MinimumAmount <= model.Amount &&
                      loan.Duration >= (int)model.Duration &&
                      loan.LoanType == model.LoanType
                select new LoanerViewModel
                    { CompanyName = loan.CompanyName, Id = loan.LoanerId, LoanType = loan.LoanType };
            return await query.ToListAsync();
        }

        public async Task<LoanerDetailViewModel> GetLoanDetail(int id)
        {
            var query = from loan in _context.Loaners
                join siteName in _context.LoanerWebsites
                    on loan.SiteId equals siteName.SiteId
                where loan.LoanerId == id
                select new LoanerDetailViewModel
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
    }
}
