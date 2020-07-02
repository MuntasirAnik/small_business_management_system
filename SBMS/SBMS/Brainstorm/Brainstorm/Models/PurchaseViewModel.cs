using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brainstorm.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
namespace Brainstorm.Models
{
    public class PurchaseViewModel
    {


        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public string Code { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string BillNo { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public List<PurchaseProduct> PurchaseProducts { get; set; }
        public List<SelectListItem> SupplierSelectListItems { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
        public List<Purchase> Purchases { get; set; }
        public List<Supplier> Suppliers { get; set; }
       






    }
}