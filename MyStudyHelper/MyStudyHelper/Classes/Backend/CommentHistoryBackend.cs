using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class CommentHistoryBackend : ICommentHistoryBackend
    {
        //ObservableCollection, works like a List but the UI is automatically updated when changes are made
        public ObservableCollection<IComments> CommentsMod { get; set; } = new ObservableCollection<IComments>();
        private readonly ICommentsProxy _commentsProxy;
        private readonly IPostsProxy _postsProxy;

        public CommentHistoryBackend(ICommentsProxy commentsProxy, IPostsProxy postsProxy)
        {
            _commentsProxy = commentsProxy;
            _postsProxy = postsProxy;
            GetCommentInfo();
        }

        //Gets all the users comments, userId sent to proxy method, API call made to get all comments with the userId from db
        public async void GetCommentInfo()
        {
            var temp = await _commentsProxy.GetCommentsByUser(App.user.Id); //Makes the API call in the proxy

            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    foreach (var item in temp) //Foreach loop used to put each item from temp (List) into ObservableCollection
                    {
                        CommentsMod.Add(item);
                    }
                }
                else return; //If temp is null, exit the method
            }
        }

        //An asynchronous Task which takes in the id which is sent to proxy method, API call made to get post from db
        public async Task<Posts> GetPost(string postId)
        {
            return await _postsProxy.GetPost(postId);
        }
    }
}