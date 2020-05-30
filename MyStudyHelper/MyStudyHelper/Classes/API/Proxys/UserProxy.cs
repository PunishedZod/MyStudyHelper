using System;
using System.Net.Http;
using System.Threading.Tasks;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.API.Proxys
{
    public class UserProxy : IUserProxy
    {
        private readonly string _baseAddress;

        public UserProxy(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        //Call when getting user info, takes the username and the password for a user
        public async Task<User> GetUserInfo(string uname, string pword)
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };

            var url = String.Format($"api/User/Username={uname}&Password={pword}");
            HttpResponseMessage response = http.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsAsync<User>();
     
                if (user != null)
                {
                    return user;
                }
                else
                    return null;
            }
            else
                return null;
        }

        //Call when creating a user
        //Returns a string detailing if it was a success or failure
        public async Task<string> PostUserInfo(User user)
        {
            HttpClient http = new HttpClient();
            var response = await http.PostAsJsonAsync($"{_baseAddress}api/User", user);
            return await response.Content.ReadAsStringAsync();
        }

        //Call when updating a user
        //Returns a string detailing if it was a success or failure
        public async Task<User> UpdateUserInfo(User user)
        {
            HttpClient http = new HttpClient();
            var response = await http.PutAsJsonAsync($"{_baseAddress}api/User", user);
            return await response.Content.ReadAsAsync<User>();
        }
    }
}