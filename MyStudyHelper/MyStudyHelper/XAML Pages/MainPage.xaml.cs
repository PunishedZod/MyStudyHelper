using Xamarin.Forms;
using System.ComponentModel;

namespace MyStudyHelper
{
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false); //Disables swiping in the app for TabbedPage
        }

        //Disables physical backbutton on device when on the MainPage
        protected override bool OnBackButtonPressed() { return true; }
    }
}