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
    public class SaleController : Controller
    {

        SaleManager _saleManager = new SaleManager();
        SaleViewModel _saleViewModel = new SaleViewModel();
        CustomerManager _customerManager = new CustomerManager();
        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        PurchaseManager _purchaseManager = new PurchaseManager();

        // GET: Sale
        [HttpGet]
        public ActionResult Add()
        {


            _saleViewModel.CustomerSelectListItems = _customerManager
                                    .GetAll()
                                    .Select(c => new SelectListItem()
                                    {
                                        Value = c.Id.ToString(),
                                        Text = c.Name
                                    }).ToList();

            ViewBag.Customer = _saleViewModel.CustomerSelectListItems;

            _saleViewModel.CategorySelectListItems = _categoryManager
                                   .GetAll()
                                   .Select(c => new SelectListItem()
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name
                                   }).ToList();

            ViewBag.Category = _saleViewModel.CategorySelectListItems;
            //var ProductSelectListItems = _productManager
            //                        .GetAll()
            //                        .Select(c => new SelectListItem()
            //                        {
            //                            Value = c.Id.ToString(),
            //                            Text = c.Name
            //                        }).ToList();


            return View();
        }
   


        [HttpPost]
        public ActionResult Add(Sale sale,string GrandTotal)
        {
            string message = "";

            double grandtotal = Convert.ToDouble(GrandTotal);
        
            if (ModelState.IsValid)
            {

                if (_saleManager.Add(sale))
                    {
                    message = "Saved";
                   setLoyalty(sale.CustomerId,grandtotal);
                }
                    else
                    {
                    message = "Not Saved";
                }
            }
            else
            {
                message = "Model State failed";
            }

            ViewBag.Message = message;
            //    //  productViewModel.Products = _productManager.GetAll();
            //    _purchaseViewModel.SupplierSelectListItems = _supplierManager
            //                           .GetAll()
            //                           .Select(c => new SelectListItem()
            //                           {
            //                               Value = c.Id.ToString(),
            //                               Text = c.Name
            //                           }).ToList();

            //    var ProductSelectListItems = _productManager
            //                         .GetAll()
            //                         .Select(c => new SelectListItem()
            //                         {
            //                             Value = c.Id.ToString(),
            //                             Text = c.Name
            //                         }).ToList();
            //    var CategorySelectListItems = _categoryManager
            //                           .GetAll()
            //                           .Select(c => new SelectListItem()
            //                           {
            //                               Value = c.Id.ToString(),
            //                               Text = c.Name
            //                           }).ToList();

            _saleViewModel.CustomerSelectListItems = _customerManager
                                     .GetAll()
                                     .Select(c => new SelectListItem()
                                     {
                                         Value = c.Id.ToString(),
                                         Text = c.Name
                                     }).ToList();

            ViewBag.Customer = _saleViewModel.CustomerSelectListItems;

            _saleViewModel.CategorySelectListItems = _categoryManager
                                   .GetAll()
                                   .Select(c => new SelectListItem()
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name
                                   }).ToList();

            ViewBag.Category = _saleViewModel.CategorySelectListItems;
           // _saleManager.Add(sale);

            return View();
        }

        public void setLoyalty(int Id,double grandTotal)
        {
            Customer customer = _customerManager.GetById(Id);
            customer.LoyaltyPoint = customer.LoyaltyPoint- Convert.ToInt32(customer.LoyaltyPoint/10) + Convert.ToInt32(grandTotal/1000);
            _customerManager.Update(customer);


        }


        public JsonResult GetLoyaltyPoint(int CustId)
        {
            
            var CustomerList = _customerManager.GetAll().Where(c => c.Id == CustId).ToList();
            var customers = from c in CustomerList select (c.LoyaltyPoint);


            return Json(customers, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetProductByCategoryId(int categoryId)
        {

            var productList = _productManager.GetAll().Where(c => c.CategoryId == categoryId).ToList();
            var products = from p in productList select (new { p.Id, p.Name });
            return Json(products, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetProductName(int ProdId)
        {

            var productList = _productManager.GetAll().Where(c => c.Id == ProdId).ToList();
            var products = from p in productList select ( p.Name );
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAvailable(int ProdId)
        {

            var SproductList = _saleManager.GetPrevious().Where(c => c.ProductId == ProdId).ToList();
            var sumS = (from p in SproductList select p.Quantity).Sum();
            var PproductList = _purchaseManager.GetPrevious().Where(c => c.ProductId == ProdId).ToList();
            var sumP = (from p in PproductList select p.Quantity).Sum();
            // var sumP=0;
            // var sumS=0;
            //foreach (var s in SproductList)
            //{
            //    sumS += Convert.ToInt32(s.Quantity);
            //}
            //foreach (var p in PproductList)
            //{
            //    sumP += Convert.ToInt32(p.Quantity);
            //}
            var r = Math.Abs(sumS - sumP);

            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReorderLevel(int proId)
        {
            var productList = _productManager.GetAll().Where(c=>c.Id==proId).ToList();
            var sum = (from p in productList select p.ReorderLevel);
            return Json(sum, JsonRequestBehavior.AllowGet);

        }
       


        public ActionResult GetSale()
        {
            // PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            var Sales = _saleManager.GetAll().OrderBy(t => t.Date).ToList();
            var Customers = _customerManager.GetAll().ToList();
            ViewBag.saleDetail = (from Sale in Sales
                                      join Customer in Customers on Sale.CustomerId equals Customer.Id orderby Sale.Id
                                      select new SaleX { Id = Sale.Id,Code=Sale.Code, Date = Sale.Date, Customer= Customer.Name}).ToList();

            return View();
        }
        public class SaleX
        {

            public int Id { get; set; }
            public string Code { get; set; }
            public string Customer { get; set; }

            public DateTime Date { get; set; }


        }

        public ActionResult GetSalDetails(int Id)
        {
            SaleViewModel saleViewModel = new SaleViewModel();
            var p = _saleManager.GetById(Id);
            saleViewModel.Code = p.Code;
            saleViewModel.Date = p.Date;
            var s = _customerManager.GetById(p.CustomerId);
            ViewBag.Customer = s.Name;
            saleViewModel.SaleProducts = _saleManager.GetPreviousbyId(Id).OrderBy(t => t.Id).ToList();

            return View(saleViewModel);
        }

        public JsonResult GetSaleCode()
        {

            var productList = _saleManager.GetAll().ToList();
            string saleCode;
            try
            {
                var products = (from s in productList
                                orderby s.Id descending
                                select s.Code).First();

                saleCode = products;
                if (saleCode == null)
                {
                    saleCode = "2019-0001";

                }
                else
                {
                    string sub = saleCode.Substring(5, 4);
                    int c = Convert.ToInt32(sub);
                    c++;
                    string s = c.ToString("0000");
                    saleCode = "2019-" + s;


                }

            }
            catch
            {
                saleCode = "2019-0001";
            }

            


            return Json(saleCode, JsonRequestBehavior.AllowGet);
        }

    }
}