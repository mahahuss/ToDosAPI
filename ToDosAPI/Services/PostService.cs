using ToDosAPI.Data;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Services;

public class PostService
{
    private readonly JsonPlaceholderService _jsonPlaceholderService;
    private readonly PostRepository _postRepo;

    public PostService(JsonPlaceholderService jsonPlaceholderService, PostRepository postRepo)
    {
        _jsonPlaceholderService = jsonPlaceholderService;
        _postRepo = postRepo;
    }

    public async Task<List<Post>> GetAndCreatePostsAsync()
    {
        var posts = await _jsonPlaceholderService.GetPostsAsync();

        Console.WriteLine(posts.Count);
        if (posts.Count == 0) return [];

        var added = await _postRepo.AddPostsAsync(posts);

        Console.WriteLine(added);
        if (added == posts.Count) return posts;

        return [];
    }

    public Task<Post?> CreatePostAsync(Post post)
    {
        return _jsonPlaceholderService.CreatePostAsync(post);
    }
}