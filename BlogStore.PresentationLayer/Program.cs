using BlogStore.BusinessLayer.Container;

var builder = WebApplication.CreateBuilder(args);

// BusinessLayer servislerini buradan ekle
builder.Services.AddBusinessServices();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline konfigürasyonlarý
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "articleDetailSlug",
    pattern: "Article/ArticleDetail/{slug}",
    defaults: new { controller = "Article", action = "ArticleDetail" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{slug?}");

app.Run();
