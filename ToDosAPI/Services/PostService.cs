
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using ToDosAPI.Data;
using ToDosAPI.Models;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Services
{
    public class PostService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PostRepository _postRepository;


        public PostService( IHttpClientFactory httpClientFactory, PostRepository postRepository) {

            _httpClientFactory = httpClientFactory;
            _postRepository = postRepository;
        }
        public async Task<List<Post>> getPostsAsync()
        {

            var httpRequestMessage = new HttpRequestMessage(
           HttpMethod.Get,
           "https://jsonplaceholder.typicode.com/posts"){};

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                if (contentStream == null) return [];

                var result = await JsonSerializer.DeserializeAsync<List<Post>>(contentStream);

                if (result != null && result.Count >0)
                {
                   var count =  _postRepository.addPostsAsync(result);
                }

                return result ?? [];
            }
            return [];

        }
        //using HttpResponseMessage response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");

        //var jsonResponse = await response.Content.ReadAsStringAsync();
        //Console.WriteLine($"{jsonResponse}");
        //var result = JsonSerializer.Deserialize<List<Post>>(jsonResponse);
        //return result ?? [];

    
    }
}
