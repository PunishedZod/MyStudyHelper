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
    public partial class PostsPage : ContentPage
    {
        private IContainer container;

        public PostsPage()
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

        //When button is clicked, navigate to CreatePostPage
        private async void btnCreatePost_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePostPage());
        }

        //When button is clicked, navigate to AccountPage
        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        //When selecting an item within the listview it will put the data selected into a model than navigate to the viewpost page with the data for viewing
        private async void lstAllPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //These three lines of code are for unselecting an item after no longer needing it
            if (e.SelectedItem == null) return;
            var itemSelected = (Posts)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;

            await Navigation.PushAsync(new ViewPostPage(itemSelected)); //ViewPostPage is pushed onto the stack, takes in the post info
        }

        //When ListView is refreshing, call the DisplayList method
        private void lstAllPosts_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstAllPosts.IsRefreshing = false;
        }

        //Gets recent posts via backend methods and displays them in a ListView
        //it is setup in a way so that if there is no changes, it won't bother with an API call
        private async void DisplayList()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IPostsBackend>();

                    if (lstAllPosts.ItemsSource != null)
                    {
                        var temp = lstAllPosts.ItemsSource as IList; //Converts ListView into a list

                        if (temp.Count != app.PostsMod.Count) //Compares the count of the list and collection, if not equal, set itemsource to collection
                        {
                            lstAllPosts.ItemsSource = app.PostsMod; //ListView itemsource set to collection from backend
                        }
                        else return;
                    }
                    else lstAllPosts.ItemsSource = app.PostsMod;
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to display all posts", "Ok"); }
        }
    }
}