using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.Backend.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

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

        //Method to get all popular posts and display them in a list
        public async void GetPostInfo()
        {
            PostsMod = new ObservableCollection<IPosts>();
            var temp = await _postsProxy.GetAllPosts();
            
            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    var orderedList = temp.OrderByDescending(x => x.UpVoteId.Count()).ToList(); //Orders the records in descending order based on how many upvotes they contain, puts it into a new list

                    foreach (var item in orderedList)
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