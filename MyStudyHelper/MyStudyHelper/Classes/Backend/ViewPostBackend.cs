using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class ViewPostBackend : IViewPostBackend
    {
        //ObservableCollection, works like a List but the UI is automatically updated when changes are made
        public ObservableCollection<IComments> CommentsList { get; set; } = new ObservableCollection<IComments>();
        private readonly ICommentsProxy _commentsProxy;
        private readonly IPostsProxy _postsProxy;

        public ViewPostBackend(ICommentsProxy commentsProxy, IPostsProxy postsProxy)
        {
            _commentsProxy = commentsProxy;
            _postsProxy = postsProxy;
        }

        //Updates post, specifically UpVote, a Post is sent to proxy method, API call made to update the post in db (Adds userId into UpVote array)
        public async Task<IPosts> UpVote(Posts post)
        {
            var info = post.UpVote.ToList();
            info.Add(App.user.Id);
            post.UpVote = info.ToArray();

            return await _postsProxy.UpdatePost(post);
        }

        //Updates post, specifically DownVote, a Post is sent to proxy method, API call made to update the post in db (Adds userId into DownVote array)
        public async Task<IPosts> DownVote(Posts post)
        {
            var info = post.DownVote.ToList();
            info.Add(App.user.Id);
            post.DownVote = info.ToArray();

            return await _postsProxy.UpdatePost(post);
        }

        //Gets all the posts comments, postId sent to proxy method, API call made to get all comments with the postId from db
        public async void GetCommentsInfo(string postId)
        {
            var temp = await _commentsProxy.GetCommentsByPost(postId); //Makes the API call in the proxy

            if (temp != null)
            {
                if (temp.Count >= 1)
                {
                    foreach (var item in temp) //Foreach loop used to put each item from temp (List) into ObservableCollection
                    {
                        CommentsList.Add(item);
                    }
                }
                else return; //If temp is null, exit the method
            }
        }

        //An asynchronous Task which takes in the info which is sent to proxy method, API call made to post comment in db
        public async Task<IComments> PostComment(string comment, string postId)
        {
            return await _commentsProxy.PostComment(new Comments { Uname = App.user.Uname, Comment = comment, PostId = postId, UserId = App.user.Id });
        }
    }
}