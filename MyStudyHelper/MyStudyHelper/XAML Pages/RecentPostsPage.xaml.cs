using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;

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

        private async void lstRecentPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var itemSelected = (Posts)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new ViewPostPage(itemSelected));
        }

        private void lstRecentPosts_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstRecentPosts.IsRefreshing = false;
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