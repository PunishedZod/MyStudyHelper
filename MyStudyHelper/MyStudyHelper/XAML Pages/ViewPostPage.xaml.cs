using System;
using Autofac;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;

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
            _postInfo = postInfo;
            Post(postInfo);
            GetComments();

            if (postInfo.UpVote.Contains(MainPage.user.Id))
            {
                btnUpVote.IsEnabled = false;
                btnDownVote.IsEnabled = false;
            }
            if (postInfo.DownVote.Contains(MainPage.user.Id))
            {
                btnDownVote.IsEnabled = false;
                btnUpVote.IsEnabled = false;
            }
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

        private void lstComments_Refreshing(object sender, EventArgs e)
        {
            GetComments();
            lstComments.IsRefreshing = false;
        }

        //Method created to bind the post info to frontend labels for display
        public void Post(Posts post) 
        {
            lblPageTitle.Text = "Post By: " + post.Uname;
            lblTopic.Text = post.Topic;
            lblPostTitle.Text = post.Title;
            lblPostContent.Text = post.Content;
            btnUpVote.Text = "+" + post.UpVote.Count().ToString();
            btnDownVote.Text = "-" + post.DownVote.Count().ToString();
        }

        //Method which begins the updating process of the upvotes then returns the updated result
        public async void UpdateUpVote() 
        {
            var postInfo = _postInfo;
            container = DependancyInjection.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IViewPostBackend>();
                if (postInfo.UpVote.Contains(MainPage.user.Id)) //If upvote array contains the logged in user's id, disable the button
                {
                    btnUpVote.IsEnabled = false;
                }
                else if (!postInfo.UpVote.Contains(MainPage.user.Id)) //else if upvote array doesn't contain the logged in user's id, send the user's id through to the upvote array then refresh page
                {
                    var updatedPost = await app.UpdateUpVote(postInfo);
                    var post = (Posts)updatedPost;
                    var previousPage = Navigation.NavigationStack.LastOrDefault();
                    await Navigation.PushAsync(new ViewPostPage(post));
                    Navigation.RemovePage(previousPage);
                }
            }
        }

        //Method which begins the updating process of the downvotes then returns the updated result
        public async void UpdateDownVote() 
        {
            var postInfo = _postInfo;
            container = DependancyInjection.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IViewPostBackend>();
                if (postInfo.DownVote.Contains(MainPage.user.Id))
                {
                    btnUpVote.IsEnabled = false;
                }
                else if (!postInfo.DownVote.Contains(MainPage.user.Id))
                {
                    var updatedPost = await app.UpdateDownVote(postInfo);
                    var post = (Posts)updatedPost;
                    var previousPage = Navigation.NavigationStack.LastOrDefault();
                    await Navigation.PushAsync(new ViewPostPage(post));
                    Navigation.RemovePage(previousPage);
                }
            }
        }

        //Method to get the comments for the post via the post id
        public void GetComments() 
        {
            var postInfo = _postInfo;
            container = DependancyInjection.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IViewPostBackend>();
                app.GetCommentsInfo(postInfo.Id);
                lstComments.ItemsSource = app.CommentsList; //Sets the commentslist as the listview's (frontend display) itemsource

                if (app.CommentsList.Count == 0)
                    lblCommentCounter.Text = "No comments have been posted";
                else
                    lblCommentCounter.Text = app.CommentsList.Count.ToString() +" Comment(s) have been posted";
            }
        }

        //Method which begins the process of sending the comment to the database through the API, (NEEDS WORK TO UPDATE THE LISTVIEW AT THE END !!!)
        public async void CreateComment() 
        {
            var postInfo = _postInfo;
            container = DependancyInjection.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IViewPostBackend>();
                await app.SendComment(txtComment.Text, postInfo.Id);

                var previousPage = Navigation.NavigationStack.LastOrDefault();
                await Navigation.PushAsync(new ViewPostPage(postInfo));
                Navigation.RemovePage(previousPage);
            }
        }
    }
}