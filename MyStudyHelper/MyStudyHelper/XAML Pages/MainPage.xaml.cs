using Xamarin.Forms;
using System.ComponentModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public static IUser user = new User(); //A public static user which holds the users info once they've logged in, for use throughout the app

        public MainPage()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);
        }
    }
}