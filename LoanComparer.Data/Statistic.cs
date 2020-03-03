using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanComparer.Data.Entities;

namespace LoanComparer.Data
{
    public class Statistic
    {
        public decimal Average { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public int Count { get; set; } = 0;
        public Decimal Total { get; set; }
        public Statistic()
        {
            Min = decimal.MaxValue;
            Max = decimal.MinValue;
        }
        public Statistic Accumulate(LoanRequest data)
        {
            Total += data.Amount;
            Count += 1;
            Max = Math.Max(Max, data.Amount);
            Min = Math.Min(Min, data.Amount);

            return this;
        }

        public Statistic Compute()
        {
            Average = Total / Count;
            return this;
        }
    }
}
