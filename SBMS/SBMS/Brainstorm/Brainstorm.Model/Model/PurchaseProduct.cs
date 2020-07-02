using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Brainstorm.Model.Model
{
   public  class PurchaseProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime MfgDate { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime ExpDate { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public double MRPPrice { get; set; }
        public string Remarks { get; set; }
        public virtual Purchase Purchase { get; set; }



    }
}
