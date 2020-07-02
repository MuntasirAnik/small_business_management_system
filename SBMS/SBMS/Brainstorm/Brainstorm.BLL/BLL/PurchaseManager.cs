using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brainstorm.Model.Model;
using Brainstorm.Repository.Repository;


namespace Brainstorm.BLL.BLL
{
  public  class PurchaseManager
    {
        PurchaseRepository _purchaseRepository = new PurchaseRepository();
        public bool Add(Purchase purchase)
        {
            return _purchaseRepository.Add(purchase);
        }
        public bool Delete(int id)
        {
            return _purchaseRepository.Delete(id);
        }
        public bool Update(Purchase purchase)
        {
            return _purchaseRepository.Update(purchase);
        }

        public List<Purchase> GetAll()
        {

            return _purchaseRepository.GetAll();
        }
        public Purchase GetById(int id)
        {

            return _purchaseRepository.GetById(id);
        }

        public List<PurchaseProduct> GetPrevious()
        {
            return _purchaseRepository.GetPrevious();

        }
        public List<PurchaseProduct> GetPreviousbyId(int Id)
        {
            return _purchaseRepository.GetPreviousbyId(Id);
        }

        }
}
