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
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
            if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword1.Text))
                DisplayAlert("Login", "SUCCESSFUL!", "OK");
            else if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword1.Text))
            {
                DisplayAlert("Login", "FAILED!", "OK");
            }
        }
    }
}