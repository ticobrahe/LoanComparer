using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using LoanComparer.Data.Models;

namespace LoanComparer.App.Models
{
    public class LoanProfile: Profile
    {
        public LoanProfile()
        {
            CreateMap<HomeViewModel, LoanRequestInfo>().ReverseMap();
        }
    }
}