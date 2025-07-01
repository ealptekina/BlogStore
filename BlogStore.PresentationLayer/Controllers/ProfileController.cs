using BlogStore.DataAccessLayer.Context;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class ProfileController : Controller
    {
        private readonly BlogContext _context; // Veritabanı işlemleri için context
        private readonly UserManager<AppUser> _userManager; // Identity kullanıcı işlemleri için UserManager
        private readonly SignInManager<AppUser> _signInManager;

        public ProfileController(BlogContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> ProfileDetailAsync()
        {
            var user = await _userManager.GetUserAsync(User); // Giriş yapan kullanıcıyı getir

            if (user == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(string Name, string Surname, string Email)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("UserLogin", "Login");

            user.Name = Name;
            user.Surname = Surname;
            user.Email = Email;
            user.NormalizedEmail = Email.ToUpper();

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["Message"] = "Profil bilgileri başarıyla güncellendi.";
            }
            else
            {
                TempData["Message"] = "Profil güncellenirken hata oluştu.";
            }
            return RedirectToAction("ProfileDetail");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string NewPassword)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("UserLogin", "Login");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, NewPassword);

            if (result.Succeeded)
            {
                TempData["Message"] = "Şifre başarıyla güncellendi.";
                // Şifre değişince kullanıcıyı çıkış yap:
                await _signInManager.SignOutAsync();
            }
            else
            {
                TempData["Message"] = "Şifre güncellenirken bir hata oluştu.";
            }

            return RedirectToAction("ProfileDetail");
        }



    }
}
