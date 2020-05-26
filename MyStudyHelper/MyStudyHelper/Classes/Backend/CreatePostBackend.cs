using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.Backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend
{
    public class CreatePostBackend : ICreatePostBackend
    {
        private readonly IPostsProxy _postsProxy;

        public CreatePostBackend(IPostsProxy postsProxy)
        {
            _postsProxy = postsProxy;
        }

        public string CheckInfo(string topic, string title, string message)
        {
            if (String.IsNullOrWhiteSpace(topic) || String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(message))
                return "Please fill in all fields, cannot be left empty";
            else
                return null;
        }

        public async Task<Posts> CreatePost(string topic, string title, string message)
        {
            return await _postsProxy.PostPost(new Posts { Topic = topic, Title = title, Content = message });
        }
    }
}