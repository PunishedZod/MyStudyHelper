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
            new PostsModel { Title = "Help! Please!", Topic = "Technology", Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet nulla quis est faucibus euismod a at magna. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Proin mollis urna non malesuada vestibulum. Nulla ex risus, consequat sit amet tempor vel, accumsan non elit." },
        };

        public ObservableCollection<CommentsModel> CommentMod = new ObservableCollection<CommentsModel>
        {
            new CommentsModel { Username = "(P) The Punished", Comment = "Hey, I can help you with any study questions that you have. If you ever need help I'm here." },
            new CommentsModel { Username = "(Z) The Zinc", Comment = "Hello, I can also assist you with any problems or questions related to studies." },
            new CommentsModel { Username = "(A) The Almighty", Comment = "No one can help you, give up and embrace death." },
            new CommentsModel { Username = "(N) The Negligible", Comment = "Yes! I am!" },
            new CommentsModel { Username = "ZodHD", Comment = "Hello, I can't help you right now, sorry. :/" },
            new CommentsModel { Username = "Zincoro", Comment = "Hey, could you give me more details on your problem please? Thanks! :)" },
            new CommentsModel { Username = "SolarGambit52", Comment = "Hey there! I don't exactly know if I can help but i can certainly try my best too! If you ever need help with your studies or any questions related, feel free to hit me up!" },
            new CommentsModel { Username = "PunishedZod", Comment = "Sorry, I really don't think you need help tbh, It seems as if your question is too vague, try to write better..." },
        };

        public ViewPostPage()
        {
            InitializeComponent();
            BindingContext = this;
            CommentsList();
        }

        //Method created to bind the post info to frontend labels for display
        public void Post()
        {
            var PostInfo = PostMod[0];

            lblTopic.Text = PostInfo.Topic;
            lblPostTitle.Text = PostInfo.Title;
            lblPostContent.Text = PostInfo.Content;
        }

        public void CommentsList()
        {
            //If statement is created to count the amount of comments currently made on the post and displays it to the user
            if(CommentMod.Count == 0)
            {
                lblCommentCounter.Text = "No Comments on Post";
            }
            else if(CommentMod.Count != 0)
            {
                lblCommentCounter.Text = CommentMod.Count().ToString() + " Comment(s) Posted";
            }

            lstComments.ItemsSource = CommentMod;
        }
    }
}