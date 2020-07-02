using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brainstorm.Model.Model;
using Brainstorm.DatabaseContext.DatabaseContext;

namespace Brainstorm.Repository.Repository
{
   public class ProductRepository
    {


        ProjectDbContext _dbContext = new ProjectDbContext();

        public bool Add(Product product)
        {
            _dbContext.Products.Add(product);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            Product aProduct = _dbContext.Products.FirstOrDefault((c => c.Id == id));
            _dbContext.Products.Remove(aProduct);
            return _dbContext.SaveChanges() > 0;
        }
        public bool Update(Product product)
        {
            Product aProduct = _dbContext.Products.FirstOrDefault(c => c.Id == product.Id);
            if (aProduct != null)
            {
                aProduct.Code = product.Code;
                aProduct.Name = product.Name;
                aProduct.CategoryId = product.CategoryId;
                aProduct.ReorderLevel = product.ReorderLevel;
                aProduct.Description = product.Description;
            }

            return _dbContext.SaveChanges() > 0;
        }

        public List<Product> GetAll()
        {

            return _dbContext.Products.ToList();
        }
        public Product GetById(int id)
        {

            return _dbContext.Products.FirstOrDefault((c => c.Id == id));
        }

    }
}
