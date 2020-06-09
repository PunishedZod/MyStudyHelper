using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;


namespace MyStudyHelper.Classes.Backend
{
    public class PostHistoryBackend : IPostHistoryBackend
    {
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        private readonly IPostsProxy _postsProxy = new PostsProxy("https://studyhelper.api.labnet.nz/");

        public PostHistoryBackend(IPostsProxy postsProxy)
        {
            _postsProxy = postsProxy;
            GetPostInfo();
        }

        public async void GetPostInfo()
        {
            var temp = await _postsProxy.GetPostsByUser(App.user.Id);

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