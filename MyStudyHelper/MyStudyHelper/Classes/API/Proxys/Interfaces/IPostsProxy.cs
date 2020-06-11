using System.Threading.Tasks;
using System.Collections.Generic;
using MyStudyHelper.Classes.API.Models;

namespace MyStudyHelper.Classes.API.Proxys.Interfaces
{
    public interface IPostsProxy
    {
        Task<List<Posts>> GetAllPosts();
        Task<Posts> GetPost(string postId);
        Task<List<Posts>> GetPostsByUser(string userId);
        Task<List<Posts>> GetRecentPosts();
        Task<Posts> PostPost(Posts post);
        Task<Posts> UpdatePost(Posts post);
    }
}