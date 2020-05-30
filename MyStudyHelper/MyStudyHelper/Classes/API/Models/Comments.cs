using MyStudyHelper.Classes.API.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyStudyHelper.Classes.API.Models
{
    //Model class to get and set data for the table and database, an interface of the model is used to ensure we aren't directly taking it from the model itself
    public class Comments : IComments
    {
        public string Id { get; set; }
        public string Uname { get; set; }
        public string Comment { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
    }
}