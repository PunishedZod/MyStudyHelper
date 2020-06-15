using System.Collections.Generic;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface IPostHistoryBackend
    {
        List<IPosts> PostsMod { get; set; }

        void GetPostInfo();
    }
}