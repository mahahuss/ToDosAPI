using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Controllers;
using ToDosAPI.Extensions;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers
{
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
            var result = await _postService.getPostsAsync();
            if (result.Count ==0) BadRequest("Unable to fetch posts");
            return Ok(result);
        }

    }
}

