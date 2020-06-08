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

            if (String.IsNullOrWhiteSpace(uname) || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(oldPword))
                return "Please fill in all required fields with valid information, cannot be left empty";
            else if (String.IsNullOrWhiteSpace(oldPword))
                return "You must enter your current password to make any changes";
            else if (oldPword != App.user.Pword)
                return "Current password entered is incorrect, please try again";
            else if (oldPword == newPword)
                return "New password cannot be the same as the old password";
            else if (!String.IsNullOrWhiteSpace(newPword) && newPword.Length < MinLength)
                return "New password length must be a minimum of 10 characters";
            else
                return null;
        }

        public async Task<IUser> Update(string uname, string email, string name, string pword)
        {
            if (pword == null || pword == "") //If new password is left empty fill it with the old password to ensure it does not return null or empty to the database
                pword = App.user.Pword;

            return await _userProxy.UpdateUserInfo(new User { Id = App.user.Id, Uname = uname, Email = email, Name = name, Pword = pword });
        }
    }
}