using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface ICommentHistoryBackend
    {
        ObservableCollection<IComments> CommentsMod { get; set; }

        void GetCommentInfo();
        Task<Posts> GetPost(string postId);
    }
}