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
    public partial class ViewPostPage : ContentPage
    {
        public ObservableCollection<PostsModel> PostMod = new ObservableCollection<PostsModel>
        {
            new PostsModel { Title = "Help! Please!", Topic = "Technology", Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                " Curabitur sit amet nulla quis est faucibus euismod a at magna." +
                " Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos." +
                " Proin mollis urna non malesuada vestibulum. Nulla ex risus, consequat sit amet tempor vel, accumsan non elit."},
        };

        public ObservableCollection<CommentsModel> CommentMod = new ObservableCollection<CommentsModel>
        {
            new CommentsModel { Username = "(P) The Punished", Comment = "Hey, I can help you with any study questions that you have. If you ever need help I'm here."},
            new CommentsModel { Username = "(Z) The Zinc", Comment = "Hello, I can also assist you with any problems or questions related to studies."},
            new CommentsModel { Username = "(A) The Almighty", Comment = "No one can help you, give up and embrace death."},
            new CommentsModel { Username = "(N) The Negligible", Comment = "Yes! I am!"},
        };

        public ViewPostPage()
        {
            InitializeComponent();
            BindingContext = this;
            Test();
        }

        public void Test()
        {
            if(CommentMod.Count == 0)
            {
                lblCommentCounter.Text = "No Comments on Post";
            }
            else if(CommentMod.Count != 0)
            {
                lblCommentCounter.Text = CommentMod.Count().ToString() + " Comment(s) Posted";
            }

            var test = PostMod[0];

            lblTopic.Text = test.Topic;
            lblPostTitle.Text = test.Title;
            lblPostContent.Text = test.Content;

            lstComments.ItemsSource = CommentMod;
        }
    }
}