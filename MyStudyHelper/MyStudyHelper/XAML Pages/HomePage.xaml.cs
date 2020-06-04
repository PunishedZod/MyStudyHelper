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
            DisplayList(); //Populates the listview with popular posts when page is initialized
        }

        //Navigates to the account page on button click by pushing a new instance of account page ontop of the navigation stack
        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        //When selecting an item within the listview it will put the data selected into a model than navigate to the viewpost page with the data for viewing
        private async void lstPopularPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var itemSelected = (Posts)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new ViewPostPage(itemSelected));
        }

        //Repopulates the listview after pulling to refresh, then stops the refresher
        private void lstPopularPosts_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstPopularPosts.IsRefreshing = false;
        }

        //Lifetime scope (dependency injection) is created to get popular posts via backend class methods
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