using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class RecentPostsBackend : IRecentPostsBackend
    {
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        private readonly IPostsProxy _postsProxy = new PostsProxy("https://studyhelper.api.labnet.nz/");

        public RecentPostsBackend(IPostsProxy postProxy)
        {
            _postsProxy = postProxy;
            GetPostInfo();
        }

        //Method to get all recent posts and display them in a list
        public async void GetPostInfo() 
        {
            PostsMod = new ObservableCollection<IPosts>();
            var temp = await _postsProxy.GetRecentPosts();

            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    foreach (var item in temp)
                    {
                        PostsMod.Add(item);

                        if (PostsMod.Count >= 10) //Limits the amount of posts being added to 10
                        {
                            break;
                        }
                    }
                }
                else
                    return;
            }
        }
    }
}