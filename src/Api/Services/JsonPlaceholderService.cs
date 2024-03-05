using Api.Models.Entities;

namespace Api.Services;

public class JsonPlaceholderService(HttpClient httpClient)
{
    public async Task<List<Post>> GetPostsAsync()
    {
        return await httpClient.GetFromJsonAsync<List<Post>>("posts") ?? [];
    }

    public async Task<Post?> CreatePostAsync(Post post)
    {
        using var response = await httpClient.PostAsJsonAsync("posts", post);
        return await response.Content.ReadFromJsonAsync<Post>();
    }
}