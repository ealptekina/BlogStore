using BlogStore.BusinessLayer.Abstract;
using BlogStore.BusinessLayer.Concrete;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.EntityFramework;
using BlogStore.EntityLayer.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace BlogStore.BusinessLayer.Container
{
    public static class MicrosoftDependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Service kayıtları
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EFCategoryDal>();

            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICommentDal, EFCommentDal>();

            services.AddScoped<IArticleService, ArticleManager>();
            services.AddScoped<IArticleDal, EFArticleDal>();

            services.AddScoped<ITagService, TagManager>();
            services.AddScoped<ITagDal, EFTagDal>();

            // DbContext ve Identity kayıtları
            services.AddDbContext<BlogContext>();

            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<BlogContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IToxicityService, ToxicityManager>();



            return services;
        }
    }
}
