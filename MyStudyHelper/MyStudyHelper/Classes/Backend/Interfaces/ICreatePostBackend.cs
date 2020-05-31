using System.Threading.Tasks;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface ICreatePostBackend
    {
        string CheckInfo(string topic, string title, string message);
        Task<IPosts> CreatePost(string topic, string title, string message);
    }
}