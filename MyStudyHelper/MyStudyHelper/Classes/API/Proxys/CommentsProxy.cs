using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.API.Proxys
{
    public class CommentsProxy : ICommentsProxy
    {
        private readonly string _baseAddress;

        public CommentsProxy(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        //Use when person views post in detail, Pass through the postID
        //CAN RETURN NULL IF THERE ARE NO COMMENTS
        public async Task<List<Comments>> GetCommentsByPost(string postId)
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };

            var url = String.Format($"api/Comments/{postId}");
            HttpResponseMessage response = http.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var comments = await response.Content.ReadAsAsync<List<Comments>>();
                if (comments != null)
                {
                    return comments;
                }
                else
                    return null;
            }
            else
                return null;
        }

        //Called when posting, Takes in a Comment class item
        //Returns a string detailing if it was a success or failure
        public async Task<Comments> PostComments(Comments comment)
        {
            HttpClient http = new HttpClient();
            var response = await http.PostAsJsonAsync($"{_baseAddress}api/Comments", comment);
            return await response.Content.ReadAsAsync<Comments>();
        }

        //Deleting a post takes in the commentsID
        //Returns a string detailing if it was a success or failure
        public async Task<string> DeleteComment(int id)
        {
            HttpClient http = new HttpClient();
            var response = await http.DeleteAsync($"{_baseAddress}api/Comments/{id}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}