using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.API.Models
{
    //Model class to get and set data for the table and database, an interface of the model is used to ensure we aren't directly taking it from the model itself
    public class User : IUser
    {
        public string Id { get; set; }
        public string Uname { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Pword { get; set; }
    }
}