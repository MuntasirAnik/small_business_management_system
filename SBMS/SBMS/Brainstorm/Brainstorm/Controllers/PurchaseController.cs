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
    public class PurchaseController : Controller
    {
        PurchaseManager _purchaseManager = new PurchaseManager();
        SupplierManager _supplierManager = new SupplierManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();
        private PurchaseViewModel _purchaseViewModel = new PurchaseViewModel();
        
        // GET: Product
        [HttpGet]
        public ActionResult Add()
        {


            _purchaseViewModel.SupplierSelectListItems = _supplierManager
                                    .GetAll()
                                    .Select(c => new SelectListItem()
                                    {
                                        Value = c.Id.ToString(),
                                        Text = c.Name
                                    }).ToList();
            ViewBag.Supplier = _purchaseViewModel.SupplierSelectListItems;

            _purchaseViewModel.CategorySelectListItems = _categoryManager
                                   .GetAll()
                                   .Select(c => new SelectListItem()
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name
                                   }).ToList();

            ViewBag.Category = _purchaseViewModel.CategorySelectListItems;
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
        public ActionResult Add(Purchase purchase)
        {
            //    string message = "";



            //    if (ModelState.IsValid)
            //    {

            //        if (_purchaseManager.Add(purchase))
            //        {
            //            message = "Saved";
            //        }
            //        else
            //        {
            //            message = "Not Saved";
            //        }
            //    }
            //    else
            //    {
            //        message = "Model State failed";
            //    }

            //    ViewBag.Message = message;
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

            _purchaseViewModel.SupplierSelectListItems = _supplierManager
                                  .GetAll()
                                  .Select(c => new SelectListItem()
                                  {
                                      Value = c.Id.ToString(),
                                      Text = c.Name
                                  }).ToList();
            ViewBag.Supplier = _purchaseViewModel.SupplierSelectListItems;

            _purchaseViewModel.CategorySelectListItems = _categoryManager
                                   .GetAll()
                                   .Select(c => new SelectListItem()
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name
                                   }).ToList();

            ViewBag.Category = _purchaseViewModel.CategorySelectListItems;
            _purchaseManager.Add(purchase);

            return View();
        }


        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var product = _productManager.GetById(id);

        //    ProductViewModel productViewModel = Mapper.Map<ProductViewModel>(product);

        //    productViewModel.Products = _productManager.GetAll();
        //    productViewModel.CategorySelectListItems = _categoryManager
        //                           .GetAll()
        //                           .Select(c => new SelectListItem()
        //                           {
        //                               Value = c.Id.ToString(),
        //                               Text = c.Name
        //                           }).ToList();



        //    return View(productViewModel);
        //}




        //[HttpPost]
        //public ActionResult Edit(ProductViewModel productViewModel)
        //{
        //    string message = "";


        //    if (ModelState.IsValid)
        //    {
        //        Product product = Mapper.Map<Product>(productViewModel);

        //        if (_productManager.Update(product))
        //        {
        //            message = "Updated";
        //        }
        //        else
        //        {
        //            message = "Not Updated";
        //        }
        //    }
        //    else
        //    {
        //        message = "ModelState Failed";
        //    }

        //    ViewBag.Message = message;
        //    productViewModel.Products = _productManager.GetAll();
        //    productViewModel.CategorySelectListItems = _categoryManager
        //                           .GetAll()
        //                           .Select(c => new SelectListItem()
        //                           {
        //                               Value = c.Id.ToString(),
        //                               Text = c.Name
        //                           }).ToList();



        //    return View(productViewModel);
        //}



        //public ActionResult Delete(int id)
        //{
        //    string message = " ";
        //    Product product = _productManager.GetById(id);

        //    if (_productManager.Delete(product.Id))
        //    {
        //        message = "Deleted";

        //    }

        //    else
        //    {
        //        message = "Not Deleted";
        //    }

        //    ViewBag.Message = message;

        //    return RedirectToAction("Add");
        //}

        public JsonResult GetProductByCategoryId(int categoryId)
        {

            var productList = _productManager.GetAll().Where(c => c.CategoryId == categoryId).ToList();
            var products = from p in productList select (new { p.Id, p.Name });
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductCode(int ProdId)
        {

            var productList = _productManager.GetAll().Where(c => c.Id == ProdId).ToList();
            var products = from p in productList select (new { p.Code,p.Name});

           
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPrevMRP(int ProdId)
        {

            var productList = _purchaseManager.GetPrevious().Where(c => c.ProductId == ProdId).OrderBy(t => t.Id).ToList();
            
            var products = from p in productList select (new { p.UnitPrice, p.MRPPrice });

            
            return Json(products, JsonRequestBehavior.AllowGet);
        }
     

        public ActionResult GetPurchaseDetails()
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            purchaseViewModel.Purchases = _purchaseManager.GetAll().OrderBy(t => t.Date).ToList();
            return PartialView("Purchase/_PurchaseDetails", purchaseViewModel);
        }

        //public ActionResult GetPurchase()
        //{
        //    PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
        //    purchaseViewModel.Purchases = _purchaseManager.GetAll().OrderBy(t => t.Date).ToList();
           
        //    return View(purchaseViewModel);
        //}



        public ActionResult GetPurchase()
        {
           // PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            var Purchases = _purchaseManager.GetAll().OrderBy(t => t.Date).ToList();
            var Suppliers = _supplierManager.GetAll().ToList();
            ViewBag.purchaseDetail = (from Purchase in Purchases
                                      join Supplier in Suppliers on 
                                      Purchase.SupplierId equals Supplier.Id orderby Purchase.Id
                                      select new PurchaseX
                                      { Id = Purchase.Id,Code=Purchase.Code,
                                          BillNo = Purchase.BillNo, Supplier = Supplier.Name, Date = Purchase.Date })
                                          .ToList();
       
            return View();
        }
        public class PurchaseX
        {

            public int Id { get; set; }
            public string Code { get; set; }
            public string BillNo { get; set; }
            public string Supplier { get; set; }
           
            public DateTime Date { get; set; }


        }


        public ActionResult GetPurDetails(int Id)
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            
            var p = _purchaseManager.GetById(Id);
            purchaseViewModel.BillNo = p.BillNo;
            purchaseViewModel.Date = p.Date;
            purchaseViewModel.Code = p.Code;
            var s = _supplierManager.GetById(p.SupplierId);
            ViewBag.SupplierName = s.Name;
            purchaseViewModel.PurchaseProducts = _purchaseManager.GetPreviousbyId(Id).
                OrderBy(t => t.Id).ToList();

            return View(purchaseViewModel);
        }

        public JsonResult GetPurCode()
        {
            var productList = _purchaseManager.GetAll().ToList();
            string purCode;
            try
            {
                var products = (from s in productList
                                orderby s.Id descending
                                select s.Code).First();
                purCode = products;
                if (string.IsNullOrEmpty(purCode))
                {
                    purCode = "2019-0001";

                }
                else
                {
                    string sub = purCode.Substring(5, 4);
                    int c = Convert.ToInt32(sub);
                    c++;
                    string s = c.ToString("0000");
                    purCode = "2019-" + s;
                }
            }
            catch
            {
                purCode = "2019-0001";
            }
      
            return Json(purCode, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchaseByBill(string bill)
        {
            var List = _purchaseManager.GetAll().Where(c => c.BillNo.ToLower() == bill.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }


    }
}