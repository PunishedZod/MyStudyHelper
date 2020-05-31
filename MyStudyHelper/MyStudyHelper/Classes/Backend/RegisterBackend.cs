using System;
using System.Threading.Tasks;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class RegisterBackend : IRegisterBackend
    {
        private readonly IUserProxy _userProxy;
        
        public RegisterBackend(IUserProxy userProxy)
        {
            _userProxy = userProxy;
        }

        public string CheckInfo(string uname, string email, string name, string pword1, string pword2)
        {
            var MinLength = 10;

            if (String.IsNullOrWhiteSpace(uname) || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(pword1) || String.IsNullOrWhiteSpace(pword2))
                return "Please fill in all required field(s) with valid information, cannot be left empty";
            else if (pword1.Length < MinLength)
                return "Password length must be a minimum of 10 characters";
            else if (pword1 != pword2)
                return "Both passwords must be matching each other";
            else
                return null;
        }

        public async Task<string> Register(string uname, string email, string name, string pword)
        {
            return await _userProxy.PostUserInfo(new User { Uname = uname, Email = email, Name = name, Pword = pword });
        }
    }
}