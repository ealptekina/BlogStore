# 📰 BlogStore - ASP.NET Core 9 Katmanlı Mimari Blog Projesi

Bu proje, **ASP.NET Core MVC 9** ile geliştirilmiş, modern mimari prensiplerine uygun olarak hazırlanmış bir **blog platformudur**. Katmanlı mimari yapısı, token tabanlı güvenlik, yorum sistemi, yazar panelleri, dashboard istatistikleri gibi birçok gelişmiş özelliği içerisinde barındırmaktadır.

## 🚀 Proje Yapısı

### 📁 Katmanlar:
- **BlogStore.EntityLayer:** Tüm entity sınıfları burada tanımlandı.
- **BlogStore.DataAccessLayer:** EF Core, repository pattern, context ve unit-of-work yapıları burada.
- **BlogStore.BusinessLayer:** Servis katmanı, validation kuralları, dependency injection container yapısı.
- **BlogStore.PresentationLayer:** ASP.NET Core MVC web katmanı, ViewComponent'ler, controller'lar ve UI.
- **MSSQL Database:** EF Core ile migration ve veritabanı yönetimi yapılmıştır.

---

## 💡 Öne Çıkan Özellikler

### 🌐 Genel:
- ASP.NET Core MVC 9 ile modern katmanlı mimari
- Entity Framework Core Code First yaklaşımı
- ASP.NET Identity ile kullanıcı yönetimi

### 🔐 Kullanıcı İşlemleri:
- Kayıt Ol / Giriş Yap
- Kullanıcı Profili Güncelleme
- Şifre Değiştirme

### 📝 Bloglar:
- Blog ekleme, güncelleme, silme
- Detay sayfası (SEO uyumlu slug URL)
- Kategoriye ve yazara göre filtreleme

### 🗨️ Yorumlar:
- Ajax ile yorum yapma
- Giriş yapılmadıysa yorum paneli gösterilmez
- Toksik içerik analizi (OpenAI/HuggingFace üzerinden)

### 📊 Admin Panel:
- Yeni blog oluşturma
- Blog listesi yönetimi
- Kullanıcı profili ve istatistik gösterimi
- Dashboard: Toplam kategori, içerik sayısı vs.

---

## 🧪 Kullanılan Teknolojiler

- ASP.NET Core 9
- Entity Framework Core
- ASP.NET Core Identity
- FluentValidation
- ViewComponent
- AutoMapper / DTO
- AJAX / jQuery
- OpenAI / Hugging Face API
- MS SQL Server

---

## 📸 Ekran Görüntüleri
![Ekran görüntüsü 2025-07-03 040149](https://github.com/user-attachments/assets/882d41ae-8ec6-400f-b9e9-eb7ca816073e)
![Ekran görüntüsü 2025-07-03 040214](https://github.com/user-attachments/assets/a216bd5f-11d1-4e89-8e6e-77ca04387a05)
![Ekran görüntüsü 2025-07-03 040943](https://github.com/user-attachments/assets/f7674f95-9d29-4196-bbc3-72af035b24c7)
![Ekran görüntüsü 2025-07-03 041016](https://github.com/user-attachments/assets/b1cd4062-2c08-49b4-92d3-0d6306164fee)
![Ekran görüntüsü 2025-07-03 041035](https://github.com/user-attachments/assets/afbc0276-b5a3-4a5a-acb5-caa3d21c3f17)
![Ekran görüntüsü 2025-07-03 040913](https://github.com/user-attachments/assets/5a8e2fe0-6157-414a-9f9a-435873cdc9b6)
![Ekran görüntüsü 2025-07-03 041052](https://github.com/user-attachments/assets/7c989def-6482-4076-a358-8f8339c49298)
![Ekran görüntüsü 2025-07-03 041113](https://github.com/user-attachments/assets/e88dfa13-f4e8-4369-81ed-2488115916dc)
![Ekran görüntüsü 2025-07-03 041201](https://github.com/user-attachments/assets/4eb212f7-d255-4a94-81ae-2da7b092b926)

ADMİN PANEL
![Ekran görüntüsü 2025-07-03 041224](https://github.com/user-attachments/assets/c53c358d-af35-49e7-82e1-f810e28eb1e7)
![Ekran görüntüsü 2025-07-03 041259](https://github.com/user-attachments/assets/307ef621-da5d-48e6-89f5-d7c49ff90d4e)
![Ekran görüntüsü 2025-07-03 041336](https://github.com/user-attachments/assets/a7504033-164f-4822-ab7c-9be1c602df4b)
![Ekran görüntüsü 2025-07-03 041413](https://github.com/user-attachments/assets/9747d624-0452-4214-bd70-ecb216c3c281)
![Ekran görüntüsü 2025-07-03 041451](https://github.com/user-attachments/assets/92c62496-1421-4105-bd7b-2178ed9a59a8)
![Ekran görüntüsü 2025-07-03 041506](https://github.com/user-attachments/assets/238f455f-3cc6-4d16-a0b3-90b7384f3212)
![Ekran görüntüsü 2025-07-03 041527](https://github.com/user-attachments/assets/37d94bcd-934a-42c6-9088-171c4dd8b801)
![Ekran görüntüsü 2025-07-03 041544](https://github.com/user-attachments/assets/968b012a-a208-432c-8e0f-8a8d9e8735db)
![Ekran görüntüsü 2025-07-03 041611](https://github.com/user-attachments/assets/1f47868b-2b34-4d95-9b28-de714b51407f)
![Ekran görüntüsü 2025-07-03 041639](https://github.com/user-attachments/assets/bcaa3e98-6fee-42d5-a767-4ec34f9e6032)
![Ekran görüntüsü 2025-07-03 041655](https://github.com/user-attachments/assets/9a57072e-831e-4f57-aaaf-a8b9fd3c8bc8)
![Ekran görüntüsü 2025-07-03 041850](https://github.com/user-attachments/assets/24c4728e-5256-4974-a055-308b649faa5f)

---

