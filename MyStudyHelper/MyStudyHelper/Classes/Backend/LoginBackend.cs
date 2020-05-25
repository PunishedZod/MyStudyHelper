using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend
{
    public class LoginBackend : ILoginBackend
    {
        private readonly IUserProxy _userProxy;

        public LoginBackend(IUserProxy userProxy)
        {
            _userProxy = userProxy;
        }

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

        public async Task<IUser> Login(string uname, string pword)
        {
            return await _userProxy.GetUserInfo(uname, pword);
        }
    }
}