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
    public partial class CommentHistoryPage : ContentPage
    {
        private IContainer container;

        public CommentHistoryPage()
        {
            InitializeComponent();
            DisplayList();
        }

        //On page appearing, do the following code below
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<Object>(this, "click_third_tab", (obj) => DisplayList()); //When page is clicked, call DisplayList (Allows refreshing of data)
            base.OnAppearing();
        }

        //On page dissapearing, do the following code below
        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<Object>(this, "click_third_tab");
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

            GetPost(itemSelected);
        }

        //When ListView is refreshing, call the DisplayList method
        private void lstCommentHistory_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstCommentHistory.IsRefreshing = false;
        }

        //Gets all of the users comments via backend methods and displays them in a ListView
        //it is setup in a way so that if there is no changes, it won't bother with an API call
        private async void DisplayList()
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ICommentHistoryBackend>();

                    if (lstCommentHistory.ItemsSource != null)
                    {
                        var temp = lstCommentHistory.ItemsSource as IList; //Converts ListView into a list

                        if (temp.Count != app.CommentsMod.Count) //Compares the count of the list and collection, if not equal, set itemsource to collection
                        {
                            lstCommentHistory.ItemsSource = app.CommentsMod; //ListView itemsource set to collection from backend
                        }
                        else return;
                    }
                    else lstCommentHistory.ItemsSource = app.CommentsMod;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to display comment history", "Ok");
            }
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
                    var post = await app.GetPost(commentInfo.PostId);

                    await Navigation.PushAsync(new ViewPostPage(post)); //ViewPostPage is pushed onto the stack, takes in the post info
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to retrieve post info", "Ok");
            }
        }
    }
}