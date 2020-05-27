using Autofac;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.Backend;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPostPage : ContentPage
    {
        readonly Posts _postInfo;
        private IContainer container;
        public ObservableCollection<IUser> Test = new ObservableCollection<IUser>();

        public ViewPostPage(Posts postInfo)
        {
            InitializeComponent();
            BindingContext = this;
            Post(postInfo);
            GetComments();
            _postInfo = postInfo;
        }

        private void btnUpVote_Clicked(object sender, EventArgs e)
        {
            UpdateUpVote();
        }

        private void btnDownVote_Clicked(object sender, EventArgs e)
        {
            UpdateDownVote();
        }

        private void btnSend_Clicked(object sender, EventArgs e)
        {
            CreateComment();
        }

        public void Post(IPosts post) //Method created to bind the post info to frontend labels for display
        {
            lblPageTitle.Text = "Post By: " + post.Uname;
            lblTopic.Text = post.Topic;
            lblPostTitle.Text = post.Title;
            lblPostContent.Text = post.Content;
            btnUpVote.Text = post.UpVoteId.Count().ToString();
            btnDownVote.Text = post.DownVoteId.Count().ToString();
        }

        public async void UpdateUpVote() //Method which begins the updating process of the upvotes then returns the updated result
        {
            var postInfo = _postInfo;
            container = DependancyInjection.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IViewPostBackend>();
                if (postInfo.UpVoteId.Contains(MainPage.user.Id)) //If upvote array contains the logged in user's id, disable the button
                {
                    btnUpVote.IsEnabled = false;
                }
                else if (!postInfo.UpVoteId.Contains(MainPage.user.Id)) //else if upvote array doesn't contain the logged in user's id, send the user's id through to the upvote array then refresh page
                {
                    var updatedPost = await app.UpdateUpVote(postInfo);
                    Posts post = (Posts)updatedPost;
                    await Navigation.PopModalAsync();
                    await Navigation.PushAsync(new ViewPostPage(post));
                }
            }
        }

        public async void UpdateDownVote() //Method which begins the updating process of the downvotes then returns the updated result
        {
            var postInfo = _postInfo;
            container = DependancyInjection.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IViewPostBackend>();
                if (postInfo.DownVoteId.Contains(MainPage.user.Id))
                {
                    btnUpVote.IsEnabled = false;
                }
                else if (!postInfo.DownVoteId.Contains(MainPage.user.Id))
                {
                    var updatedPost = await app.UpdateDownVote(postInfo);
                    Posts post = (Posts)updatedPost;
                    await Navigation.PopModalAsync();
                    await Navigation.PushAsync(new ViewPostPage(post));
                }
            }
        }

        public void GetComments() //Method to get the comments for the post via the post id
        {
            var postInfo = _postInfo;
            container = DependancyInjection.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IViewPostBackend>();
                app.GetCommentsInfo(postInfo.Id);
                lstComments.ItemsSource = app.CommentsList; //Sets the commentslist as the listview's (frontend display) itemsource
            }
        }

        public void CreateComment() //Method which begins the process of sending the comment to the database through the API, (NEEDS WORK TO UPDATE THE LISTVIEW AT THE END !!!)
        {
            var postInfo = _postInfo;
            container = DependancyInjection.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IViewPostBackend>();
                app.SendComment(txtComment.Text, postInfo.Id);
            }
        }
    }
}