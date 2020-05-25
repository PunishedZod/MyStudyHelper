using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend
{
    class RegisterBackend
    {
        private IUserProxy _userProxy;

        public RegisterBackend(IUserProxy userProxy)
        {
            _userProxy = userProxy;
        }

        public string CheckInfo(string uname, string email, string name, string pword1, string pword2)
        {
            if (String.IsNullOrWhiteSpace(uname) && String.IsNullOrWhiteSpace(email)
                && String.IsNullOrWhiteSpace(pword1) && String.IsNullOrWhiteSpace(pword2))
                return "Please fill in all required field(s) with valid info";
            else
                return null;
        }

        public async Task<IUser> Register(User user)
        {
            return await _userProxy.PostUserInfo(user);
        }
    }
}