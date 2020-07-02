using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brainstorm.Model.Model
{
   public class SaleProduct
    {

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double MRP { get; set; }
        public double TotalMRP { get; set; }
        public virtual Sale Sale { get; set; }

    }
}
