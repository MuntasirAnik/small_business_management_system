using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brainstorm.Model.Model
{
    public class Purchase
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName ="Date")]
        public DateTime Date { get; set; }
        public string BillNo { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public List<PurchaseProduct> PurchaseProducts { get; set; }

    }
}
