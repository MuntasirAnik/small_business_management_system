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
    public class SaleViewModel
    {


        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<SaleProduct> SaleProducts { get; set; }
        public List<SelectListItem> CustomerSelectListItems { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
    }
}