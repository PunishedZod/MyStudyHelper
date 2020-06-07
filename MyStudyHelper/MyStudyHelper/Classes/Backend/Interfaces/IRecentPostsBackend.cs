using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface IRecentPostsBackend
    {
        ObservableCollection<IPosts> PostsMod { get; set; }

        void GetPostInfo();
    }
}