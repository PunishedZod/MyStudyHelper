using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        //CAN RETURN NULL IF THE USER DOESNT EXIST OR THERE IS AN ERROR
        public async Task<IUser> GetUserInfo(string uname, string pword)
        {
            var http = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };

            var url = String.Format($"api/User/Username={uname}&Password={pword}");
            HttpResponseMessage response = http.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var user = response.Content.ReadAsAsync<User>();
                if (user != null)
                {
                    return await user;
                }
                else
                    return null;
            }
            else
                return null;
        }

        //Call when creating a user
        //Returns a string detailing if it was a success or failure
        public async Task<string> PostUserInfo(User user) //NOTE: UNSURE IF THIS FUNCTION WORKS FULLY OR AT ALL !!!
        {
            HttpClient http = new HttpClient();
            var response = await http.PostAsJsonAsync($"{_baseAddress}api/User", user);
            return await response.Content.ReadAsStringAsync();
        }

        //NOTE: DOES NOT WORK NEEDS PROPER WORK DONE !!!
        //Call when updating a user
        //Returns a string detailing if it was a success or failure
        //public async Task<string> UpdateUserInfo(User user) //NOTE: UNSURE IF THIS FUNCTION WORKS FULLY OR AT ALL !!!
        //{
        //    HttpClient http = new HttpClient();
        //    var response = await http.PutAsJsonAsync($"{_baseAddress}api/User", user);
        //    return await response.Content.ReadAsStringAsync();
        //}
    }
}