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
        private IContainer container;
        private Posts post; //Post for storing info received through constructor (Used for upvotes, downvotes, etc)

        public ViewPostPage(Posts post) //Takes in a post in the parameters of the constructor to use upon page start up
        {
            InitializeComponent();
            this.post = post; //Stores the post info in a Post
        }

        //On page appearing, do the following code below
        protected override void OnAppearing()
        {
            DisplayList();
            DisableButtons();
            base.OnAppearing();
        }

        //On page dissapearing, do the following code below
        protected override void OnDisappearing()
        {
            if (container != null) container.Dispose(); //Disposes of container (Used for managing resources and memory)
            base.OnDisappearing();
        }

        //When button is clicked, call the BeginComment method
        private void btnSend_Clicked(object sender, EventArgs e)
        {
            BeginComment();
        }

        //When ListView is refreshing, call the DisplayList method
        private void lstComments_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstComments.IsRefreshing = false;
        }

        //Disables the UpVote and DownVote buttons if user has already upvoted or downvoted the post
        private void DisableButtons()
        {
            //If statements which check if UpVote and/or DownVote array contain the users id, if one of the does, disable the buttons
            if (post.UpVote.Contains(App.user.Id))
            {
                btnUpVote.IsEnabled = false;
                btnDownVote.IsEnabled = false;
            }

            if (post.DownVote.Contains(App.user.Id))
            {
                btnDownVote.IsEnabled = false;
                btnUpVote.IsEnabled = false;
            }
        }

        //Gets post and the comments for this specific post via backend methods and displays them in a ListView
        private async void DisplayList()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IViewPostBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        post = await app.GetPost(post.Id); //API call made from backend to get the post from the db via postId

                        //Binds the post info to frontend labels for display
                        lblPageTitle.Text = "Post By: " + post.Uname;
                        lblTopic.Text = post.Topic;
                        lblPostTitle.Text = post.Title;
                        lblPostContent.Text = post.Content;
                        btnUpVote.Text = "+" + post.UpVote.Count().ToString();
                        btnDownVote.Text = "-" + post.DownVote.Count().ToString();

                        app.GetCommentsInfo(post.Id); //Gets the comments for the post by taking in the post id and making API call to the db
                        lstComments.ItemsSource = app.CommentsList; //Populates ListView itemsource with backend collection

                        //If Else statements which display the amount of comments on the post
                        if (app.CommentsList.Count == 0) lblCommentCounter.Text = "No Comments Have Been Posted Yet";
                        else if (app.CommentsList.Count == 1) lblCommentCounter.Text = app.CommentsList.Count.ToString() + " Comment on Post";
                        else lblCommentCounter.Text = app.CommentsList.Count.ToString() + " Comments on Post";
                    }
                    else await DisplayAlert("No Internet Access", "Connection to network not found, please try again", "Ok");
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to display comments", "Ok"); }
        }

        //Event handler method which updates the post upon clicking either UpVote or DownVote button via backend methods, returns the updated post
        private async void UpdatePost(object sender, EventArgs e)
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IViewPostBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        var btn = (Button)sender; //Stores the button info into the var

                        //If Else statements which determine what code to do based on which button was pressed
                        if (btn == btnUpVote)
                        {
                            btnUpVote.IsEnabled = false;

                            var updatedPost = await app.UpVote(post); //Post is sent to backend, API call made to update post in db, returns the updated post

                            //These three lines of code are for pushing a new instance of a page ontop of the nav stack then removing the previous page in the stack
                            var previousPage = Navigation.NavigationStack.LastOrDefault();
                            await Navigation.PushAsync(new ViewPostPage((Posts)updatedPost)); //Re-navigates to ViewPostPage with the updatedPost (Converted from an IPost to a Post)
                            Navigation.RemovePage(previousPage);
                        }
                        else if (btn == btnDownVote)
                        {
                            btnDownVote.IsEnabled = false;

                            var updatedPost = await app.DownVote(post); //Post is sent to the backend, API call made to update post in db, returns the updated post

                            //These three lines of code are for pushing a new instance of a page ontop of the nav stack then removing the previous page in the stack
                            var previousPage = Navigation.NavigationStack.LastOrDefault();
                            await Navigation.PushAsync(new ViewPostPage((Posts)updatedPost)); //Re-navigates to ViewPostPage with the updatedPost (Converted from an IPost to a Post)
                            Navigation.RemovePage(previousPage);
                        }
                    }
                    else await DisplayAlert("No Internet Access", "Connection to network not found, please try again", "Ok");
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to upvote or downvote", "Ok"); }
        }

        //Begins the process of sending a comment, API call is made to post the comment to the db
        private async void BeginComment() 
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IViewPostBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        if (!String.IsNullOrWhiteSpace(txtComment.Text))
                        {
                            btnSend.IsEnabled = false;

                            await app.PostComment(txtComment.Text, post.Id); //Comment is sent to the backend (contains postId to get post when needed), API call made to post comment in db
                            lstComments.BeginRefresh(); //Refreshes ListView when new comment has been sent and added

                            txtComment.Text = "";
                            btnSend.IsEnabled = true;
                        }
                        else return;
                    }
                    else await DisplayAlert("No Internet Access", "Connection to network not found, please try again", "Ok");
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to send comment", "Ok"); }
        }
    }
}