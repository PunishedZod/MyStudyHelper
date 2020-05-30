using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.API.Models
{
    //Model class to get and set data for the table and database, an interface of the model is used to ensure we aren't directly taking it from the model itself
    public class Posts : IPosts
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] UpVote { get; set; }
        public string[] DownVote { get; set; }
        public string UserId { get; set; }
        public string Uname { get; set; }
    }
}