
using Microsoft.EntityFrameworkCore;
using MyDatabase.Models;

namespace MyDatabase.Repository.Posts
{
    public class PostsRepoImpl(DatabaseContext database) : IPostsRepo
    {
        public readonly DatabaseContext _databaseContext = database;

        public async Task<List<Post>> GetPosts()
        {
            return await _databaseContext.Post.ToListAsync();
        }
    }
}
