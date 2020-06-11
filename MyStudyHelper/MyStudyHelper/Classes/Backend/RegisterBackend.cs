﻿using System;
using System.Threading.Tasks;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class RegisterBackend : IRegisterBackend
    {
        private readonly IUserProxy _userProxy;
        private readonly int MinLength = 10; //Sets the password length

        public RegisterBackend(IUserProxy userProxy)
        {
            _userProxy = userProxy;
        }

        //Checks the info sent through from XAML.cs, If Else statements determine whether valid or not, if valid return null, if not return error message
        public string CheckInfo(string uname, string email, string name, string pword1, string pword2)
        {
            if (String.IsNullOrWhiteSpace(uname) || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(pword1) || String.IsNullOrWhiteSpace(pword2))
                return "Please fill in all required fields with valid information, cannot be left empty";
            else if (pword1.Length < MinLength)
                return "Password length must be a minimum of 10 characters";
            else if (pword1 != pword2)
                return "Both passwords must be matching each other";
            else return null;
        }

        //An asynchronous Task which takes in the info which is sent to proxy method, API call made to post user in db
        public async Task<string> Register(string uname, string email, string name, string pword)
        {
            return await _userProxy.PostUser(new User { Uname = uname, Email = email, Name = name, Pword = pword });
        }
    }
}