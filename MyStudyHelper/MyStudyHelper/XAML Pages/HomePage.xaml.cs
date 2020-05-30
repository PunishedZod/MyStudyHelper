using Autofac;
using MyStudyHelper.Classes.Backend.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : CarouselPage
    {
        private IContainer container;

        public HomePage()
        {
            InitializeComponent();
            DisplayList();
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        //Method to get popular posts from the backend class
        public void DisplayList() 
        {
            container = DependancyInjection.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IHomeBackend>();
                lstPopularPosts.ItemsSource = app.PostsMod;
            }
        }
    }
}