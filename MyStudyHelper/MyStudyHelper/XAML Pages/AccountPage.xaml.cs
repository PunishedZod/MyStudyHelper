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
        }

        //On page appearing, do the following code below
        protected override void OnAppearing()
        {
            DisplayUser();
            base.OnAppearing();
        }

        //On page dissapearing, do the following code below
        protected override void OnDisappearing()
        {
            if (container != null) container.Dispose(); //Disposes of container (Used for managing resources and memory)
            base.OnDisappearing();
        }

        //When button is clicked, call the BeginUpdate method
        private void btnChange_Clicked(object sender, EventArgs e)
        {
            BeginUpdate();
        }

        //When button is clicked, call the BeginLogout method
        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            BeginLogout();
        }

        //Populates the Entry fields with users info
        private void DisplayUser()
        {
            //Populates the Entry fields with the info stored in the public static User
            txtUsername.Text = App.user.Uname;
            txtEmail.Text = App.user.Email;
            txtName.Text = App.user.Name;
        }

        //Begins the update process, uses dependency injection via lifetimescope to access methods from the backend
        private async void BeginUpdate()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IAccountBackend>();
                    var validation = app.CheckInfo(txtUsername.Text, txtEmail.Text, txtName.Text, txtOldPassword.Text, txtNewPassword.Text); //New info sent to backend for validation

                    if (validation == null)
                    {
                        var user = await app.Update(txtUsername.Text, txtEmail.Text, txtName.Text, txtNewPassword.Text); //New info sent to backend for API call to update user in db
                        await DisplayAlert("Update Successful", "Successfully updated, please log back in again", "Ok");

                        BeginLogout();
                    }
                    else await DisplayAlert("Invalid or Empty Field(s)", $"{validation}", "Ok");
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to update info", "Ok"); }
        }

        //Begins the logout process, clears any info stored, then navigates to the LoginPage
        private async void BeginLogout()
        {
            //Clears the uname and pword info stored (if any)
            await Storage.LocalStorage.WriteTextFileAsync("username.txt", "");
            await Storage.LocalStorage.WriteTextFileAsync("password.txt", "");

            App.Current.MainPage = new NavigationPage(new XAML_Pages.LoginPage());
        }
    }
}