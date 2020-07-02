using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brainstorm.Model.Model;
using Brainstorm.DatabaseContext.DatabaseContext;

namespace Brainstorm.Repository.Repository
{
    public class SaleRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();

        public bool Add(Sale sale)
        {
            _dbContext.Sales.Add(sale);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            Sale aSale = _dbContext.Sales.FirstOrDefault(c => c.Id == id);
            _dbContext.Sales.Remove(aSale);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Update(Sale sale)
        {
           Sale aSale = _dbContext.Sales.FirstOrDefault(c => c.Id == sale.Id);
            if (aSale != null)
            {
                aSale.Date = sale.Date;
                aSale.CustomerId= sale.CustomerId;

            }

            return _dbContext.SaveChanges() > 0;
        }

        public List<Sale> GetAll()
        {

            return _dbContext.Sales.ToList();
        }
        public Sale GetById(int id)
        {

            return _dbContext.Sales.FirstOrDefault((c => c.Id == id));
        }

        public List<SaleProduct> GetPrevious()
        {

            return _dbContext.SaleProducts.ToList();
        }
        public List<SaleProduct> GetPreviousbyId(int Id)
        {

            return _dbContext.SaleProducts.Where(c => c.Sale.Id == Id).ToList();
        }
    }
}
