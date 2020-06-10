﻿using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class ViewPostBackend : IViewPostBackend
    {
        public ObservableCollection<IComments> CommentsList { get; set; } = new ObservableCollection<IComments>();
        private readonly ICommentsProxy _commentsProxy;
        private readonly IPostsProxy _postsProxy;
        public ViewPostBackend(IPostsProxy postsProxy, ICommentsProxy commentsProxy)
        {
            _postsProxy = postsProxy;
            _commentsProxy = commentsProxy;
        }

        //Takes in a Post, adds the users id to the upvoteid, returns post id and post to post proxy
        public async Task<IPosts> UpVote(Posts post)
        {
            var info = post.UpVote.ToList();
            info.Add(App.user.Id);
            post.UpVote = info.ToArray();

            return await _postsProxy.UpdatePost(post);
        }

        //Takes in a post, adds the users id to the downvoteid, returns post id and post to post proxy
        public async Task<IPosts> DownVote(Posts post)
        {
            var info = post.DownVote.ToList();
            info.Add(App.user.Id);
            post.DownVote = info.ToArray();

            return await _postsProxy.UpdatePost(post);
        }

        //This is where the data will be added from the API to the list
        public async void GetCommentsInfo(string id)
        {
            var temp = await _commentsProxy.GetCommentsByPost(id);

            if (temp != null)
            {
                if (temp.Count >= 1)
                {
                    foreach (var item in temp)
                    {
                        CommentsList.Add(item);
                    }
                }
                else return;
            }
        }

        //Takes in the users comment and the postid, returns the comment info including the users username and id to comment proxy
        public async Task<IComments> PostComment(string comment, string postId)
        {
            return await _commentsProxy.PostComments(new Comments { Uname = App.user.Uname, Comment = comment, PostId = postId, UserId = App.user.Id });
        }
    }
}