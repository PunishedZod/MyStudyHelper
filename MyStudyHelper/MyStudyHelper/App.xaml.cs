using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyStudyHelper
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new XAML_Pages.LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
