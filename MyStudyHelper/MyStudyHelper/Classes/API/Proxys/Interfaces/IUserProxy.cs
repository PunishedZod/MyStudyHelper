using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.API.Proxys.Interfaces
{
    public interface IUserProxy
    {
        Task<User> GetUserInfo(string uname, string pword);
        Task<string> PostUserInfo(User user);
        //Task<string> UpdateUserInfo(User user);
    }
}