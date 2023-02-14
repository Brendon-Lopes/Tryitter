using BackEndTryitter.Models;

namespace BackEndTryitter.Repositories
{
    public interface IPostRepository
    {
        Post? GetPostById(Guid Id);
        List<Post> GetAllPostsByUsername(string username);
        List<Post> GetAllPosts();
        void AddPost(Post post);
        void UpdateTextPost(Guid id, string text);
        void DeletePost(Guid id);
    }
}
