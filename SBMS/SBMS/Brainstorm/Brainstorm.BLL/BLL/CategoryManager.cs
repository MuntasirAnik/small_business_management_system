using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brainstorm.Model.Model;
using Brainstorm.Repository.Repository;
namespace Brainstorm.BLL.BLL
{
  public  class CategoryManager
    {
        CategoryRepository _categoryRepository = new CategoryRepository();

        public bool Add(Category category)
        {
            return _categoryRepository.Add(category);
        }

        public List<Category> GetAll()
        {

            return _categoryRepository.GetAll();
        }

        public bool Update(Category category)

        {
            return _categoryRepository.Update(category);
        }


        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);

        }


        public bool Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }
    }
}
