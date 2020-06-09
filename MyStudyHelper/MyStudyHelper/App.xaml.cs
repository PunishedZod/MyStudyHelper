using System;
using Autofac;
using Xamarin.Forms;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper
{
    public partial class App : Application
    {
        private IContainer container;
        public static IUser user = new User();

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new XAML_Pages.LoadingPage()); //This page is set as default to ensure navigation to either the login page or the mainpage if user is logged in is possible
        }

        //Checks if user details were remembered for automatic login next time app starts up
        protected override async void OnStart()
        {
            var Uname = await Storage.LocalStorage.ReadTextFileAsync("username.txt"); //Stores uname into local storage for automatic login
            var Pword = await Storage.LocalStorage.ReadTextFileAsync("password.txt"); //Stores pword into local storage for automatic login

            if (String.IsNullOrWhiteSpace(Uname) && String.IsNullOrWhiteSpace(Pword)) //If else statement which decides which page to navigate to depending on if user was logged in or not
            {
                MainPage = new NavigationPage(new XAML_Pages.LoginPage());
            }
            else
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ILoginBackend>();
                    user = await app.Login(Uname, Pword);

                    MainPage = new NavigationPage(new MainPage());
                }
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}