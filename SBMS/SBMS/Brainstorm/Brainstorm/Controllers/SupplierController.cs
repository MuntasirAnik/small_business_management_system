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
    public class SupplierController : Controller
    {
        SupplierManager _supplierManager = new SupplierManager();
        // GET: Supplier


        [HttpGet]
        public ActionResult Add()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel();

            supplierViewModel.Suppliers = _supplierManager.GetAll();


            return View(supplierViewModel);
        }



        [HttpPost]
        public ActionResult Add(SupplierViewModel supplierViewModel)
        {
            string message = "";
            Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);


           

            if (ModelState.IsValid)
            {

                    if (_supplierManager.Add(supplier))
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
            supplierViewModel.Suppliers = _supplierManager.GetAll();


            return RedirectToAction("Add");
        }






        [HttpGet]
        public ActionResult Search()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel();

            supplierViewModel.Suppliers = _supplierManager.GetAll();


            return View(supplierViewModel);

        }

        [HttpPost]
        public ActionResult Search(SupplierViewModel supplierViewModel)
        {
            var suppliers = _supplierManager.GetAll();

            if (supplierViewModel.Code != null)
            {
               suppliers = suppliers.Where(c => c.Code.Contains(supplierViewModel.Code)).ToList();
            }
            if (supplierViewModel.Name != null)
            {
                suppliers= suppliers.Where(c => c.Name.ToLower().Contains(supplierViewModel.Name.ToLower())).ToList();
            }

            supplierViewModel.Suppliers = suppliers;


            return View(supplierViewModel);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var supplier = _supplierManager.GetById(id);

           
            SupplierViewModel supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);
            supplierViewModel.Suppliers = _supplierManager.GetAll();



            return View(supplierViewModel);
        }




        [HttpPost]
        public ActionResult Edit(SupplierViewModel supplierViewModel)
        {
            string message = "";


            if (ModelState.IsValid)
            {
                Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);


                if (_supplierManager.Update(supplier))
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
            supplierViewModel.Suppliers = _supplierManager.GetAll();



            return RedirectToAction("Add");
        }

        public ActionResult Delete(int id)
        {
            string message = " ";
           Supplier supplier = _supplierManager.GetById(id);

            if (_supplierManager.Delete(supplier.Id))
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





        public JsonResult GetSupplierByCode(string code)
        {
            var List = _supplierManager.GetAll().Where(c => c.Code.ToLower() == code.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierByName(string name)
        {
            var List = _supplierManager.GetAll().Where(c => c.Name.ToLower() == name.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSupplierByEmail(string email)
        {
            var List = _supplierManager.GetAll().Where(c => c.Email.ToLower() == email.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierByContact(string contact)
        {
            var List = _supplierManager.GetAll().Where(c => c.Contact.ToLower() == contact.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }








    }
}