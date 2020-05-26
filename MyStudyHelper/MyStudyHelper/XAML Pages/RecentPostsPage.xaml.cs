using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecentPostsPage : ContentPage
    {
        public RecentPostsPage()
        {
            InitializeComponent();
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        public void DisplayList() //Method to get all recent posts (descending order) from the backend class
        {
            RecentPostsBackend recentPosts = new RecentPostsBackend();
            recentPosts.GetPostInfo();
            lstRecentPosts.ItemsSource = recentPosts.PostsMod;
        }
    }
}