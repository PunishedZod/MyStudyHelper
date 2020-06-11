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

        //On page dissapearing, do the following code below
        protected override void OnDisappearing()
        {
            if (container != null) container.Dispose(); //Disposes of container (Used for managing resources and memory)
            base.OnDisappearing();
        }

        //When button is clicked, call the BeginLogin method
        private void btnSignin_Clicked(object sender, EventArgs e)
        {
            BeginLogin();
        }

        //When button is clicked, navigate to RegisterPage
        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        //Begins the login process, uses dependency injection via lifetimescope to access methods from the backend
        private async void BeginLogin()
        {
            try //Try the following code below
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ILoginBackend>();
                    string validation = app.CheckInfo(txtUsername.Text, txtPassword.Text); //Uname and pword sent to backend for validation

                    if (validation == null)
                    {
                        var user = await app.Login(txtUsername.Text, txtPassword.Text); //Uname and pword sent to backend for API call to get the user out of the db

                        if (user != null)
                        {
                            if (swtchRememberLogin.IsToggled == true) //If "Remember Me" button is toggled, store uname and pword for auto login
                            {
                                await Storage.LocalStorage.WriteTextFileAsync("username.txt", user.Uname);
                                await Storage.LocalStorage.WriteTextFileAsync("password.txt", user.Pword);
                            }

                            btnSignin.IsEnabled = false;
                            actIndicator.IsRunning = true;

                            App.user = user; //Stores the user info into a public static IUser to use "globally" throughout the app

                            //These three lines of code are for pushing a new instance of a page ontop of the nav stack then removing the previous page in the stack
                            var previousPage = Navigation.NavigationStack.LastOrDefault();
                            await Navigation.PushAsync(new MainPage()); //Navigates to the MainPage
                            Navigation.RemovePage(previousPage);

                            actIndicator.IsRunning = false;
                        }
                        else await DisplayAlert("Login Details Incorrect", "Login details are incorrect, please try again", "Ok"); //Displays alert for incorrect login info
                    }
                    else await DisplayAlert("Invalid or Empty Field(s)", $"{validation}", "Ok");
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to login", "Ok"); } //If an exception is thrown, catch it and display an alert (Try Catches prevent the app from crashing when an exception is thrown)
        }
    }
}