using System;
using Autofac;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private IContainer container;

        public RegisterPage()
        {
            InitializeComponent();
        }

        //On page dissapearing, do the following code below
        protected override void OnDisappearing()
        {
            if (container != null) container.Dispose(); //Disposes of container (Used for managing resources and memory)
            base.OnDisappearing();
        }

        //When button is clicked, call the BeginRegistration method
        private void btnSignup_Clicked(object sender, EventArgs e)
        {
            BeginRegistration();
        }

        //Begins the registration process, uses dependency injection via lifetimescope to access methods from the backend
        private async void BeginRegistration()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IRegisterBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        var validation = app.CheckInfo(txtUsername.Text, txtEmail.Text, txtName.Text, txtPassword1.Text, txtPassword2.Text); //Info sent to backend for validation

                        if (validation == null)
                        {
                            btnSignup.IsEnabled = false;

                            await app.Register(txtUsername.Text, txtEmail.Text, txtName.Text, txtPassword2.Text); //Info is sent to backend for API call to register user in the db
                            await DisplayAlert("Registration Successful", "Account successfully registered, please login", "Ok");

                            //These three lines of code are for pushing a new instance of a page ontop of the nav stack then removing the previous page in the stack
                            var previousPage = Navigation.NavigationStack.LastOrDefault();
                            await Navigation.PushAsync(new LoginPage()); //Navigates back to the LoginPage
                            Navigation.RemovePage(previousPage);
                        }
                        else await DisplayAlert("Invalid or Empty Field(s)", $"{validation}", "Ok");
                    }
                    else await DisplayAlert("No Internet Access", "Connection to network not found, please try again", "Ok");
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to register", "Ok"); }
        }
    }
}