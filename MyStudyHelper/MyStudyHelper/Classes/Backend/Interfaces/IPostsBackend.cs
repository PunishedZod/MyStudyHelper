using MyStudyHelper.Classes.API.Models.Interfaces;
using System.Collections.ObjectModel;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface IPostsBackend
    {
        ObservableCollection<IPosts> PostsMod { get; set; }

        void GetPostInfo();
    }
}