using System;
using Autofac;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPostPage : ContentPage
    {
        readonly Posts _postInfo; //A post used to store the post info for things like API calls, (used for things like upvotes and downvotes, etc)
        private IContainer container;

        public ViewPostPage(Posts postInfo) //Takes in a post in the parameters of the constructor to use upon page start up
        {
            InitializeComponent();
            _postInfo = postInfo; //Stores the post info in a post model
            Post(postInfo); //Populates the post in the frontend view
            DisplayComments(); //Populates the listview with all comments related to the post when page is initialized

            if (postInfo.UpVote.Contains(MainPage.user.Id)) //If the upvote array contains the logged in user's id, disable button
            {
                btnUpVote.IsEnabled = false;
                btnDownVote.IsEnabled = false;
            }
            if (postInfo.DownVote.Contains(MainPage.user.Id)) //If the downvote array contains the logged in user's id, disable button
            {
                btnDownVote.IsEnabled = false;
                btnUpVote.IsEnabled = false;
            }
        }

        //Calls the method which begins the upvoting process on button click
        private void btnUpVote_Clicked(object sender, EventArgs e)
        {
            UpdateUpVote();
        }

        //Calls the method which begins the downvoting process on button click
        private void btnDownVote_Clicked(object sender, EventArgs e)
        {
            UpdateDownVote();
        }

        //Calls the method which begins the process of sending a comment to the db on button click
        private void btnSend_Clicked(object sender, EventArgs e)
        {
            SendComment();
        }

        //Repopulates the listview after pulling to refresh, then stops the refresher
        private void lstComments_Refreshing(object sender, EventArgs e)
        {
            DisplayComments();
            lstComments.IsRefreshing = false;
        }

        //Method which binds the post info to frontend labels for display
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
            try
            {
                var postInfo = _postInfo;
                btnUpVote.IsEnabled = false;
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IViewPostBackend>();
                    var updatedPost = await app.UpdateUpVote(postInfo); //Takes in the post info for use in the backend class
                    var post = (Posts)updatedPost; //The updatedpost is converted from an interface of a post model into a post model

                    var previousPage = Navigation.NavigationStack.LastOrDefault();
                    await Navigation.PushAsync(new ViewPostPage(post)); //Re-navigates to the view post page to display the updated info (new upvote and/or downvote count)
                    Navigation.RemovePage(previousPage);
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to upvote", "Ok");
            }
        }

        //Method which begins the updating process of the downvotes then returns the updated result
        public async void UpdateDownVote()
        {
            try
            {
                var postInfo = _postInfo;
                btnDownVote.IsEnabled = false;
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IViewPostBackend>();
                    var updatedPost = await app.UpdateDownVote(postInfo); //Takes in the post info for use in the backend class
                    var post = (Posts)updatedPost; //The updatedpost is converted from an interface of a post model into a post model

                    var previousPage = Navigation.NavigationStack.LastOrDefault();
                    await Navigation.PushAsync(new ViewPostPage(post)); //Re-navigates to the view post page to display the updated info (new upvote and/or downvote count)
                    Navigation.RemovePage(previousPage);
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to downvote", "Ok");
            }
        }

        //Method to get all the comments for the post, retrieves them via post id
        public async void DisplayComments() 
        {
            try
            {
                var postInfo = _postInfo;
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IViewPostBackend>();
                    app.GetCommentsInfo(postInfo.Id); //Gets the comments for the post by taking in the post id and making an API call to the db
                    lstComments.ItemsSource = app.CommentsList; //Populates the listviews (frontend display) itemsource with the comments list from the backend

                    if (app.CommentsList.Count == 0) //If else statements which displays how much comments/replies are on the post to the user
                        lblCommentCounter.Text = "No replies have been posted yet";
                    else
                        lblCommentCounter.Text = app.CommentsList.Count.ToString() + " Reply(s) to post";
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to display comments", "Ok");
            }
        }

        //Method which begins the process of sending the comment to the database through the API
        public async void SendComment() 
        {
            try
            {
                var postInfo = _postInfo;
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IViewPostBackend>();

                    if (!String.IsNullOrWhiteSpace(txtComment.Text)) //If comment box isn't empty, send comment and re-navigate to the view post page
                    {
                        btnSend.IsEnabled = false;
                        await app.SendComment(txtComment.Text, postInfo.Id);

                        var previousPage = Navigation.NavigationStack.LastOrDefault();
                        await Navigation.PushAsync(new ViewPostPage(postInfo));
                        Navigation.RemovePage(previousPage);
                    }
                    else
                        return; //If comment box is empty, do nothing
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to send comment", "Ok");
            }
        }
    }
}