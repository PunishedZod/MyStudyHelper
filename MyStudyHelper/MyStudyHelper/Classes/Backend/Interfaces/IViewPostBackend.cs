using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface IViewPostBackend
    {
        ObservableCollection<IComments> CommentsList { get; set; }

        Task<IPosts> DownVote(Posts post);
        void GetCommentsInfo(string id);
        Task<IComments> PostComment(string comment, string postId);
        Task<IPosts> UpVote(Posts post);
    }
}