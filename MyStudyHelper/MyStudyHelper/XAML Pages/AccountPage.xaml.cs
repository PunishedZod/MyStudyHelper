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
            AccountDetails();
        }

        private void btnChange_Clicked(object sender, EventArgs e)
        {
            BeginUpdate();
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new XAML_Pages.LoginPage());
        }

        public void AccountDetails()
        {
            txtUsername.Text = MainPage.user.Uname;
            txtEmail.Text = MainPage.user.Email;
            txtName.Text = MainPage.user.Name;
        }

        public async void BeginUpdate()
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IAccountBackend>();
                    var validation = app.CheckInfo(txtUsername.Text, txtEmail.Text, txtName.Text, txtOldPassword.Text, txtNewPassword.Text);

                    if (validation == null)
                    {
                        var user = await app.Update(txtUsername.Text, txtEmail.Text, txtName.Text, txtNewPassword.Text);
                        if (user != null)
                        {
                            await DisplayAlert("User Updated", "Account successfully updated", "Ok");
                            MainPage.user = user;

                            txtUsername.Text = user.Uname;
                            txtEmail.Text = user.Email;
                            txtName.Text = user.Name;
                            txtOldPassword.Text = "";
                            txtNewPassword.Text = "";
                        }
                        else
                            await DisplayAlert("Invalid or Empty Field(s)", "Update unsuccessful, please try again", "Ok");
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