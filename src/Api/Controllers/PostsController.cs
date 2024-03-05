using Api.Models.Entities;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class PostsController : BaseController
{
    private readonly PostService _postService;

    public PostsController(PostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult> GetPosts()
    {
        var result = await _postService.GetAndCreatePostsAsync();

        if (result.Count == 0) BadRequest("Unable to fetch posts");

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreatePost()
    {
        var result = await _postService.CreatePostAsync(new Post
        {
            Title = "testing",
            Body = "nice"
        });

        if (result is null) return BadRequest();

        return Ok(result);
    }
}