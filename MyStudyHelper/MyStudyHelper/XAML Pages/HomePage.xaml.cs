using MyStudyHelper.Classes.API.Models;
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
    public partial class HomePage : ContentPage
    {
        public ObservableCollection<PostsModel> PostMod = new ObservableCollection<PostsModel>
        {
            new PostsModel {Title="Post Title"},
            new PostsModel {Content="Test"}
        };

        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
            Test();
        }

        public void Test()
        {
            lstPopularPosts.ItemsSource = PostMod;
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }
    }
}