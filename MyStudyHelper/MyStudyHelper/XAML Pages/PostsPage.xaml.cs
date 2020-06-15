using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostsPage : ContentPage
    {
        private IContainer container;
        private List<IPosts> Posts = new List<IPosts>();
        private readonly ObservableCollection<IPosts> PostsMod = new ObservableCollection<IPosts>();

        //minItems and maxItems ints used for loading items on scrolling
        private int minItems;
        private int maxItems;

        public PostsPage()
        {
            InitializeComponent();
        }

        //On page appearing, do the following code below
        protected override void OnAppearing()
        {
            //sets the necessary variables and resets and clears the necessary variables and lists/collections
            minItems = 0;
            maxItems = 10;
            Posts.Clear();
            PostsMod.Clear();

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

        //When ListView is refreshing, resets the min and max items, clears the list and collection, calls the DisplayList method
        private void lstAllPosts_Refreshing(object sender, EventArgs e)
        {
            minItems = 0;
            maxItems = 10;
            Posts.Clear();
            PostsMod.Clear();

            DisplayList();
            lstAllPosts.IsRefreshing = false;
        }

        //Called when scrolling down the ListView and an item appears, it'll add to the observable collection in increments of 10
        //This method is useful because it means not all data loads at once, it gets it in increments of 10, useful if alot of items are stored
        private void lstAllPosts_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (actIndicator.IsRunning || PostsMod.Count == 0) return;

            if ((IPosts)e.Item == PostsMod[PostsMod.Count - 1])
            {
                actIndicator.IsRunning = true;

                //min and max increased, for example, the ObservableCollection could only hold 10, now it can hold 20
                minItems += 10;
                maxItems += 10;

                for (int i = minItems; i < maxItems && i < Posts.Count; i++) //min and max increased, can add an extra +10 items now
                {
                    PostsMod.Add(Posts[i]);
                }

                actIndicator.IsRunning = false;
            }
        }

        //Gets all posts via backend methods and displays them in a ListView
        private async void DisplayList()
        {
            try
            {
                actIndicator.IsRunning = true;

                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IPostsBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        if (app.PostsMod.Count != 0)
                        {
                            lblSubTitle.IsVisible = false;

                            Posts = app.PostsMod; //List is set to collection from the backend

                            if (Posts.Count >= 10) //If Else statements which limit how much items are displayed, if more than 10, limit to 10, if not then put them into collection
                            {
                                for (int i = minItems; i < maxItems; i++) //For loop allows adding items to a limit of 10
                                {
                                    PostsMod.Add(app.PostsMod[i]);
                                }
                            }
                            else
                            {
                                foreach (var item in app.PostsMod) //If less than or equal to 10, it'll just put the items into the collection
                                {
                                    PostsMod.Add(item);
                                }
                            }

                            lstAllPosts.ItemsSource = PostsMod; //ListView itemsource set to collection
                        }
                        else lblSubTitle.IsVisible = true; //If no items, display an "empty" message
                    }
                    else await DisplayAlert("No Internet Access", "Connection to network not found, please try again", "Ok");

                    actIndicator.IsRunning = false;
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to display all posts", "Ok"); }
        }
    }
}