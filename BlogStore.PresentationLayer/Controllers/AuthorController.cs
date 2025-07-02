using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BlogStore.PresentationLayer.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;


        public AuthorController(IArticleService articleService, ICategoryService categoryService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetProfile()
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.name = userProfile.Name;
            ViewBag.surname = userProfile.Surname;
            ViewBag.id = userProfile.Id;
            return View();
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            List<SelectListItem> values = (from x in _categoryService.TGetAll()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList();
            ViewBag.v = values;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            var userProfile = await _userManager.FindByNameAsync(User.Identity.Name);

            article.AppUserId = userProfile.Id;
            article.CreatedDate = DateTime.Now;

            _articleService.TInsert(article);

            return RedirectToAction("Index", "Default");
        }

        public async Task<IActionResult> MyArticleList()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("UserLogin", "Login");

            // Kullanıcının makalelerini getir
            var articles = _articleService.TGetArticlesByAppUser(user.Id);

            return View(articles); // View'a List<Article> gönder
        }

        // Tüm yazarları listeler
        public IActionResult Index(int page = 1)
        {
            int pageSize = 6; // Her sayfada 6 yazar gösterilecek
            var allAuthors = _userManager.Users.ToList();
            var pagedAuthors = allAuthors
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(allAuthors.Count / (double)pageSize);

            return View(pagedAuthors);
        }


        // Seçilen yazarın bloglarını listeler

        public IActionResult AuthorArticles(string authorName, int page = 1, int pageSize = 3)
        {
            if (string.IsNullOrEmpty(authorName))
                return NotFound();

            var names = authorName.Split('-');
            if (names.Length < 2)
                return NotFound();

            string name = names[0];
            string surname = names[1];

            var author = _userManager.Users.FirstOrDefault(u =>
                u.Name.ToLower() == name && u.Surname.ToLower() == surname);

            if (author == null)
                return NotFound();

            var allArticles = _articleService.TGetArticlesByAppUser(author.Id);

            // Sayfalama için Skip ve Take uygulayalım:
            var pagedArticles = allArticles
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            // Toplam sayfa sayısı
            int totalArticles = allArticles.Count();
            int totalPages = (int)Math.Ceiling(totalArticles / (double)pageSize);

            // ViewBag ile toplam sayfa ve mevcut sayfayı gönder
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.AuthorNameSlug = authorName;

            return View(pagedArticles);
        }

    }
}