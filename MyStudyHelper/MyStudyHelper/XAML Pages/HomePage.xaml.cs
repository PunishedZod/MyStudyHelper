using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;
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
            MessagingCenter.Subscribe<Object>(this, "click_first_tab", (obj) =>
            {
                DisplayList(); //Populates the listview with popular posts when page is initialized
            });
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<Object>(this, "click_first_tab");
            base.OnDisappearing();
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
        private async void DisplayList()
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IHomeBackend>();

                    if (lstPopularPosts.ItemsSource != null)
                    {
                        var temp = lstPopularPosts.ItemsSource as IList;

                        if (temp.Count != app.PostsMod.Count)
                        {
                            lstPopularPosts.ItemsSource = app.PostsMod;
                        }
                        else return;
                    }
                    else
                        lstPopularPosts.ItemsSource = app.PostsMod;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to display popular posts", "Ok");
            }
        }
    }
}