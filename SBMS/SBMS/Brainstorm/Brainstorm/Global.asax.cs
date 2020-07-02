using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Brainstorm.Model.Model;
using Brainstorm.Models;
using AutoMapper;

namespace Brainstorm
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //initialize
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Category,CategoryViewModel>();
                cfg.CreateMap<CategoryViewModel,Category>();

                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<CustomerViewModel, Customer>();

                cfg.CreateMap<Supplier, SupplierViewModel>();
                cfg.CreateMap<SupplierViewModel, Supplier>();

                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, Product>();

                //cfg.CreateMap<Purchase, PurchaseViewModel>();
                //cfg.CreateMap<PurchaseViewModel, Purchase>();

                //cfg.CreateMap<PurchaseProduct, PurchaseProductViewModel>();
                //cfg.CreateMap<PurchaseProductViewModel, PurchaseProduct>();
            } );
        }
    }
}
