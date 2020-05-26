using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend
{
    public class AccountBackend
    {
        private readonly IUserProxy _userProxy;

        public AccountBackend(IUserProxy userProxy)
        {
            _userProxy = userProxy;
        }

        //public string CheckInfo(string uname, string email, string name, string pword1, string pword2)
        //{
        //    var MinLength = 12;

        //    if (String.IsNullOrWhiteSpace(uname) || String.IsNullOrWhiteSpace(email)
        //        || String.IsNullOrWhiteSpace(pword1) || String.IsNullOrWhiteSpace(pword2))
        //        return "Please fill in all required field(s) with valid information, cannot be left empty";
        //    else if (pword1.Length < MinLength)
        //        return "Password length must be a minimum of 12 characters long";
        //    else if (pword1 != pword2)
        //        return "Both passwords must be matching each other";
        //    else
        //        return null;
        //}

        //public async Task<string> Update(string uname, string email, string name, string pword)
        //{
        //    return await _userProxy.UpdateUserInfo(new User { Uname = uname, Email = email, Name = name, Pword = pword });
        //}
    }
}