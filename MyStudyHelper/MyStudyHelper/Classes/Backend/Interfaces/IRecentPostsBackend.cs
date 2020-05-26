using MyStudyHelper.Classes.API.Models.Interfaces;
using System.Collections.ObjectModel;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface IRecentPostsBackend
    {
        ObservableCollection<IPosts> PostsMod { get; set; }

        void GetPostInfo();
    }
}