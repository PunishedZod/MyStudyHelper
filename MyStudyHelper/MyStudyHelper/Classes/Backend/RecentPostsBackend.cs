using System.Collections.ObjectModel;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class RecentPostsBackend : IRecentPostsBackend
    {
        //ObservableCollection, works like a List but the UI is automatically updated when changes are made
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        private readonly IPostsProxy _postsProxy;

        public RecentPostsBackend(IPostsProxy postProxy)
        {
            _postsProxy = postProxy;
            GetPostInfo();
        }

        //Gets posts (most recent order), proxy method used, API call made to get all posts from db
        public async void GetPostInfo() 
        {
            var temp = await _postsProxy.GetRecentPosts(); //Makes the API call in the proxy

            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    foreach (var item in temp) //Foreach loop used to put each item from temp (List) into ObservableCollection
                    {
                        PostsMod.Add(item);
                        if (PostsMod.Count >= 10) break; //Limits the amount of posts added to a max of 10, exits Foreach loop
                    }
                }
                else return; //If temp is null, exit the method
            }
        }
    }
}