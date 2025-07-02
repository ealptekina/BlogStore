namespace BlogStore.PresentationLayer.Models
{
    public class AuthorArticleWithCategoriesViewModel
    {
        public string UserName { get; set; }
        public int ArticleCount { get; set; }
        public List<string> Categories { get; set; }
    }
}
