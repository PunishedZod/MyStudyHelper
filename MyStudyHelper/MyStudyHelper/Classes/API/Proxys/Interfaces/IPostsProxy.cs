using MyStudyHelper.Classes.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.API.Proxys.Interfaces
{
    public interface IPostsProxy
    {
        Task<string> DeletePost(int id);
        Task<List<Posts>> GetAllPosts();
        Task<List<Posts>> GetPopularPosts();
        Task<List<Posts>> GetRecentPosts();
        Task<string> PostPosts(Posts post);
    }
}