using MyStudyHelper.Classes.API.Models.Interfaces;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface ILoginBackend
    {
        string CheckInfo(string uname, string pword);
        Task<IUser> Login(string uname, string pword);
    }
}