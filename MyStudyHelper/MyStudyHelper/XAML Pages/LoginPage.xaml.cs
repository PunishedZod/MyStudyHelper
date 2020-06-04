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
                    Navigation.RemovePage(page);
            }
            base.OnAppearing();
        }

        //Calls the method which begins validating and getting user information for login on button click
        private void btnSignin_Clicked(object sender, EventArgs e)
        {
            BeginLogin();
        }

        //Navigates to the register page on button click by pushing a new instance of register page ontop of the navigation stack
        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        //Method uses dependency injection which enables the use of methods from the backend class via lifetimescope
        public async void BeginLogin()
        {
            try //try catch to catch an unexpected error which may crash the app, try catches are used throughout the app to prevent any unexpected crashes from happening
            {
                actIndicator.IsRunning = true;
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ILoginBackend>();
                    string validation = app.CheckInfo(txtUsername.Text, txtPassword.Text); //Parameters take in uname and pword and validates it in the backend, if info is valid return null

                    if (validation == null)
                    {
                        var user = await app.Login(txtUsername.Text, txtPassword.Text); //Parameters take in uname and pword and check if user exists in the db, if they do, return user, if not return null

                        if (user != null)
                        {
                            MainPage.user = user; //Stores user into a static user for use throughout the app
                            var previousPage = Navigation.NavigationStack.LastOrDefault();
                            await Navigation.PushAsync(new MainPage()); //Pushes a new instance of main page ontop of the navigation stack after logging in with valid user info
                            Navigation.RemovePage(previousPage);
                            actIndicator.IsRunning = false;
                        }
                        else //If user does not exist in the db, returns null, displays error message
                        {
                            actIndicator.IsRunning = false;
                            await DisplayAlert("Invalid or Empty Field(s)", "Login details are incorrect, please try again", "Ok");
                        }
                    }
                    else //If details are incorrect, display an error message
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