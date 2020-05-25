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
using Xamarin.Forms.Xaml;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : CarouselPage
    {
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        readonly IPostsProxy postsProxy = new PostsProxy("https://localhost:44323/");

        public HomePage()
        {
            InitializeComponent();
            BindingContext = this;
            GetPostInfo();
        }

        //Method to get all popular posts and display them in a list
        public async void GetPostInfo()
        {
            PostsMod = new ObservableCollection<IPosts>();
            var temp = await postsProxy.GetPopularPosts();

            if (temp != null)
            {
                if (temp.Count > 0)
                {
                    foreach (var item in temp)
                    {
                        PostsMod.Add(item);
                    }
                }
                else
                    PostsMod.Add(temp[0]);
            }
            lstPopularPosts.ItemsSource = PostsMod;
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }
    }
}