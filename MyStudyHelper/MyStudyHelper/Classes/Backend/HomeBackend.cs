using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.Backend.Interfaces;
using System.Collections.ObjectModel;

namespace MyStudyHelper.Classes.Backend
{
    public class HomeBackend : IHomeBackend
    {
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        private readonly IPostsProxy _postsProxy = new PostsProxy("https://localhost:44323/");

        public HomeBackend()
        {
            GetPostInfo();
        }

        public HomeBackend(IPostsProxy postProxy)
        {
            _postsProxy = postProxy;
            GetPostInfo();
        }

        public async void GetPostInfo() //Method to get all popular posts and display them in a list
        {
            PostsMod = new ObservableCollection<IPosts>();
            var temp = await _postsProxy.GetPopularPosts();

            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    foreach (var item in temp)
                    {
                        PostsMod.Add(item);
                    }
                }
                else
                    PostsMod.Add(temp[0]);
            }
        }
    }
}