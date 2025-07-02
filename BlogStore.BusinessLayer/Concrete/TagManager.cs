using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
    public class TagManager : ITagService
    {
        private readonly ITagDal _tagDal;

        public TagManager(ITagDal tagDal)
        {
            _tagDal = tagDal;
        }

        public List<Tag> GetTagsByArticleId(int articleId)
        {
            return _tagDal.GetTagsByArticleId(articleId);
        }

        public void TDelete(int id)
        {
            _tagDal.Delete(id);
        }

        public List<Tag> TGetAll()
        {
            return _tagDal.GetAll();
        }

        public Tag TGetById(int id)
        {
            return _tagDal.GetById(id);
        }

        public void TInsert(Tag entity)
        {
            _tagDal.Insert(entity);
        }

        public void TUpdate(Tag entity)
        {
            _tagDal.Update(entity);
        }
    }
}
