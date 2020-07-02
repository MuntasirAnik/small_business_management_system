using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brainstorm.Model.Model;
using Brainstorm.Models;
using Brainstorm.BLL.BLL;

namespace Brainstorm.Controllers
{
    public class SalesReportController : Controller
    {
        SaleManager _saleManager = new SaleManager();
        SaleViewModel _saleViewModel = new SaleViewModel();

        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        PurchaseViewModel _purchaseViewModel = new PurchaseViewModel();
        // GET: SalesReport
        public ActionResult GetSalesReport()
        {
          
            return View();
        }

        public ActionResult GetSalesReportByDate( DateTime startDate, DateTime endDate)
        {
            var Product = _productManager.GetAll().ToList();
            var Category = _categoryManager.GetAll().ToList();
            var SaleProducts = _saleManager.GetPrevious().ToList();
            var Sale = _saleManager.GetAll().ToList();
            var PurchaseProducts = _purchaseManager.GetPrevious().ToList();
            var Purchase = _purchaseManager.GetAll().ToList();

            var q = (from salPro in SaleProducts
                     join sal in Sale on salPro.Sale.Id equals sal.Id
                     join pur in Purchase on sal.Date.Date equals pur.Date.Date
                     join purpro in PurchaseProducts on new{ pur.Id,salPro.ProductId} equals new{ purpro.Purchase.Id,purpro.ProductId }
                     join Prod in Product on salPro.ProductId equals Prod.Id
                     join Cat in Category on Prod.CategoryId equals Cat.Id
                     orderby sal.Date.Date
                     where 
                      (sal.Date.Date >= startDate.Date && endDate.Date >= sal.Date.Date)
                     select new SalesReport { Id = sal.Id, Code = Prod.Code, Name = Prod.Name, Category = Cat.Name, SoldQuantity = salPro.Quantity, CostPrice = purpro.UnitPrice, SalesPrice = salPro.MRP, Profit= salPro.MRP- purpro.UnitPrice,TotalProfit= salPro.Quantity*(salPro.MRP - purpro.UnitPrice) }).ToList();
            ViewBag.x = q;
            return PartialView("Report/_GetSalesReport");

        }

        public class SalesReport
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public int SoldQuantity { get; set; }
            public double CostPrice { get; set; }
            public double SalesPrice { get; set; }
            public double Profit { get; set; }
            public double TotalProfit { get; set; }


        }
       
        //public double GetProfit(int SaleID,int PurchID)
        //{
        //    var Product = _productManager.GetAll().ToList();
        //    var Category = _categoryManager.GetAll().ToList();
        //    var SaleProducts = _saleManager.GetPrevious().ToList();
        //    var Sale = _saleManager.GetAll().ToList();
        //    var PurchaseProducts = _purchaseManager.GetPrevious().ToList();
        //    var Purchase = _purchaseManager.GetAll().ToList();
        //    double profit;
        //    try
        //    {
        //        var q =( from salPro in SaleProducts
        //                join sal in Sale on salPro.Sale.Id equals sal.Id
        //                where sal.Id == SaleID
        //                select salPro.MRP).Sum();
        //        profit = q - GetCost(PurchID);
        //    }


        //    catch
        //    {
        //        profit = 0;
        //    }
        //    return profit;
        //}

        //public double GetCost(int PurchID)
        //{

        //    var PurchaseProducts = _purchaseManager.GetPrevious().ToList();
        //    var Purchase = _purchaseManager.GetAll().ToList();
        //    double cost;
        //    try
        //    {
        //        var q = (from purPro in PurchaseProducts
        //                 join pur in Purchase on purPro.Purchase.Id equals pur.Id
        //                 where pur.Id == PurchID
        //                 select purPro.UnitPrice).Sum();
        //        cost = q ;
        //    }


        //    catch
        //    {
        //        cost = 0;
        //    }
        //    return cost;
        //}



    }
}