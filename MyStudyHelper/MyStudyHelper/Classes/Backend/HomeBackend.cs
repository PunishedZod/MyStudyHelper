using System.Linq;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class HomeBackend : IHomeBackend
    {
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        private readonly IPostsProxy _postsProxy = new PostsProxy("https://studyhelper.api.labnet.nz/");

        public HomeBackend(IPostsProxy postsProxy)
        {
            _postsProxy = postsProxy;
            GetPostInfo();
        }

        //Method to get all popular posts and display them in a list
        public async void GetPostInfo()
        {
            var temp = await _postsProxy.GetAllPosts();
            
            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    var orderedList = temp.OrderByDescending(x => x.UpVote.Count()).ToList(); //Orders the records in descending order based on how many upvotes they contain, puts it into a new list

                    foreach (var item in orderedList)
                    {
                        PostsMod.Add(item);
                        if (PostsMod.Count >= 10) break; //Limits the amount of posts being added to 10
                    }
                }
                else return;
            }
        }
    }
}