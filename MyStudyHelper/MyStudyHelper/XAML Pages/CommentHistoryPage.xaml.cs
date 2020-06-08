using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.Backend;
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

        public async void DisplayList()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ICommentHistoryBackend>();
                    lstCommentHistory.ItemsSource = app.CommentsMod;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to display comment history", "Ok");
            }
        }

        public async void GetPost(Comments commentInfo)
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<CommentHistoryBackend>();
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