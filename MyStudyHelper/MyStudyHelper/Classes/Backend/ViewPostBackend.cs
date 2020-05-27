using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend
{
    public class ViewPostBackend : IViewPostBackend
    {
        public ObservableCollection<Comments> CommentsList { get; set; } = new ObservableCollection<Comments>();
        private readonly IPostsProxy _postsProxy = new PostsProxy("https://localhost:44323/");
        private readonly ICommentsProxy _commentsProxy = new CommentsProxy("https://localhost:44323/");

        public ViewPostBackend(IPostsProxy postsProxy)
        {
            _postsProxy = postsProxy;
        }

        public ViewPostBackend(ICommentsProxy commentsProxy)
        {
            _commentsProxy = commentsProxy;
        }

        public async Task<IPosts> UpdateUpVote(Posts post) //Takes in a Post, adds the users id to the upvoteid, returns post id and post to post proxy
        {
            var id = post.Id;
            var info = post.UpVoteId.ToList();
            info.Add(MainPage.user.Id);
            post.UpVoteId = info.ToArray();

            return await _postsProxy.UpdatePost(id, post);
        }

        public async Task<IPosts> UpdateDownVote(Posts post) //Takes in a Post, adds the users id to the downvoteid, returns post id and post to post proxy
        {
            var id = post.Id;
            var info = post.DownVoteId.ToList();
            info.Add(MainPage.user.Id);
            post.DownVoteId = info.ToArray();

            return await _postsProxy.UpdatePost(id, post);
        }

        public async void GetCommentsInfo(string id) //This is where the data will be added from the API to the list
        {
            var temp = await _commentsProxy.GetCommentsByPost(id);
            if (temp != null)
            {
                if (temp.Count > 1)
                {
                    foreach (var item in temp)
                    {
                        CommentsList.Add(item);
                    }
                }
                else
                    return;
            }
        }

        public async Task<Comments> SendComment(string comment, string postId) //Takes in the users comment and the postid, returns the comment info including the users username and id to comment proxy
        {
            return await _commentsProxy.PostComments(new Comments { Uname = MainPage.user.Uname, Comment = comment, PostId = postId, UId = MainPage.user.Id });
        }
    }
}