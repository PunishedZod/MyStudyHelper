using MyStudyHelper.Classes.Backend.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Autofac;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecentPostsPage : ContentPage
    {
        private IContainer container;

        public RecentPostsPage()
        {
            InitializeComponent();
            DisplayList();
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        public void DisplayList() //Method to get all recent posts (descending order) from the backend class
        {
            container = DependancyInjection.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IRecentPostsBackend>();
                lstRecentPosts.ItemsSource = app.PostsMod;
            }
        }
    }
}