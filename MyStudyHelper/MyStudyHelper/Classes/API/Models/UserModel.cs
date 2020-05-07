using System;
using System.Collections.Generic;
using System.Text;

namespace MyStudyHelper.Classes.API.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Uname { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Pword { get; set; }
    }
}