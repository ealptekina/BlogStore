using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using BlogStore.PresentationLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BlogStore.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;  
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;

        public ArticleController(IArticleService articleService, ICommentService commentService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _commentService = commentService;
            _categoryService = categoryService;
        }

        [Route("Article/ArticleDetail/{slug}")]
        public IActionResult ArticleDetail(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return NotFound();

            var article = _articleService.TGetArticleBySlug(slug);
            if (article == null)
                return NotFound();

            ViewBag.i = article.ArticleId;
            return View(article);
        }

        // Listeleme
        public IActionResult ArticleList(int id)
        {
            var articles = _articleService.TGetListWithCategoryAndUser();
            return View(articles);
        }

        public IActionResult GetCommentsByArticle(int articleId)
        {
            var comments = _commentService.TGetCommentsByArticle(articleId);
            return PartialView("_ArticleDetailCommentListComponentPartial", comments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryService.TGetAll(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article, IFormFile ImageFile)
        {
            // Kullanıcının ID'sini GUID string olarak al
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                article.AppUserId = userId;  // GUID string olduğu için direkt atayabilirsin
            }
            else
            {
                // Kullanıcı login değilse işlemi engelle
                return Unauthorized();
            }

            // Slug boşsa başlıktan oluştur
            if (string.IsNullOrEmpty(article.Slug))
            {
                article.Slug = GenerateSlug(article.Title);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                article.ImageUrl = "/uploads/" + fileName;
            }

            article.CreatedDate = DateTime.Now;
            _articleService.TInsert(article);
            return RedirectToAction("ArticleList");
        }

        // Basit slug oluşturma metodu
        private string GenerateSlug(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return Guid.NewGuid().ToString(); // başlık yoksa rastgele id

            // Küçük harfe çevir, boşlukları '-' ile değiştir, diğer karakterleri kaldır (basit)
            var slug = title.ToLowerInvariant()
                            .Replace(" ", "-")
                            .Replace(".", "")
                            .Replace(",", "")
                            .Replace(";", "")
                            .Replace(":", "")
                            .Replace("?", "")
                            .Replace("!", "")
                            .Replace("@", "")
                            .Replace("#", "")
                            .Replace("$", "")
                            .Replace("%", "")
                            .Replace("^", "")
                            .Replace("&", "")
                            .Replace("*", "")
                            .Replace("(", "")
                            .Replace(")", "");

            // Daha kapsamlı temizleme istersen regex kullanabilirsin.

            return slug;
        }



        // GET: Makaleyi yükle ve formu doldur
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = _articleService.TGetById(id);
            if (article == null) return NotFound();

            ViewBag.Categories = new SelectList(_categoryService.TGetAll(), "CategoryId", "CategoryName", article.CategoryId);
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Article article, IFormFile ImageFile)
        {
            var existingArticle = _articleService.TGetById(article.ArticleId);
            if (existingArticle == null) return NotFound();

            // Slug boşsa başlıktan oluştur
            if (string.IsNullOrEmpty(article.Slug))
            {
                article.Slug = GenerateSlug(article.Title);
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                existingArticle.ImageUrl = "/uploads/" + fileName;
            }

            // Güncelleme yapılacak alanlar
            existingArticle.Title = article.Title;
            existingArticle.Description = article.Description;
            existingArticle.CategoryId = article.CategoryId;
            existingArticle.Slug = article.Slug;

            _articleService.TUpdate(existingArticle);
            return RedirectToAction("ArticleList");
        }




        public IActionResult Delete(int id)
        {
            var article = _articleService.TGetById(id);
            if (article == null) return NotFound();

            _articleService.TDelete(article.ArticleId);
            return RedirectToAction("ArticleList");
        }

    }
}
