using System.Threading.Tasks;
using MyStudyHelper.Classes.API.Models;

namespace MyStudyHelper.Classes.API.Proxys.Interfaces
{
    public interface IUserProxy
    {
        Task<User> GetUserInfo(string uname, string pword);
        Task<string> PostUserInfo(User user);
        Task<User> UpdateUserInfo(User user);
    }
}