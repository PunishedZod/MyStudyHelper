using System;
using System.Threading.Tasks;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class LoginBackend : ILoginBackend
    {
        private readonly IUserProxy _userProxy;

        public LoginBackend(IUserProxy userProxy)
        {
            _userProxy = userProxy;
        }

        //Validates the username and password entered through the textboxes in the frontend
        public string CheckInfo(string uname, string pword)
        {
            if (String.IsNullOrWhiteSpace(uname) && String.IsNullOrWhiteSpace(pword))
                return "Username & password cannot be left empty";
            else if (String.IsNullOrWhiteSpace(uname))
                return "Username cannot be left empty";
            else if (String.IsNullOrWhiteSpace(pword))
                return "Password cannot be left empty";
            else
                return null;
        }

        //Takes in username and password entered and sends it to the userproxy to make a GET call to the API for the user info
        public async Task<IUser> Login(string uname, string pword)
        {
            return await _userProxy.GetUserInfo(uname, pword); //Returns the username and password to a method in userproxy
        }
    }
}