using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using LoanComparer.Data.Entities;
using LoanComparer.Data.Models;

namespace LoanComparer.App.Models
{
    public class LoanProfile: Profile
    {
        public LoanProfile()
        {
            CreateMap<HomeViewModel, LoanRequestInfo>().ReverseMap();
            CreateMap<HomeViewModel, LoanRequest>();
            CreateMap<CreateProviderViewModel, Loaner>();
        }
    }
}