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
    public partial class AccountPage : ContentPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        //Method to be programmed here to be validate the changes in info made and compare the current and new info

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void btnChange_Clicked(object sender, EventArgs e)
        {
            //Update user details function to be programmed here, user details are changed and sent to the database for updating
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            //Logout function/method to be programmed here, user is logged out and pages are reset to login page (first page)
        }
    }
}