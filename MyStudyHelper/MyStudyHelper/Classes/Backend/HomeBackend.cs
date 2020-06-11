using System.Linq;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class HomeBackend : IHomeBackend
    {
        //ObservableCollection, works like a List but the UI is automatically updated when changes are made
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        private readonly IPostsProxy _postsProxy;

        public HomeBackend(IPostsProxy postsProxy)
        {
            _postsProxy = postsProxy;
            GetPostInfo();
        }

        //Gets posts and orders them by most popular, proxy method used, API call made to get all posts from db
        public async void GetPostInfo()
        {
            var temp = await _postsProxy.GetAllPosts(); //Makes the API call in the proxy

            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    var orderedList = temp.OrderByDescending(x => x.UpVote.Count()); //Lambda expressions used to order posts by most popular (Via UpVote.Count)

                    foreach (var item in orderedList) //Foreach loop used to put each item from temp (List) into ObservableCollection
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