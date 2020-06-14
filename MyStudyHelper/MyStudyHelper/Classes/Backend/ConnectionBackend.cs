using Xamarin.Essentials;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.Classes.Backend
{
    public class ConnectionBackend : IConnectionBackend
    {
        public bool HasConnection()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet) return false;
            else return true;
        }
    }
}