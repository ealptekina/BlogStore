using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Dtos;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void TDelete(int id)
        {
            _categoryDal.Delete(id);
        }

        public List<Category> TGetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category TGetById(int id)
        {
            return _categoryDal.GetById(id);
        }

        public List<CategoryWithArticleCountDto> TGetCategoryWithArticleCount()
        {
            return _categoryDal.GetCategoryWithArticleCount();
        }

        // Kategori adı boş değilse, uzunluğu 3 ile 30 karakter arasındaysa
        // ve içinde 'a' harfi geçiyorsa veritabanına eklenir.
        public void TInsert(Category entity)
        {
            if (entity.CategoryName != "" && entity.CategoryName.Length >= 3 && entity.CategoryName.Length <= 30 && entity.CategoryName.Contains('a'))
            {
                _categoryDal.Insert(entity);
            }
            else
            {
                //hata mesajı
            }
        }

        public void TUpdate(Category entity)
        {
            if (entity.CategoryName != null)
            {
                _categoryDal.Update(entity);
            }
            else
            {
                //hata mesajı
            }
        }
    }
}
