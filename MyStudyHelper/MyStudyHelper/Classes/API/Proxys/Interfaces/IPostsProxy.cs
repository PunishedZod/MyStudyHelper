﻿using MyStudyHelper.Classes.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.API.Proxys.Interfaces
{
    public interface IPostsProxy
    {
        Task<string> DeletePost(string id);
        Task<List<Posts>> GetAllPosts();
        Task<List<Posts>> GetPostsByUser(string userId);
        Task<List<Posts>> GetRecentPosts();
        Task<Posts> PostPost(Posts post);
        Task<Posts> UpdatePost(Posts post);
    }
}