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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            lblError.IsVisible = false;
        }

        //More work to be done here, ALOT

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtEmail.Text)
                && !string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPassword1.Text)
                && !string.IsNullOrEmpty(txtPassword2.Text) && txtPassword1.Text == txtPassword2.Text)
            {
                await DisplayAlert("Registration Successful", "You have successfully registered! Please sign in with your account.", "Sign In");
                await Navigation.PushAsync(new LoginPage());
            }     
            else if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPassword1.Text)
                || string.IsNullOrEmpty(txtPassword2.Text) || txtPassword1.Text != txtPassword2.Text)
            {
                lblError.IsVisible = true;
            }
        }
    }
}