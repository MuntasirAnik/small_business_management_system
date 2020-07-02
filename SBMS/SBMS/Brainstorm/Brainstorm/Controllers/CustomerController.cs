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
    public class CustomerController : Controller
    {
        CustomerManager _customerManager =new CustomerManager();
        // GET: Customer


        [HttpGet]
        public ActionResult Add()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
          
            customerViewModel.Customers = _customerManager.GetAll();


            return View(customerViewModel);
        }



        [HttpPost]
        public ActionResult Add(CustomerViewModel customerViewModel)
        {
            string message = "";
            Customer customer = Mapper.Map<Customer>(customerViewModel);


            if (ModelState.IsValid)
            {

                if (_customerManager.Add(customer))
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
            customerViewModel.Customers = _customerManager.GetAll();


            return RedirectToAction("Add");
        }




        [HttpGet]
        public ActionResult Search()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();

            customerViewModel.Customers = _customerManager.GetAll();

            return View(customerViewModel);

        }

        [HttpPost]
        public ActionResult Search(CustomerViewModel customerViewModel)
        {
            var customers = _customerManager.GetAll();

            if (customerViewModel.Code != null)
            {
                customers = customers.Where(c => c.Code.Contains(customerViewModel.Code)).ToList();
            }
            if (customerViewModel.Name != null)
            {
                customers = customers.Where(c => c.Name.ToLower().Contains(customerViewModel.Name.ToLower())).ToList();
            }

            customerViewModel.Customers = customers;


            return View(customerViewModel);
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = _customerManager.GetById(id);

            CustomerViewModel customerViewModel = Mapper.Map<CustomerViewModel>(customer);

            customerViewModel.Customers = _customerManager.GetAll();



            return View(customerViewModel);
        }




        [HttpPost]
        public ActionResult Edit(CustomerViewModel customerViewModel)
        {
            string message = "";


            if (ModelState.IsValid)
            {
              
                Customer customer = Mapper.Map<Customer>(customerViewModel);

                if (_customerManager.Update(customer))
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
            customerViewModel.Customers = _customerManager.GetAll();



            return RedirectToAction("Add");
        }

        public ActionResult Delete(int id)
        {
            string message = " ";
            Customer customer = _customerManager.GetById(id);

            if (_customerManager.Delete(customer.Id))
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



        public JsonResult GetCustomerByCode(string code)
        {
            var List = _customerManager.GetAll().Where(c => c.Code.ToLower() == code.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomerByName(string name)
        {
            var List = _customerManager.GetAll().Where(c => c.Name.ToLower() == name.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerByEmail(string email)
        {
            var List = _customerManager.GetAll().Where(c => c.Email.ToLower() == email.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomerByContact(string contact)
        {
            var List = _customerManager.GetAll().Where(c => c.Contact.ToLower() == contact.ToLower()).ToList();
            bool isExist = false;
            if (List.Count() > 0)
            {
                isExist = true;
            }
            return Json(isExist, JsonRequestBehavior.AllowGet);
        }





    }
}