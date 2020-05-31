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

        //Gets all posts, returns a list
        //CAN RETURN NULL IF THERE ARE NO POSTS
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
                else
                    return null;
            }
            else
                return null;
        }

        //Gets all posts by user, pass through the userId
        //CAN RETURN NULL IF THERE ARE NO POSTS
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
                else
                    return null;
            }
            else
                return null;
        }

        //Gets all posts, (most recent via descending order)
        //CAN RETURN NULL IF THERE ARE NO POSTS
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
                else
                    return null;
            }
            else
                return null;
        }

        //Call when posting a post, takes in a post class item
        public async Task<Posts> PostPost(Posts post)
        {
            HttpClient http = new HttpClient();
            var response = await http.PostAsJsonAsync($"{_baseAddress}api/Posts", post);
            return await response.Content.ReadAsAsync<Posts>();
        }

        //Call when updating a post, takes in a post class item
        public async Task<Posts> UpdatePost(Posts post)
        {
            HttpClient http = new HttpClient();
            var response = await http.PutAsJsonAsync($"{_baseAddress}api/Posts", post);
            return await response.Content.ReadAsAsync<Posts>();
        }

        //Call when deleting a post, takes in the postId
        //Returns a string detailing if it was a success or failure
        public async Task<string> DeletePost(string id)
        {
            HttpClient http = new HttpClient();
            var response = await http.DeleteAsync($"{_baseAddress}api/Posts/{id}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}