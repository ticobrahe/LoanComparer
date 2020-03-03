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
        public decimal AverageAmount { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public int Count { get; set; } = 0;
        public Decimal Total { get; set; }
        public decimal TotalDuration { get; set; }
        public decimal AverageDuration { get; set; }
        public Statistic()
        {
            Min = decimal.MaxValue;
            Max = decimal.MinValue;
        }
        public Statistic Accumulate(LoanRequest data)
        {
            TotalDuration += data.Duration;
            Total += data.Amount;
            Count += 1;
            Max = Math.Max(Max, data.Amount);
            Min = Math.Min(Min, data.Amount);

            return this;
        }

        public Statistic Compute()
        {
            AverageAmount = Total / Count;
            AverageDuration = Math.Floor(TotalDuration / Count);
            return this;
        }
    }
}
