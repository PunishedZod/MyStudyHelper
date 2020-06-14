using System;
using Autofac;
using System.Linq;
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
        }

        //On page appearing, do the following code below
        protected override void OnAppearing()
        {
            DisplayList();
            base.OnAppearing();
        }

        //On page dissapearing, do the following code below
        protected override void OnDisappearing()
        {
            if (container != null) container.Dispose(); //Disposes of container (Used for managing resources and memory)
            base.OnDisappearing();
        }

        //When button is clicked, navigate to AccountPage
        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        //When item in ListView is selected, navigate to ViewPostPage
        private async void lstPopularPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //These three lines of code are for unselecting an item after no longer needing it
            if (e.SelectedItem == null) return;
            var itemSelected = (Posts)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;

            await Navigation.PushAsync(new ViewPostPage(itemSelected)); //ViewPostPage is pushed onto the stack, takes in the post info
        }

        //When ListView is refreshing, call the DisplayList method
        private void lstPopularPosts_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstPopularPosts.IsRefreshing = false;
        }

        //Gets popular posts via backend methods and displays them in a ListView
        //it is setup in a way so that if there is no changes, it won't bother with an API call
        private async void DisplayList()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IHomeBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        if (lstPopularPosts.ItemsSource != null)
                        {
                            var temp = lstPopularPosts.ItemsSource as IList; //Converts ListView into a list

                            if (temp.Count != app.PostsMod.Count()) //Compares the count of the list and collection, if not equal, set itemsource to collection
                            {
                                lstPopularPosts.ItemsSource = app.PostsMod; //ListView itemsource set to collection from backend
                            }
                            else return;
                        }
                        else lstPopularPosts.ItemsSource = app.PostsMod;
                    }
                    else await DisplayAlert("No Internet Access", "Connection to network not found, please try again", "Ok");
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to display popular posts", "Ok"); }
        }
    }
}