using BlogStore.EntityLayer.Entities;

namespace BlogStore.PresentationLayer.Models
{
    public class DashboardViewModel
    {
        public int CategoryCount { get; set; }
        public int ArticleCount { get; set; }
        public int CommentCount { get; set; }

        public List<AuthorArticleWithCategoriesViewModel> AuthorArticleWithCategories { get; set; }
        public List<Article> Last5Articles { get; set; } = new List<Article>();
        public Article MostCommentedArticle { get; set; }
        public List<CategoryStatViewModel> CategoryStats { get; set; } = new List<CategoryStatViewModel>();
    }


    public class AuthorArticleCountViewModel
    {
        public string UserName { get; set; }
        public int ArticleCount { get; set; }
    }

    public class CategoryStatViewModel
    {
        public string CategoryName { get; set; }
        public int Count { get; set; }
    }
}
