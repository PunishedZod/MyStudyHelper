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
        private readonly Posts post; //A post used to store the post info for things like API calls, (used for things like upvotes and downvotes, etc)

        public ViewPostPage(Posts post) //Takes in a post in the parameters of the constructor to use upon page start up
        {
            InitializeComponent();
            this.post = post; //Stores the post info in a post model
            DisplayPost(); //Populates the post in the frontend view
            DisplayComments(); //Populates the listview with all comments related to the post when page is initialized
            DisableButtons();
        }

        //Calls the method which begins the upvoting process on button click
        private void btnUpVote_Clicked(object sender, EventArgs e)
        {
            BeginUpVote();
        }

        //Calls the method which begins the downvoting process on button click
        private void btnDownVote_Clicked(object sender, EventArgs e)
        {
            BeginDownVote();
        }

        //Calls the method which begins the process of sending a comment to the db on button click
        private void btnSend_Clicked(object sender, EventArgs e)
        {
            BeginComment();
        }

        //Repopulates the listview after pulling to refresh, then stops the refresher
        private void lstComments_Refreshing(object sender, EventArgs e)
        {
            DisplayComments();
            lstComments.IsRefreshing = false;
        }

        //Method which binds the post info to frontend labels for display
        private void DisplayPost() 
        {
            lblPageTitle.Text = "Post By: " + post.Uname;
            lblTopic.Text = post.Topic;
            lblPostTitle.Text = post.Title;
            lblPostContent.Text = post.Content;
            btnUpVote.Text = "+" + post.UpVote.Count().ToString();
            btnDownVote.Text = "-" + post.DownVote.Count().ToString();
        }

        //Method to get all the comments for the post, retrieves them via post id
        private async void DisplayComments()
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IViewPostBackend>();
                    app.GetCommentsInfo(post.Id); //Gets the comments for the post by taking in the post id and making an API call to the db
                    lstComments.ItemsSource = app.CommentsList; //Populates the listviews (frontend display) itemsource with the comments list from the backend

                    if (app.CommentsList.Count == 0) lblCommentCounter.Text = "No Comments Have Been Posted Yet"; //If else statements which displays how much comments/replies are on the post to the user
                    else if (app.CommentsList.Count == 1) lblCommentCounter.Text = app.CommentsList.Count.ToString() + " Comment on Post";
                    else lblCommentCounter.Text = app.CommentsList.Count.ToString() + " Comments on Post";
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to display comments", "Ok");
            }
        }

        private void DisableButtons()
        {
            if (post.UpVote.Contains(App.user.Id)) //If the upvote array contains the logged in user's id, disable button
            {
                btnUpVote.IsEnabled = false;
                btnDownVote.IsEnabled = false;
            }
            if (post.DownVote.Contains(App.user.Id)) //If the downvote array contains the logged in user's id, disable button
            {
                btnDownVote.IsEnabled = false;
                btnUpVote.IsEnabled = false;
            }
        }

        //Method which begins the updating process of the upvotes then returns the updated result
        private async void BeginUpVote()
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    btnUpVote.IsEnabled = false;
                    var app = scope.Resolve<IViewPostBackend>();
                    var updatedPost = await app.PostUpVote(post); //Takes in the post info for use in the backend class

                    var previousPage = Navigation.NavigationStack.LastOrDefault();
                    await Navigation.PushAsync(new ViewPostPage((Posts)updatedPost)); //Converts the updated post from an interface to a post then re-navigates to the view post page to display the updated info (new upvote and/or downvote count)
                    Navigation.RemovePage(previousPage);
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to upvote", "Ok");
            }
        }

        //Method which begins the updating process of the downvotes then returns the updated result
        private async void BeginDownVote()
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    btnDownVote.IsEnabled = false;
                    var app = scope.Resolve<IViewPostBackend>();
                    var updatedPost = await app.PostDownVote(post); //Takes in the post info for use in the backend class

                    var previousPage = Navigation.NavigationStack.LastOrDefault();
                    await Navigation.PushAsync(new ViewPostPage((Posts)updatedPost)); //Re-navigates to the view post page to display the updated info (new upvote and/or downvote count)
                    Navigation.RemovePage(previousPage);
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to downvote", "Ok");
            }
        }

        //Method which begins the process of sending the comment to the database through the API
        private async void BeginComment() 
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IViewPostBackend>();
                    if (!String.IsNullOrWhiteSpace(txtComment.Text)) //If comment box isn't empty, send comment and re-navigate to the view post page
                    {
                        btnSend.IsEnabled = false;
                        await app.PostComment(txtComment.Text, post.Id);

                        var previousPage = Navigation.NavigationStack.LastOrDefault();
                        await Navigation.PushAsync(new ViewPostPage(post));
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