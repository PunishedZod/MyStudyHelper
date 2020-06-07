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
        Task<IComments> SendComment(string comment, string postId);
        Task<IPosts> UpdateDownVote(Posts post);
        Task<IPosts> UpdateUpVote(Posts post);
    }
}