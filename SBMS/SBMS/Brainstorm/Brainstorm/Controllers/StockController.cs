using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brainstorm.Model.Model;
using Brainstorm.Models;
using Brainstorm.BLL.BLL;
using AutoMapper;
namespace Brainstorm.Controllers
{
    public class StockController : Controller
    {
        
        SaleManager _saleManager = new SaleManager();
        SaleViewModel _saleViewModel = new SaleViewModel();
       
        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        PurchaseManager _purchaseManager = new PurchaseManager();
        PurchaseViewModel _purchaseViewModel = new PurchaseViewModel();
        public ActionResult GetStock()
        {
            _purchaseViewModel.CategorySelectListItems = _categoryManager
                                   .GetAll()
                                   .Select(c => new SelectListItem()
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name
                                   }).ToList();

            ViewBag.Category = _purchaseViewModel.CategorySelectListItems;
            return View();
        }

        public ActionResult GetStockByDate(int ProductId, DateTime startDate, DateTime endDate)
        {
            
            var Product = _productManager.GetAll().ToList();
            var Category = _categoryManager.GetAll().ToList();
            var PurchaseProducts = _purchaseManager.GetPrevious().ToList();
            var Purchase = _purchaseManager.GetAll().ToList();
            
            var q= (from PurPro in PurchaseProducts
                    join Pur in Purchase on PurPro.Purchase.Id equals Pur.Id
                    join Prod in Product on PurPro.ProductId equals Prod.Id
                     join Cat in Category on Prod.CategoryId equals Cat.Id
                     orderby Pur.Date.Date 
                where Prod.Id == ProductId 
               &&
            (Pur.Date.Date >= startDate.Date && endDate.Date >= Pur.Date.Date)
                    select new Stock{ Id = PurPro.Id, Code = Prod.Code, Product = Prod.Name, Category = Cat.Name, ReorderLevel = Prod.ReorderLevel,Expdate=PurPro.ExpDate,OpeningBalance= GetOpeningBal(ProductId, Pur.Date.Date),In=stockIn(ProductId, Pur.Date.Date),Out= stockout(ProductId, Pur.Date.Date),ClosingBalance= GetOpeningBal(ProductId, Pur.Date.Date)+(stockIn(ProductId, Pur.Date.Date)- stockout(ProductId, Pur.Date.Date)) }).ToList();
            ViewBag.all = q;
            return PartialView("Stock/_GetStockByDate");
        }
        public int stockIn(int ProductId, DateTime Date)
        {
            var PurchaseProducts = _purchaseManager.GetPrevious().ToList();
            var Purchase = _purchaseManager.GetAll().ToList();
            int quant;
            try
            {
                var q =( from PurPro in PurchaseProducts
                         join Pur in Purchase on PurPro.Purchase.Id equals Pur.Id
                         where PurPro.ProductId == ProductId && (Pur.Date.Date == Date.Date)
                         select PurPro.Quantity).Sum();
                quant = q ;

            }
            catch
            {
                quant = 0;
            }


            return quant;
        }
        public int stockout(int ProductId, DateTime Date)
        {
            var SaleProducts = _saleManager.GetPrevious().ToList();
            var Sales = _saleManager.GetAll().ToList();
            int quant;
            try
            {
                var q = (from salePro in SaleProducts
                         join sal in Sales on salePro.Sale.Id equals sal.Id
                         where salePro.ProductId == ProductId && (sal.Date.Date == Date.Date)
                         select salePro.Quantity).Sum();
                quant = q;

            }
            catch
            {
                quant = 0;
            }

            return quant;
        }
        public int GetOpeningBal(int ProductId, DateTime startDate)
        {
            var Product = _productManager.GetAll().ToList();
            var Category = _categoryManager.GetAll().ToList();
            var PurchaseProducts = _purchaseManager.GetPrevious().ToList();
            var Purchase = _purchaseManager.GetAll().ToList();
            int quant;
            try
            {
                var q = (from PurPro in PurchaseProducts
                         join Pur in Purchase on PurPro.Purchase.Id equals Pur.Id
                         where PurPro.ProductId == ProductId && (Pur.Date.Date < startDate.Date)
                         select PurPro.Quantity).Sum();
                quant = q- GetSale(ProductId,startDate);

            }
            catch
            {
                quant = 0;
            }


            return quant;
        }
        public int GetSale(int ProductId, DateTime startDate)
        {
       
            var SaleProducts = _saleManager.GetPrevious().ToList();
            var Sales= _saleManager.GetAll().ToList();
            int quant;
            try
            {
                var q = (from salePro in SaleProducts
                         join sal in Sales on salePro.Sale.Id equals sal.Id
                         where salePro.ProductId == ProductId && (sal.Date.Date < startDate.Date)
                         select salePro.Quantity).Sum();
                quant = q;

            }
            catch
            {
                quant = 0;
            }

            return quant;
        }



        public class Stock
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Product { get; set; }
            public string Category { get; set; }
            public int ReorderLevel { get; set; }
            public DateTime Expdate { get; set; }
            public int OpeningBalance { get; set; }
            public int In{ get; set; }
            public int Out{ get; set; }
            public int ClosingBalance { get; set; }
        }

    }
}