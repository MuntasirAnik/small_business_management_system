using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brainstorm.Model.Model;
using Brainstorm.Repository.Repository;

namespace Brainstorm.BLL.BLL
{
      
    public class SaleManager
    {

        SaleRepository _saleRepository = new SaleRepository();
        public bool Add(Sale sale)
        {
            return _saleRepository.Add(sale);
        }
        public bool Delete(int id)
        {
            return _saleRepository.Delete(id);
           
        }
        public bool Update(Sale sale)
        {
            return _saleRepository.Update(sale);
           
        }

        public List<Sale> GetAll()
        {
            return _saleRepository.GetAll();
          
        }
        public Sale GetById(int id)
        {

            return _saleRepository.GetById(id);
        }

        public List<SaleProduct> GetPrevious()
        {
            return _saleRepository.GetPrevious();
        }
        public List<SaleProduct> GetPreviousbyId(int Id)
        {
            return _saleRepository.GetPreviousbyId(Id);
        }

        }
    }
