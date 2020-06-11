using System.Threading.Tasks;
using System.Collections.Generic;
using MyStudyHelper.Classes.API.Models;

namespace MyStudyHelper.Classes.API.Proxys.Interfaces
{
    public interface ICommentsProxy
    {
        Task<List<Comments>> GetCommentsByPost(string postId);
        Task<List<Comments>> GetCommentsByUser(string userId);
        Task<Comments> PostComment(Comments comment);
    }
}