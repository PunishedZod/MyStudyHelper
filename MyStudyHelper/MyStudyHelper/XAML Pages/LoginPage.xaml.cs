using System;
using Autofac;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private IContainer container;

        public LoginPage()
        {
            InitializeComponent();
        }

        //Helps with navigation, removes the previous page from the navigation stack
        protected override void OnAppearing()
        {
            if (Navigation.NavigationStack.Count > 1)
            {
                Page page = Navigation.NavigationStack.First();
                if (page != null && page != this)
                {
                    Navigation.RemovePage(page);
                }
            }
            base.OnAppearing();
        }

        private void btnSignin_Clicked(object sender, EventArgs e)
        {
            BeginLogin();
        }

        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        //Method uses dependency injection which enables the use of methods from the backend class
        public async void BeginLogin()
        {
            try //try catch to catch an unexpected error which may crash the app
            {
                actIndicator.IsRunning = true; //Begins loading symbol
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ILoginBackend>();
                    string validation = app.CheckInfo(txtUsername.Text, txtPassword.Text); //Takes info entered into a method in the backend which validates if what was entered is valid or invalid
                    if (validation == null) //If info entered was valid return null
                    {
                        var user = await app.Login(txtUsername.Text, txtPassword.Text); //Takes info entered into a method in the backend which sends it to the userproxy to make an API call to get the specified user match the info entered
                        if (user != null) //If user exists in the database, does not return null and carries on through
                        {
                            MainPage.user = user; //Stores the user info from the database into a static variable to use throughout the app once logged in
                            var previousPage = Navigation.NavigationStack.LastOrDefault();
                            await Navigation.PushAsync(new MainPage()); //Pushes a new instance of MainPage into the navigation stack after logging in with valid user info
                            Navigation.RemovePage(previousPage);
                            actIndicator.IsRunning = false; //Ends loading symbol
                        }
                        else //If user does not exist in the database, returns null, displays error message
                        {
                            actIndicator.IsRunning = false;
                            await DisplayAlert("Invalid or Empty Field(s)", "Login details are incorrect, please try again", "Ok");
                        }
                    }
                    else //Else display error message
                    {
                        actIndicator.IsRunning = false;
                        await DisplayAlert("Invalid or Empty Field(s)", $"{validation}", "Ok");
                    }
                }
            }
            catch //Catches exceptions and displays an error message to prevent app from crashing
            {
                actIndicator.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please try again", "Ok");
            }
        }
    }
}