using System;
using Autofac;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private IContainer container;

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private void btnSignup_Clicked(object sender, EventArgs e)
        {
            BeginRegistration();
        }

        public async void BeginRegistration()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IRegisterBackend>();
                    var validation = app.CheckInfo(txtUsername.Text, txtEmail.Text, txtName.Text, txtPassword1.Text, txtPassword2.Text);

                    if (validation == null)
                    {
                        var user = await app.Register(txtUsername.Text, txtEmail.Text, txtName.Text, txtPassword2.Text);
                        if (user != null)
                        {
                            var previousPage = Navigation.NavigationStack.LastOrDefault();
                            await Navigation.PushAsync(new LoginPage()); //Pushes a new instance of LoginPage into the navigation stack after registering with valid user info
                            Navigation.RemovePage(previousPage);
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