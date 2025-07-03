using BlogStore.BusinessLayer.Abstract;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlogStore.BusinessLayer.Concrete
{
    public class ToxicityManager : IToxicityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _modelRoute;

        public ToxicityManager(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["HuggingFace:ApiKey"];
            _modelRoute = configuration["HuggingFace:ModelRoute"];
        }

        public async Task<(bool IsToxic, float Score)> AnalyzeCommentAsync(string commentText)
        {
            try
            {
                var requestBody = new { inputs = commentText };
                var requestJson = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var response = await _httpClient.PostAsync($"https://api-inference.huggingface.co/models/{_modelRoute}", content);

                if (!response.IsSuccessStatusCode)
                    return (false, 0); // API hatası varsa toksik değil say

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine("HuggingFace API Response: " + responseString);

                var parsed = JsonConvert.DeserializeObject<List<List<LabelScore>>>(responseString);

                var toxic = parsed?[0]?.FirstOrDefault(x => x.Label.ToLower().Contains("toxic"));
                var score = toxic?.Score ?? 0;

                return (score > 0.05f, score);
            }
            catch (TaskCanceledException ex)
            {
                // Bu exception genellikle zaman aşımından kaynaklanır
                Console.WriteLine("Toxicity API zaman aşımı: " + ex.Message);
                return (false, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Toxicity API genel hata: " + ex.Message);
                return (false, 0);
            }
        }



        private class LabelScore
        {
            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("score")]
            public float Score { get; set; }
        }
    }
}
