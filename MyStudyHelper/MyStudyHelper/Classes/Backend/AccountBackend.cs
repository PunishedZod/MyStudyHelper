using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
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

        //Checks the info sent through from XAML.cs, If Else statements determine whether valid or not, if valid return null, if not return error message
        public string CheckInfo(string uname, string email, string name, string oldPword, string newPword)
        {
            var MinLength = 10; //Sets the password length
            var emailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"; //Uses a Regex pattern to validate the email address

             if (String.IsNullOrWhiteSpace(oldPword))
                return "You must enter your current password to make any changes";
            else if (String.IsNullOrWhiteSpace(uname) || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(oldPword))
                return "Please fill in all required fields with valid information, cannot be left empty";
            else if (!Regex.IsMatch(email, emailPattern))
                return "Email address entered is not valid";
            else if (oldPword != App.user.Pword)
                return "Current password entered is incorrect, please try again";
            else if (oldPword == newPword)
                return "New password cannot be the same as the old password";
            else if (!String.IsNullOrWhiteSpace(newPword) && newPword.Length < MinLength)
                return "New password length must be a minimum of 10 characters";
            else return null;
        }

        //An asynchronous Task which takes in the info which is sent to proxy method, API call made to update user in db
        public async Task<IUser> Update(string uname, string email, string name, string pword)
        {
            if (String.IsNullOrWhiteSpace(pword)) pword = App.user.Pword; //If new pword is empty fill with old pword, ensures it does not return null or empty to db

            return await _userProxy.UpdateUser(new User { Id = App.user.Id, Uname = uname, Email = email, Name = name, Pword = pword });
        }
    }
}