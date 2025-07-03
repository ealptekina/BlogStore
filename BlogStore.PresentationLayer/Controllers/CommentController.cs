using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Dtos;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogStore.PresentationLayer.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;  // <-- buraya ekledik
        private readonly IToxicityService _toxicityService;

        public CommentController(ICommentService commentService, UserManager<AppUser> userManager, IToxicityService toxicityService)
        {
            _commentService = commentService;
            _userManager = userManager; // constructor'da ata
            _toxicityService = toxicityService;
        }

        public IActionResult CommentList()
        {
            var values = _commentService.GetCommentsWithArticleTitles();
            return View(values);
        }


        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.IsValid = false;
            _commentService.TInsert(comment);
            return RedirectToAction("CommentList");
        }

        [HttpGet]
        public IActionResult UpdateComment(int id)
        {
            var value = _commentService.TGetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateComment(Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.IsValid = false;
            _commentService.TUpdate(comment);
            return RedirectToAction("CommentList");
        }

        public IActionResult DeleteComment(int id)
        {
            _commentService.TDelete(id);
            return RedirectToAction("CommentList");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(Comment comment)
        {
            if (comment.ArticleId == 0)
            {
                return Json(new { success = false, message = "Makale bilgisi eksik." });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Json(new { success = false, message = "Giriş yapmalısınız." });
            }

            comment.AppUserId = user.Id.ToString();
            comment.UserNameSurname = user.UserName;
            comment.CommentDate = DateTime.Now;
            comment.IsValid = false;

            try
            {
                _commentService.TInsert(comment);
                // redirectUrl KALDIRILDI, sadece success ve message dönüyoruz
                return Json(new { success = true, message = "Yorumunuz başarıyla eklendi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Yorum eklenirken hata oluştu: " + ex.Message });
            }
        }






        [HttpGet]
        [Authorize]
        public IActionResult ReplyToComment(int id) // id: cevap verilecek yorumun Id'si
        {
            var originalComment = _commentService.TGetById(id);
            if (originalComment == null)
            {
                return NotFound();
            }

            // Yeni yorum (cevap) oluşturmak için View'e original comment bilgisi gönderebilirsin
            ViewBag.OriginalComment = originalComment;
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReplyToComment(int id, Comment replyComment)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("UserLogin", "Login");

            var originalComment = _commentService.TGetById(id);
            if (originalComment == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(replyComment.CommentDetail))
            {
                ModelState.AddModelError("CommentDetail", "Cevap zorunludur.");
                ViewBag.OriginalComment = originalComment;
                return View(replyComment);
            }

            replyComment.AppUserId = user.Id.ToString();
            replyComment.UserNameSurname = user.UserName;
            replyComment.CommentDate = DateTime.Now;
            replyComment.IsValid = false;

            replyComment.ParentCommentId = id;

            try
            {
                _commentService.TInsert(replyComment);
                return RedirectToAction("CommentList");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Yorum eklenirken hata oluştu: " + ex.Message);
                ViewBag.OriginalComment = originalComment;
                return View(replyComment);
            }
        }





        // GET: Comment/TestToxicity
        public IActionResult TestToxicity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TestToxicity(string commentText)
        {
            if (string.IsNullOrWhiteSpace(commentText))
            {
                ViewBag.Message = "Lütfen yorum giriniz.";
                return View();
            }

            var (isToxic, score) = await _toxicityService.AnalyzeCommentAsync(commentText);

            ViewBag.CommentText = commentText;
            ViewBag.IsToxic = isToxic;
            ViewBag.Score = score;

            return View();
        }

    }
}
