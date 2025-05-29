using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EFArticleDal : GenericRepository<Article>, IArticleDal
    {
        private readonly BlogContext _context;
        public EFArticleDal(BlogContext context) : base(context)
        {
        }
    }
}
