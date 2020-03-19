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
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnSignin_Clicked(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            //    DisplayAlert("Login", "SUCCESSFUL!", "OK");
            //else if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            //{
            //    DisplayAlert("Login", "FAILED!", "OK");
            //}
        }

        private void btnSignup_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}