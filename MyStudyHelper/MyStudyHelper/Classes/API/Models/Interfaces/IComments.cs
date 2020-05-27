namespace MyStudyHelper.Classes.API.Models.Interfaces
{
    public interface IComments
    {
        string Comment { get; set; }
        string Id { get; set; }
        string PostId { get; set; }
        string UId { get; set; }
        string Uname { get; set; }
    }
}