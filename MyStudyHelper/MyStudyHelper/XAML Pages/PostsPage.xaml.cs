using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
            DisplayList(); //Populates the listview with all posts when page is initialized
        }

        //Navigates to the create a post page on button click by pushing a new instance of create post page ontop of the navigation stack
        private async void btnCreatePost_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePostPage());
        }

        //Navigates to the account page on button click by pushing a new instance of account page ontop of the navigation stack
        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        //When selecting an item within the listview it will put the data selected into a model than navigate to the viewpost page with the data for viewing
        private async void lstAllPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var itemSelected = (Posts)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new ViewPostPage(itemSelected));
        }

        //Repopulates the listview after pulling to refresh, then stops the refresher
        private void lstAllPosts_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstAllPosts.IsRefreshing = false;
        }

        //Lifetime scope (dependency injection) is created to get all posts via backend class methods
        public async void DisplayList()
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IPostsBackend>();
                    lstAllPosts.ItemsSource = app.PostsMod;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to display all posts", "Ok");
            }
        }
    }
}