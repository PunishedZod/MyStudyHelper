namespace MyStudyHelper.Classes.API.Models.Interfaces
{
    public interface IComments
    {
        string Comment { get; set; }
        int DownVote { get; set; }
        string Id { get; set; }
        string PostId { get; set; }
        int UpVote { get; set; }
        string Uname { get; set; }
    }
}