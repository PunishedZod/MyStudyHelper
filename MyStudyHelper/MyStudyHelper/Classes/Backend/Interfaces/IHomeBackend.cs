﻿using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.Classes.Backend.Interfaces
{
    public interface IHomeBackend
    {
        ObservableCollection<IPosts> PostsMod { get; set; }

        void GetPostInfo();
    }
}