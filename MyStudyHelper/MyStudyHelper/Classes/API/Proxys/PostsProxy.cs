using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.API.Proxys
{
    public class PostsProxy : IPostsProxy
    {
        private readonly string _baseAddress;

        public PostsProxy(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        //Gets all posts, returns a list, returns null if no posts
        public async Task<List<Posts>> GetAllPosts()
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };

            var url = String.Format($"api/Posts");
            HttpResponseMessage response = http.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var posts = await response.Content.ReadAsAsync<List<Posts>>();

                if (posts != null)
                {
                    return posts;
                }
                else return null;
            }
            else return null;
        }

        //Gets all posts by user, pass through the userId, returns a list, pass through the userId, returns null if no posts
        public async Task<List<Posts>> GetPostsByUser(string userId)
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };

            var url = String.Format($"api/Posts/UserId={userId}");
            HttpResponseMessage response = http.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var posts = await response.Content.ReadAsAsync<List<Posts>>();

                if (posts != null)
                {
                    return posts;
                }
                else return null;
            }
            else return null;
        }

        //Gets all posts in most recent order, returns a list, returns null if no posts
        public async Task<List<Posts>> GetRecentPosts()
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };

            var url = String.Format($"api/Posts/RecentPosts");
            HttpResponseMessage response = http.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var posts = await response.Content.ReadAsAsync<List<Posts>>();

                if (posts != null)
                {
                    return posts;
                }
                else return null;
            }
            else return null;
        }

        //Gets a post, pass through the postId, returns null if no post
        public async Task<Posts> GetPost(string postId)
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };

            var url = String.Format($"api/Posts/{postId}");
            HttpResponseMessage response = http.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var posts = await response.Content.ReadAsAsync<Posts>();

                if (posts != null)
                {
                    return posts;
                }
                else return null;
            }
            else return null;
        }

        //Call when posting a post, takes in a post class item, returns the post
        public async Task<Posts> PostPost(Posts post)
        {
            HttpClient http = new HttpClient();
            var response = await http.PostAsJsonAsync($"{_baseAddress}api/Posts", post);
            return await response.Content.ReadAsAsync<Posts>();
        }

        //Call when updating a post, takes in a post class item, returns the post
        public async Task<Posts> UpdatePost(Posts post)
        {
            HttpClient http = new HttpClient();
            var response = await http.PutAsJsonAsync($"{_baseAddress}api/Posts", post);
            return await response.Content.ReadAsAsync<Posts>();
        }
    }
}