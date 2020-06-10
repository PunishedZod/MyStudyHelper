using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class PostsBackend : IPostsBackend
    {
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        private readonly IPostsProxy _postsProxy;

        public PostsBackend(IPostsProxy postProxy)
        {
            _postsProxy = postProxy;
            GetPostInfo();
        }

        //Method to get all posts and display them in a list
        public async void GetPostInfo()
        {
            var temp = await _postsProxy.GetRecentPosts();

            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    foreach (var item in temp)
                    {
                        PostsMod.Add(item);
                    }
                }
                else return;
            }
        }
    }
}