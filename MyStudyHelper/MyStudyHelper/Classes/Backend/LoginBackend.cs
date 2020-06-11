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

        //Checks the info sent through from XAML.cs, If Else statements determine whether valid or not, if valid return null, if not return error message
        public string CheckInfo(string uname, string pword)
        {
            if (String.IsNullOrWhiteSpace(uname) && String.IsNullOrWhiteSpace(pword))
                return "Username & password cannot be left empty";
            else if (String.IsNullOrWhiteSpace(uname))
                return "Username cannot be left empty";
            else if (String.IsNullOrWhiteSpace(pword))
                return "Password cannot be left empty";
            else return null;
        }

        //An asynchronous Task which takes in the info which is sent to proxy method, API call made to get user in db if exists
        public async Task<IUser> Login(string uname, string pword)
        {
            return await _userProxy.GetUser(uname, pword);
        }
    }
}