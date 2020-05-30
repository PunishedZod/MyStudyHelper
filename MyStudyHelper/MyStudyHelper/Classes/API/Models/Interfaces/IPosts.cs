namespace MyStudyHelper.Classes.API.Models.Interfaces
{
    public interface IPosts
    {
        string Content { get; set; }
        string[] DownVote { get; set; }
        string Id { get; set; }
        string Title { get; set; }
        string Topic { get; set; }
        string Uname { get; set; }
        string[] UpVote { get; set; }
        string UserId { get; set; }
    }
}