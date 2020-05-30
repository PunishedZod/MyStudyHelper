using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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