using System.Threading.Tasks;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface IAccountBackend
    {
        string CheckInfo(string uname, string email, string name, string pword1, string pword2);
        Task<IUser> Update(string uname, string email, string name, string pword);
    }
}