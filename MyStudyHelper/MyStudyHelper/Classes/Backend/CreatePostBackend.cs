using System;
using System.Threading.Tasks;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class CreatePostBackend : ICreatePostBackend
    {
        private readonly IPostsProxy _postsProxy = new PostsProxy("https://studyhelper.api.labnet.nz/");

        public CreatePostBackend(IPostsProxy postsProxy)
        {
            _postsProxy = postsProxy;
        }

        public string CheckInfo(string topic, string title, string message)
        {
            if (topic == "Choose Topic *")
                return "Please choose a topic, cannot be left empty";
            else if (String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(message))
                return "Please fill in all fields, cannot be left empty";
            else
                return null;
        }

        public async Task<IPosts> CreatePost(string topic, string title, string message)
        {
            string[] baseVote = new string[] { "BaseVote" };

            return await _postsProxy.PostPost(new Posts { Topic = topic, Title = title, Content = message, UpVote = baseVote, DownVote = baseVote, UserId = App.user.Id, Uname = App.user.Uname });
        }
    }
}