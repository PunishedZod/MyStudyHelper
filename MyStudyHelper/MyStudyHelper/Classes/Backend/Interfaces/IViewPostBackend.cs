using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend
{
    public interface IViewPostBackend
    {
        ObservableCollection<Comments> CommentsList { get; set; }

        void GetCommentsInfo(string id);
        Task<Comments> SendComment(string comment, string postId);
        Task<IPosts> UpdateDownVote(Posts post);
        Task<IPosts> UpdateUpVote(Posts post);
    }
}