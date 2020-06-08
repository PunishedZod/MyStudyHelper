using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class CommentHistoryBackend : ICommentHistoryBackend
    {
        public ObservableCollection<IComments> CommentsMod { get; set; } = new ObservableCollection<IComments>();
        private readonly ICommentsProxy _commentsProxy = new CommentsProxy("https://studyhelper.api.labnet.nz/");
        private readonly IPostsProxy _postsProxy;

        public CommentHistoryBackend(ICommentsProxy commentsProxy, IPostsProxy postsProxy)
        {
            _commentsProxy = commentsProxy;
            _postsProxy = postsProxy;
            GetCommentInfo();
        }

        public async void GetCommentInfo()
        {
            CommentsMod = new ObservableCollection<IComments>();
            var temp = await _commentsProxy.GetCommentsByUser(App.user.Id);

            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    foreach (var item in temp)
                    {
                        CommentsMod.Add(item);
                    }
                }
                else
                    CommentsMod.Add(temp[0]);
            }
        }

        public async Task<Posts> GetPost(string postId)
        {
            return await _postsProxy.GetPostInfo(postId); //Returns the username and password to a method in userproxy
        }
    }
}