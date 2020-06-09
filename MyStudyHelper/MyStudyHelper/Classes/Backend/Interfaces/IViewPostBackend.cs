using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface IViewPostBackend
    {
        ObservableCollection<IComments> CommentsList { get; set; }

        void GetCommentsInfo(string id);
        Task<IComments> PostComment(string comment, string postId);
        Task<IPosts> PostDownVote(Posts post);
        Task<IPosts> PostUpVote(Posts post);
    }
}