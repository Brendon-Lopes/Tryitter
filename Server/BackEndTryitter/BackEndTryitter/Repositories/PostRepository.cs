using BackEndTryitter.Contexts;
using BackEndTryitter.Contracts.Post;
using BackEndTryitter.Exceptions;
using BackEndTryitter.Models;
using System.Net;

namespace BackEndTryitter.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly TryitterContext _context;

        public PostRepository(TryitterContext context)
        {
            _context = context;
        }

        public Post? GetPostById(Guid Id)
        {
            return _context.Posts.FirstOrDefault(p => p.PostId == Id);
        }

        public List<Post> GetAllPostsByUsername(string username)
        {
            return _context.Posts.Where(p => p.User.Username == username).ToList();
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }
        
        public Post? GetLastPostByUsername(string username)
        {
            return _context.Posts
                .Where(p => p.User.Username == username)
                .OrderBy(p => p.PostId)
                .FirstOrDefault();
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);

            _context.SaveChanges();
        }

        public void UpdateTextPost(Guid id, string text)
        {
            var post = _context.Posts.FirstOrDefault(p => p.PostId == id);
            
            if (post == null)
                throw new CustomException(HttpStatusCode.NotFound, "Post not found");

            post.Text = text;
            post.UpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public void DeletePost(Guid id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.PostId == id);

            if (post == null)
                throw new CustomException(HttpStatusCode.NotFound, "Post not found");

            _context.Posts.Remove(post);

            _context.SaveChanges();
        }
    }
}
