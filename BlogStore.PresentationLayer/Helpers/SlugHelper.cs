namespace BlogStore.PresentationLayer.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string title)
        {
            string slug = title.ToLowerInvariant()
                .Replace("ç", "c")
                .Replace("ğ", "g")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ş", "s")
                .Replace("ü", "u");

            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-").Trim('-');

            return slug;
        }
    }
}
