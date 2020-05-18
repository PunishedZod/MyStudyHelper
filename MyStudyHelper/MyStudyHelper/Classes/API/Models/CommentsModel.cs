using System;
using System.Collections.Generic;
using System.Text;

namespace MyStudyHelper.Classes.API.Models
{
    public class CommentsModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public int PostId { get; set; }
    }
}