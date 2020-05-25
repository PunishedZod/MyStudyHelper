namespace MyStudyHelper.Classes.API.Models.Interfaces
{
    public interface IUser
    {
        string Email { get; set; }
        string Id { get; set; }
        string Name { get; set; }
        string Pword { get; set; }
        string Uname { get; set; }
    }
}