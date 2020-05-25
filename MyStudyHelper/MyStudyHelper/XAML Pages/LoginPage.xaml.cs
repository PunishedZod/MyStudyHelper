using Autofac;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private IContainer container;

        public LoginPage()
        {
            InitializeComponent();
            lblError.IsVisible = false;
        }

        private void btnSignin_Clicked(object sender, EventArgs e)
        {
            BeginLogin();
        }

        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        public async void BeginLogin()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ILoginBackend>();
                    string validation = app.CheckInfo(txtUsername.Text, txtPassword.Text);
                    if (validation == null)
                    {
                        var user = await app.Login(txtUsername.Text, txtPassword.Text); //SHOULD BE WORKING ONCE API HAS BEEN HOSTED
                        if (user != null)
                        {
                            MainPage.user = (User)user;
                            await Navigation.PopModalAsync();
                        }
                        else
                            await DisplayAlert("Invalid or Empty Field(s)", "Login details are incorrect, please try again", "Ok");
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