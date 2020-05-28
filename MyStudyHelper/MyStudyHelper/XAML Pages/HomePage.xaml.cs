using MyStudyHelper.Classes.Backend;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : CarouselPage
    {
        public HomePage()
        {
            InitializeComponent();
            DisplayList();
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        //Method to get popular posts from the backend class
        public void DisplayList() 
        {
            HomeBackend home = new HomeBackend();
            home.GetPostInfo();
            lstPopularPosts.ItemsSource = home.PostsMod;
        }
    }
}