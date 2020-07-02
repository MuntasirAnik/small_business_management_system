using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brainstorm.Model.Model;
using Brainstorm.Repository.Repository;

namespace Brainstorm.BLL.BLL
{
   

   public class SupplierManager
    {

        SupplierRepository _supplierRepository = new SupplierRepository();

        public bool Add(Supplier supplier)
        {
            return _supplierRepository.Add(supplier);
        }

        public List<Supplier> GetAll()
        {
            return _supplierRepository.GetAll();
        }


        public bool Update(Supplier supplier)
        {
            return _supplierRepository.Update(supplier);
        }

        public Supplier GetById(int id)
        {

            return _supplierRepository.GetById(id);
        }

        public bool Delete(int id)
        {

            return _supplierRepository.Delete(id);
        }

        }




}
