using MyStudyHelper.Classes.API.Models.Interfaces;
using System.Threading.Tasks;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    interface IRegisterBackend
    {
        string CheckInfo(string uname, string email, string name, string pword1, string pword2);
        Task<string> Register(string uname, string email, string name, string pword);
    }
}