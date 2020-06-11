using System.Collections.ObjectModel;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class PostHistoryBackend : IPostHistoryBackend
    {
        //ObservableCollection, works like a List but the UI is automatically updated when changes are made
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        private readonly IPostsProxy _postsProxy;

        public PostHistoryBackend(IPostsProxy postsProxy)
        {
            _postsProxy = postsProxy;
            GetPostInfo();
        }

        //Gets all the users posts, userId sent to proxy method, API call made to get all posts with the userId from db
        public async void GetPostInfo()
        {
            var temp = await _postsProxy.GetPostsByUser(App.user.Id); //Makes the API call in the proxy

            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    foreach (var item in temp) //Foreach loop used to put each item from temp (List) into ObservableCollection
                    {
                        PostsMod.Add(item);
                    }
                }
                else return; //If temp is null, exit the method
            }
        }
    }
}