using System.Net;
using static System.GC;
namespace TaskMVC.Service
{

    public class BaseService : IDisposable
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _baseUrl;

        public BaseService(HttpClient client, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor;
            _baseUrl = config.GetSection("ApiUrls:BaseUrl").Value;
        }

        public void Dispose() => SuppressFinalize(true);


        public async Task<TResult?> MakeRequest<TResult, TData>(string address, string methodType, TData data,
            string token = "")
        {
            if (string.IsNullOrEmpty(address)) throw new ArgumentNullException("address");
            if (string.IsNullOrEmpty(methodType)) throw new ArgumentNullException("method type");

            if (!string.IsNullOrEmpty(token))
                _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var apiResult = methodType.ToUpper() switch
            {
                "POST" => await _client.PostAsJsonAsync($"{_baseUrl}{address}", data),
                "PUT" => await _client.PutAsJsonAsync($"{_baseUrl}{address}", data),
                "DELETE" => await _client.DeleteAsync($"{_baseUrl}{address}"),
                _ => await _client.GetAsync($"{_baseUrl}{address}")
            };

            if (apiResult.StatusCode == HttpStatusCode.BadRequest)
            {
                var res = await apiResult.Content.ReadAsStringAsync();
                return default;
            }

            if (!apiResult.IsSuccessStatusCode)
                return default;

            var result = await apiResult.Content.ReadFromJsonAsync<TResult>();

            return result ?? default;
        }

     
    }
}
