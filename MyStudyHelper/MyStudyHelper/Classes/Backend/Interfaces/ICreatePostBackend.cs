using MyStudyHelper.Classes.API.Models;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface ICreatePostBackend
    {
        string CheckInfo(string topic, string title, string message);
        Task<Posts> CreatePost(string topic, string title, string message);
    }
}