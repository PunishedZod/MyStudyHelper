using System;
using System.Threading.Tasks;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class AccountBackend : IAccountBackend
    {
        private readonly IUserProxy _userProxy;

        public AccountBackend(IUserProxy userProxy)
        {
            _userProxy = userProxy;
        }

        public string CheckInfo(string uname, string email, string name, string oldPword, string newPword)
        {
            var MinLength = 10;

            if (String.IsNullOrWhiteSpace(uname) && String.IsNullOrWhiteSpace(email) && String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(oldPword) && String.IsNullOrWhiteSpace(newPword))
                return "All fields are empty, please fill in atleast one field";
            else if (String.IsNullOrWhiteSpace(oldPword))
                return "You must enter your current password to make any changes";
            else if (oldPword != MainPage.user.Pword)
                return "Current password entered is incorrect, please try again";
            else if (uname == MainPage.user.Uname || email == MainPage.user.Email || name == MainPage.user.Name)
                return "One or more field(s) contain old user info, please enter updated info";
            else if (oldPword == newPword)
                return "New password cannot be the same as the old password";
            else if (!String.IsNullOrWhiteSpace(newPword) && newPword.Length < MinLength)
                return "New password length must be a minimum of 10 characters";
            else
                return null;
        }

        public async Task<IUser> Update(string uname, string email, string name, string pword)
        {
            if (uname == null)
                uname = MainPage.user.Uname;
            if (email == null)
                email = MainPage.user.Email;
            if (name == null)
                name = MainPage.user.Name;
            if (pword == null)
                pword = MainPage.user.Pword;

            return await _userProxy.UpdateUserInfo(new User { Id = MainPage.user.Id, Uname = uname, Email = email, Name = name, Pword = pword });
        }
    }
}