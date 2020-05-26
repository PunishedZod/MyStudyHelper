using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyStudyHelper.Classes.Backend
{
    public class RecentPostsBackend : IRecentPostsBackend
    {
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        private readonly IPostsProxy _postsProxy = new PostsProxy("https://localhost:44323/");

        public RecentPostsBackend()
        {
            GetPostInfo();
        }

        public RecentPostsBackend(IPostsProxy postProxy)
        {
            _postsProxy = postProxy;
            GetPostInfo();
        }

        public async void GetPostInfo() //Method to get all recent posts (descending order) and display them in a list
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
                        if (PostsMod.Count >= 15)
                        {
                            break;
                        }
                    }
                }
                else
                    PostsMod.Add(temp[0]);
            }
        }
    }
}