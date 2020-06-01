using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;

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

        private async void lstPopularPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var itemSelected = (Posts)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new ViewPostPage(itemSelected));
        }

        private void lstPopularPosts_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstPopularPosts.IsRefreshing = false;
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