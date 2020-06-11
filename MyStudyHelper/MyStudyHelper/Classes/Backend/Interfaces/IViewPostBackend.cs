using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface IViewPostBackend
    {
        ObservableCollection<IComments> CommentsList { get; set; }

        Task<IPosts> DownVote(Posts post);
        void GetCommentsInfo(string postId);
        Task<IComments> PostComment(string comment, string postId);
        Task<IPosts> UpVote(Posts post);
    }
}