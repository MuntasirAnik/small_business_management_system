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
    public class ProductController : Controller
    {
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();



        // GET: Product
        [HttpGet]
        public ActionResult Add()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            Product product = new Product();
            productViewModel.Products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager
                                   .GetAll()
                                   .Select(c => new SelectListItem()
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name
                                   }).ToList();

            return View(productViewModel);
        }



        [HttpPost]
        public ActionResult Add(ProductViewModel productViewModel)
        {
            string message = "";
            Product product = Mapper.Map<Product>(productViewModel);


            if (ModelState.IsValid)
            {

                if (_productManager.Add(product))
                {
                    message = "Saved";
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
            productViewModel.Products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager
                                .GetAll()
                                .Select(c => new SelectListItem()
                                {
                                    Value = c.Id.ToString(),
                                    Text = c.Name
                                }).ToList();
            return RedirectToAction("Add");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _productManager.GetById(id);

            ProductViewModel productViewModel = Mapper.Map<ProductViewModel>(product);

            productViewModel.Products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager
                                   .GetAll()
                                   .Select(c => new SelectListItem()
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name
                                   }).ToList();



            return View(productViewModel);
        }




        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            string message = "";


            if (ModelState.IsValid)
            {
                Product product = Mapper.Map<Product>(productViewModel);

                if (_productManager.Update(product))
                {
                    message = "Updated";
                }
                else
                {
                    message = "Not Updated";
                }
            }
            else
            {
                message = "ModelState Failed";
            }

            ViewBag.Message = message;
            productViewModel.Products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager
                                   .GetAll()
                                   .Select(c => new SelectListItem()
                                   {
                                       Value = c.Id.ToString(),
                                       Text = c.Name
                                   }).ToList();



            return RedirectToAction("Add");
        }



        public ActionResult Delete(int id)
        {
            string message = " ";
           Product product = _productManager.GetById(id);

            if (_productManager.Delete(product.Id))
            {
                message = "Deleted";

            }

            else
            {
                message = "Not Deleted";
            }

            ViewBag.Message = message;

            return RedirectToAction("Add");
        }



        public JsonResult GetProductByCode(string code)
        {
            var List = _productManager.GetAll().Where(c => c.Code.ToLower() == code.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductByName(string name)
        {
            var List = _productManager.GetAll().Where(c => c.Name.ToLower() == name.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }


    }
}