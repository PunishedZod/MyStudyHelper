using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPostPage : ContentPage
    {
        public ObservableCollection<IPosts> PostsMod { get; set; } = new ObservableCollection<IPosts>();
        readonly IPostsProxy postsProxy = new PostsProxy("https://localhost:44323/");

        public ObservableCollection<Comments> CommentsMod = new ObservableCollection<Comments>
        {
            new Comments { Uname = "(P) The Punished", Comment = "Hey, I can help you with any study questions that you have. If you ever need help I'm here." },
            new Comments { Uname = "(Z) The Zinc", Comment = "Hello, I can also assist you with any problems or questions related to studies." },
            new Comments { Uname = "(A) The Almighty", Comment = "No one can help you, give up and embrace death." },
            new Comments { Uname = "(N) The Negligible", Comment = "Yes! I am!" },
            new Comments { Uname = "ZodHD", Comment = "Hello, I can't help you right now, sorry. :/" },
            new Comments { Uname = "Zincoro", Comment = "Hey, could you give me more details on your problem please? Thanks! :)" },
            new Comments { Uname = "SolarGambit52", Comment = "Hey there! I don't exactly know if I can help but i can certainly try my best too! If you ever need help with your studies or any questions related, feel free to hit me up!" },
            new Comments { Uname = "PunishedZod", Comment = "Sorry, I really don't think you need help tbh, It seems as if your question is too vague, try to write better..." },
        };

        public ViewPostPage(IPosts postData)
        {
            InitializeComponent();
            BindingContext = this;
            Post(postData);
            CommentsList();
        }

        //Method created to bind the post info to frontend labels for display
        public void Post(IPosts postData)
        {
            var PostInfo = postData;
            lblPageTitle.Text = "Post By: " + PostInfo.Uname;
            lblTopic.Text = PostInfo.Topic;
            lblPostTitle.Text = PostInfo.Title;
            lblPostContent.Text = PostInfo.Content;
        }

        public void CommentsList()
        {
            //If statement is created to count the amount of comments currently made on the post and displays it to the user
            if(CommentsMod.Count == 0)
            {
                lblCommentCounter.Text = "No Comments on Post";
            }
            else if(CommentsMod.Count != 0)
            {
                lblCommentCounter.Text = CommentsMod.Count().ToString() + " Comment(s) Posted";
            }

            lstComments.ItemsSource = CommentsMod;
        }

        public void ButtonUpdate()
        {
            //Insert upvote and downvote code here
        }

        private void btnUpVote_Clicked(object sender, EventArgs e)
        {
            ButtonUpdate();
        }

        private void btnDownVote_Clicked(object sender, EventArgs e)
        {
            ButtonUpdate();
        }

        private void btnSend_Clicked(object sender, EventArgs e)
        {
            //Send text from Editor into the model
        }






        //public ObservableCollection<Posts> PostMod = new ObservableCollection<Posts>
        //{
        //    new Posts { Title = "Help! Please!", Topic = "Technology", Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur sit amet nulla quis est faucibus euismod a at magna. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Proin mollis urna non malesuada vestibulum. Nulla ex risus, consequat sit amet tempor vel, accumsan non elit." },
        //};
    }
}