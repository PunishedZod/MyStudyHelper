using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private IContainer container;

        public AccountPage()
        {
            InitializeComponent();
            DisplayAccountDetails(); //Populates the text boxes with the logged in user's details
        }

        //Calls the method which begins validating and updating the user in the db
        private void btnChange_Clicked(object sender, EventArgs e)
        {
            BeginUpdate();
        }

        //Logs the user out of app on button click
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            BeginLogout();
        }

        //Populates the text box fields with the logged in user's info
        private void DisplayAccountDetails()
        {
            txtUsername.Text = App.user.Uname;
            txtEmail.Text = App.user.Email;
            txtName.Text = App.user.Name;
        }

        //Method uses dependency injection which enables the use of methods from the backend class via lifetimescope
        private async void BeginUpdate()
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IAccountBackend>();
                    var validation = app.CheckInfo(txtUsername.Text, txtEmail.Text, txtName.Text, txtOldPassword.Text, txtNewPassword.Text); //Parameters take in user info and validates it in the backend, if info is valid return null

                    if (validation == null)
                    {
                        var user = await app.Update(txtUsername.Text, txtEmail.Text, txtName.Text, txtNewPassword.Text); //Parameters take in the valid user info and updates it in the db
                        await DisplayAlert("User Updated", "Account successfully updated", "Ok");

                        txtUsername.Text = user.Uname; //Repopulates the text box fields with the updated user info
                        txtEmail.Text = user.Email;
                        txtName.Text = user.Name;
                        txtOldPassword.Text = "";
                        txtNewPassword.Text = "";

                        App.user = user; //Updates the static user with the updated user info
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

        private async void BeginLogout()
        {
            await Storage.LocalStorage.WriteTextFileAsync("username.txt", ""); //Clears the data stored in local storage
            await Storage.LocalStorage.WriteTextFileAsync("password.txt", "");

            App.Current.MainPage = new NavigationPage(new XAML_Pages.LoginPage()); //Resets the navigation and sets the current page as the login page
        }
    }
}