﻿using Autofac;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
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
    public partial class RegisterPage : ContentPage
    {
        private IContainer container;

        public RegisterPage()
        {
            InitializeComponent();
            lblError.IsVisible = false;
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void btnSignup_Clicked(object sender, EventArgs e)
        {

        }

        public async void BeginRegistration()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<RegisterBackend>();
                    var validation = app.CheckInfo(txtUsername.Text, txtEmail.Text, txtName.Text, txtPassword1.Text, txtPassword2.Text);

                    if (validation == null)
                    {
                        var user = await app.Register(); //SHOULD BE WORKING ONCE API HAS BEEN HOSTED
                        if (user != null)
                        {
                            MainPage.user = (User)user;
                            await Navigation.PopModalAsync();
                        }
                        else
                            await DisplayAlert("Invalid or Empty Field(s)", "Registration unsuccessful, please try again", "Ok");
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