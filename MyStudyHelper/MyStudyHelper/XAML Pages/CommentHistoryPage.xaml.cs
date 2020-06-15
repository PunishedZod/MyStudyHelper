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
    public partial class CommentHistoryPage : ContentPage
    {
        private IContainer container;
        private List<IComments> Comments = new List<IComments>();
        private readonly ObservableCollection<IComments> CommentsMod = new ObservableCollection<IComments>();

        //minItems and maxItems ints used for loading items on scrolling
        private int minItems;
        private int maxItems;

        public CommentHistoryPage()
        {
            InitializeComponent();
        }

        //On page appearing, do the following code below
        protected override void OnAppearing()
        {
            //sets the necessary variables and resets and clears the necessary variables and lists/collections
            minItems = 0;
            maxItems = 10;
            Comments.Clear();
            CommentsMod.Clear();

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

        //When item in ListView is selected, call the GetPost method
        private void lstCommentHistory_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //These three lines of code are for unselecting an item after no longer needing it
            if (e.SelectedItem == null) return;
            var itemSelected = (Comments)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;

            GetPost(itemSelected); //Calls the GetPost method which takes in the comment, API call made to get the post via commentId
        }

        //When ListView is refreshing, call the DisplayList method
        private void lstCommentHistory_Refreshing(object sender, EventArgs e)
        {
            minItems = 0;
            maxItems = 10;
            CommentsMod.Clear();
            Comments.Clear();

            DisplayList();
            lstCommentHistory.IsRefreshing = false;
        }

        //Called when scrolling down the ListView and an item appears, it'll add to the observable collection in increments of 10
        //This method is useful because it means not all data loads at once, it gets it in increments of 10, useful if alot of items are stored
        private void lstCommentHistory_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (actIndicator.IsRunning || CommentsMod.Count == 0) return;

            if ((IComments)e.Item == CommentsMod[CommentsMod.Count - 1])
            {
                actIndicator.IsRunning = true;

                //min and max increased, for example, the ObservableCollection could only hold 10, now it can hold 20
                minItems += 10;
                maxItems += 10;

                for (int i = minItems; i < maxItems && i < Comments.Count; i++) //min and max increased, can add an extra +10 items now
                {
                    CommentsMod.Add(Comments[i]);
                }

                actIndicator.IsRunning = false;
            }
        }

        //Gets all comments by user via backend methods and displays them in a ListView
        private async void DisplayList()
        {
            try
            {
                actIndicator.IsRunning = true;

                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ICommentHistoryBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        if (app.CommentsMod.Count != 0)
                        {
                            lblSubTitle.IsVisible = false;

                            Comments = app.CommentsMod; //List is set to collection from the backend

                            if (Comments.Count >= 10) //If Else statements which limit how much items are displayed, if more than 10, limit to 10, if not then put them into collection
                            {
                                for (int i = minItems; i < maxItems; i++) //For loop allows adding items to a limit of 10
                                {
                                    CommentsMod.Add(app.CommentsMod[i]);
                                }
                            }
                            else
                            {
                                foreach (var item in app.CommentsMod) //If less than or equal to 10, it'll just put the items into the collection
                                {
                                    CommentsMod.Add(item);
                                }
                            }

                            lstCommentHistory.ItemsSource = CommentsMod; //ListView itemsource set to collection
                        }
                        else lblSubTitle.IsVisible = true; //If no items, display an "empty" message
                    }
                    else await DisplayAlert("No Internet Access", "Connection to network not found, please try again", "Ok");

                    actIndicator.IsRunning = false;
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to display user post history", "Ok"); }
        }

        //Gets the post associated with the comment selected in the ListView via backend methods, API call is made containing the postId stored within the comment
        private async void GetPost(Comments commentInfo)
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ICommentHistoryBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        var post = await app.GetPost(commentInfo.PostId);
                        await Navigation.PushAsync(new ViewPostPage(post)); //ViewPostPage is pushed onto the stack, takes in the post info
                    }
                    else await DisplayAlert("No Internet Access", "Connection to network not found, please try again", "Ok");
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to retrieve post info", "Ok"); }
        }
    }
}