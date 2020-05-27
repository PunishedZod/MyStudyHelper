using MyStudyHelper.Classes.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.API.Proxys.Interfaces
{
    public interface ICommentsProxy
    {
        Task<string> DeleteComment(int id);
        Task<List<Comments>> GetCommentsByPost(string postId);
        Task<Comments> PostComments(Comments comment);
    }
}