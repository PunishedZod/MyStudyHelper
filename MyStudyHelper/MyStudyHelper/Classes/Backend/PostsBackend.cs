﻿using System.Collections.Generic;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class PostsBackend : IPostsBackend
    {
        //List of IPosts, used to store the items being grabbed from db
        public List<IPosts> PostsMod { get; set; } = new List<IPosts>();
        private readonly IPostsProxy _postsProxy;

        public PostsBackend(IPostsProxy postProxy)
        {
            _postsProxy = postProxy;
            GetPostInfo();
        }

        //Gets all posts, proxy method used, API call made to get all posts from db
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
                    }
                }
                else return; //If temp is null, exit the method
            }
        }
    }
}