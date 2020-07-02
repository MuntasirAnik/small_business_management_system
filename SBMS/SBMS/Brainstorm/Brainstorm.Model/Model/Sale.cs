using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brainstorm.Model.Model
{
   public class Sale
    {

        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<SaleProduct> SaleProducts { get; set; }

    }
}
