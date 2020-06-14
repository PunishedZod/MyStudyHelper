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
        public static IUser user = new User(); //User info is stored in this public static IUser upon logging in, allows the use of user info throughout the app

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new XAML_Pages.LoadingPage()); //LoadingPage is the first page to load upon startup of app
        }

        //When up starts up, do the following code below
        protected override async void OnStart()
        {
            //If a uname and pword exist within LocalStorage, store into vars
            var uname = await Storage.LocalStorage.ReadTextFileAsync("username.txt");
            var pword = await Storage.LocalStorage.ReadTextFileAsync("password.txt");

            //If Else statements which determine what actions to take on startup, if uname and pword exist, make an API call and log user in, else navigate to LoginPage
            if (!String.IsNullOrWhiteSpace(uname) && !String.IsNullOrWhiteSpace(pword))
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ILoginBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        user = await app.Login(uname, pword); //Uname and pword sent to backend for API call to get the user out of the db
                        MainPage = new NavigationPage(new MainPage());
                    }
                    else await MainPage.DisplayAlert("No Internet Access", "Connection to network not found, please restart the app", "Ok");
                }
            }
            else MainPage = new NavigationPage(new XAML_Pages.LoginPage());
        }
    }
}