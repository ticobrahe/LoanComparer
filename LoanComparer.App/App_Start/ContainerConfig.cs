using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using LoanComparer.App.Models;
using LoanComparer.Data.Entities;
using LoanComparer.Data.Repositories;
using LoanComparer.Data.Repositories.Interfaces;

namespace LoanComparer.App
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<LoanRepository>().As<ILoanRepository>().InstancePerRequest();
            builder.RegisterType<AdminRepository>().As<IAdminRepository>().InstancePerRequest();
            builder.RegisterType<LoanDbContext>().InstancePerRequest();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new LoanProfile());
            });

            builder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}