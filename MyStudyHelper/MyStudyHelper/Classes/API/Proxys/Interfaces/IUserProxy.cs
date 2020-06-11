using System.Threading.Tasks;
using MyStudyHelper.Classes.API.Models;

namespace MyStudyHelper.Classes.API.Proxys.Interfaces
{
    public interface IUserProxy
    {
        Task<User> GetUser(string uname, string pword);
        Task<string> PostUser(User user);
        Task<User> UpdateUser(User user);
    }
}