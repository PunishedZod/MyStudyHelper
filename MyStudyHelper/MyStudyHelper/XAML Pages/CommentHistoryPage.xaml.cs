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
            MessagingCenter.Subscribe<Object>(this, "click_third_tab", (obj) =>
            {
                DisplayList(); //Populates the listview with all the logged in users comments when page is initialized
            });
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<Object>(this, "click_third_tab");
            base.OnDisappearing();
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        private void lstCommentHistory_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var itemSelected = (Comments)e.SelectedItem;
            GetPost(itemSelected); //Calls the method which begins grabbing the post via the comment id stored within the comment model
            ((ListView)sender).SelectedItem = null;
        }

        private void lstCommentHistory_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstCommentHistory.IsRefreshing = false;
        }

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
                        var temp = lstCommentHistory.ItemsSource as IList;

                        if (temp.Count != app.CommentsMod.Count)
                        {
                            lstCommentHistory.ItemsSource = app.CommentsMod;
                        }
                        else return;
                    }
                    else
                        lstCommentHistory.ItemsSource = app.CommentsMod;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to display comment history", "Ok");
            }
        }

        private async void GetPost(Comments commentInfo)
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ICommentHistoryBackend>();
                    var postInfo = await app.GetPost(commentInfo.PostId);
                    await Navigation.PushAsync(new ViewPostPage(postInfo));
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to retrieve post info", "Ok");
            }
        }
    }
}