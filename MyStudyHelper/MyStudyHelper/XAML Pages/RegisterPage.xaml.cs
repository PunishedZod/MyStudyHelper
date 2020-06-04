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

        //Calls the method which begins validating and registering the user to the db
        private void btnSignup_Clicked(object sender, EventArgs e)
        {
            BeginRegistration();
        }

        //Method uses dependency injection which enables the use of methods from the backend class via lifetimescope
        public async void BeginRegistration()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IRegisterBackend>();
                    var validation = app.CheckInfo(txtUsername.Text, txtEmail.Text, txtName.Text, txtPassword1.Text, txtPassword2.Text); //Parameters take in user info and validates it in the backend, if info is valid return null

                    if (validation == null)
                    {
                        btnSignup.IsEnabled = false;
                        await app.Register(txtUsername.Text, txtEmail.Text, txtName.Text, txtPassword2.Text); //Parameters take in the valid user info and inserts it into the db, registering them
                        await DisplayAlert("User Registered", "Account successfully registered, please login via username and password", "Ok");

                        var previousPage = Navigation.NavigationStack.LastOrDefault();
                        await Navigation.PushAsync(new LoginPage()); //Pushes a new instance of login page ontop of the navigation stack after registering with valid user info
                        Navigation.RemovePage(previousPage);
                    }
                    else
                        await DisplayAlert("Invalid or Empty Field(s)", $"{validation}", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, please try again", "Ok");
            }
        }
    }
}