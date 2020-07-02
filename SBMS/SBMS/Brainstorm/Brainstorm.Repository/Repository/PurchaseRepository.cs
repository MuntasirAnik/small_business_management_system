using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brainstorm.Model.Model;
using Brainstorm.DatabaseContext.DatabaseContext;

namespace Brainstorm.Repository.Repository
{
   public class PurchaseRepository
    {

        ProjectDbContext _dbContext = new ProjectDbContext();

        public bool Add( Purchase purchase)
        {
            _dbContext.Purchases.Add(purchase);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
        Purchase aPurchase = _dbContext.Purchases.FirstOrDefault(c => c.Id == id);
            _dbContext.Purchases.Remove(aPurchase);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Update(Purchase purchase)
        {
        Purchase aPurchase = _dbContext.Purchases.FirstOrDefault(c => c.Id == purchase.Id);
            if (aPurchase != null)
            {
            aPurchase.Date = purchase.Date;
            aPurchase.BillNo = purchase.BillNo;
            aPurchase.SupplierId = purchase.SupplierId;

            }

            return _dbContext.SaveChanges() > 0;
        }

        public List<Purchase> GetAll()
        {

            return _dbContext.Purchases.ToList();
        }
        public Purchase GetById(int id)
        {

            return _dbContext.Purchases.FirstOrDefault((c => c.Id == id));
        }

        public List<PurchaseProduct> GetPrevious()
        {

            return _dbContext.PurchaseProducts.ToList();
        }

        public List<PurchaseProduct> GetPreviousbyId(int Id)
        {

            return _dbContext.PurchaseProducts.Where(c => c.Purchase.Id == Id).ToList();
        }
       

    }
}
