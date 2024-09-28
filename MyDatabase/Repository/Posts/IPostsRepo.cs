
using MyDatabase.Models;

namespace MyDatabase.Repository.Posts
{
    public interface IPostsRepo
    {

        public Task<List<Post>> GetPosts();

    }
}
