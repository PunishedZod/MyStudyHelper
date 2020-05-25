using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostsPage : ContentPage
    {
        //public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        //readonly IPostsProxy postsProxy = new PostsProxy("https://localhost:44323/");

        public ObservableCollection<Posts> PostsMod = new ObservableCollection<Posts>
        {
            new Posts { Uname = "ZodHD", Topic = "Technology", Title = "Help! Please!", Content = "Quisque a nisl fermentum, fringilla ligula a, lobortis risus." },
            new Posts { Uname = "Dei", Topic = "Art", Title = "Need Assistance", Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam malesuada erat ac sapien porta aliquet. Nam finibus eros eu nisi consequat, et accumsan dui porttitor." },
            new Posts { Uname = "Fafnir", Topic = "Technology", Title = "A Question About Studies", Content = "Cras nibh arcu, sagittis ac sollicitudin et, porttitor a velit. Suspendisse dignissim eu turpis vel porta. Vivamus tincidunt eleifend augue non egestas. Morbi accumsan nisl ut risus pretium imperdiet. Cras posuere nisl auctor dolor convallis, non condimentum purus pellentesque. Praesent vel viverra urna. Pellentesque ac risus mauris." },
            new Posts { Uname = "Zesty", Topic = "Art", Title = "Studying Inquries?", Content = "Cras nibh arcu, sagittis ac sollicitudin et, porttitor a velit. Suspendisse." },
        };

        public PostsPage()
        {
            InitializeComponent();
            BindingContext = this;
            GetPostInfo();
        }

        //Method to get all posts in descending order and display them in a list
        public void GetPostInfo()
        {
            //PostsMod = new ObservableCollection<IPosts>();
            //var temp = await postsProxy.GetRecentPosts();

            //if (temp != null)
            //{
            //    if (temp.Count > 0)
            //    {
            //        foreach (var item in temp)
            //        {
            //            PostsMod.Add(item);
            //        }
            //    }
            //    else
            //        PostsMod.Add(temp[0]);
            //}
            lstAllPosts.ItemsSource = PostsMod;
        }

        private async void btnCreatePost_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePostPage());
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            //NOTE: TEMPORARY, PLEASE CHANGE BACK TO "ACCOUNT PAGE" NAVIGATION
            await Navigation.PushAsync(new AccountPage());
        }

        private async void lstAllPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var itemSelected = (IPosts)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new ViewPostPage(itemSelected));
        }
    }
}