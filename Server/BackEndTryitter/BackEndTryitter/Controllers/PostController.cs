using BackEndTryitter.Contracts.Post;
using BackEndTryitter.Models;
using BackEndTryitter.Repositories;
using BackEndTryitter.Services.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BackEndTryitter.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public IActionResult GetPostById([FromRoute] Guid id)
        {
            var post = _postRepository.GetPostById(id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet]
        [Route("user/{username}")]
        [AllowAnonymous]
        public IActionResult GetPostsByUsername([FromRoute] string username)
        {
            var posts = _postRepository.GetAllPostsByUsername(username);

            if (!posts.Any())
                return NotFound();

            return Ok(posts);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllPosts()
        {
            var posts = _postRepository.GetAllPosts();

            if (!posts.Any())
                return NotFound();

            return Ok(posts);
        }

        [HttpGet]
        [Route("last/{username}")]
        public IActionResult GetLastPostByUsername([FromRoute] string username)
        {
            var post = _postRepository.GetLastPostByUsername(username);
            
            if (post == null)
                return NotFound();

            if (!AuthorizationServices.CheckAuthorization(HttpContext, post.UserId))
                return Unauthorized();

            return Ok(post);
        }

        [HttpPost]
        public IActionResult AddPost([FromBody] PostRequest post)
        {
            if (!AuthorizationServices.CheckAuthorization(HttpContext, post.userId))
                return Unauthorized();

            if (post.text.Length > 300 || post.text.Length == 0)
                return BadRequest("Text Length is invalid");

            Post addPost = new()
            {
                PostId = Guid.NewGuid(),
                UserId = post.userId,
                Text = post.text,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        
            _postRepository.AddPost(addPost);

            return CreatedAtAction(nameof(GetPostById), new { id = addPost.PostId }, addPost);
        }

        [HttpPatch]
        [Route("{PostId}")]
        public IActionResult UpdatePost([FromRoute] Guid postId, [FromBody] PostText text)
        {
            var postToUpdate = _postRepository.GetPostById(postId);
            

            if (text.text.Length > 300 || text.text.Length == 0)
                return BadRequest("Text Length is invalid");

            if (postToUpdate == null)
                return NotFound();
            
            if (!AuthorizationServices.CheckAuthorization(HttpContext, postToUpdate.UserId))
                return Unauthorized();

            _postRepository.UpdateTextPost(postId, text.text);

            return NoContent();
        }

        [HttpDelete]
        [Route("{PostId}")]
        public IActionResult DeletePost([FromRoute] Guid postId)
        {
            var postToUpdate = _postRepository.GetPostById(postId);

            if (postToUpdate == null)
                return NotFound();

            if (!AuthorizationServices.CheckAuthorization(HttpContext, postToUpdate.UserId))
                return Unauthorized();

            _postRepository.DeletePost(postId);

            return NoContent();
        }
    }
}
