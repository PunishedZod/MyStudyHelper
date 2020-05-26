using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.Backend;
using Autofac;

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

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void btnChange_Clicked(object sender, EventArgs e)
        {
            //BeginUpdate();
        }

        private void btnLogout_Clicked(object sender, EventArgs e)
        {
            //Logout function/method to be programmed here, user is logged out and pages are reset to login page (first page)
        }

        //public async void BeginUpdate()
        //{
        //    try
        //    {
        //        container = DependancyInjection.Configure();

        //        using (var scope = container.BeginLifetimeScope())
        //        {
        //            var app = scope.Resolve<AccountBackend>();
        //            var validation = app.CheckInfo(txtUsername.Text, txtEmail.Text, txtName.Text, txtOldPassword.Text, txtNewPassword.Text);

        //            if (validation == null)
        //            {
        //                var user = await app.Update(); //SHOULD BE WORKING ONCE API HAS BEEN HOSTED
        //                if (user != null)
        //                {
        //                    await Navigation.PopModalAsync();
        //                }
        //                else
        //                    await DisplayAlert("Invalid or Empty Field(s)", "Registration unsuccessful, please try again", "Ok");
        //            }
        //            else
        //                await DisplayAlert("Invalid or Empty Field(s)", $"{validation}", "Ok");
        //        }
        //    }
        //    catch
        //    {
        //        await DisplayAlert("Error", "Something went wrong, please try again", "Ok");
        //    }
        //}
    }
}