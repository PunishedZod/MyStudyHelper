using System;
using Autofac;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePostPage : ContentPage
    {
        private IContainer container;

        public CreatePostPage()
        {
            InitializeComponent();
        }

        //On page appearing, do the following code below
        protected override void OnAppearing()
        {
            DisplayList();
            base.OnAppearing();
        }

        //On page dissapearing, do the following code below
        protected override void OnDisappearing()
        {
            if (container != null) container.Dispose(); //Disposes of container (Used for managing resources and memory)
            base.OnDisappearing();
        }

        //When button is clicked, call the BeginPost method
        private void btnPost_Clicked(object sender, EventArgs e)
        {
            BeginPost();
        }

        //Begins the post creation process, post is created via backend methods
        private async void BeginPost() 
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ICreatePostBackend>();
                    var network = scope.Resolve<IConnectionBackend>();

                    if (network.HasConnection()) //If Else statements which determine if you have internet connection, if you do then continue, if you don't then display an alert
                    {
                        var validation = app.CheckInfo(txtTopic.SelectedItem.ToString(), txtTitle.Text, txtMessage.Text); //Post sent to backend for validation

                        if (validation == null)
                        {
                            btnPost.IsEnabled = false;

                            var createdPost = await app.CreatePost(txtTopic.SelectedItem.ToString(), txtTitle.Text, txtMessage.Text); //Post sent to backend for API call to post in db, returns post
                            var post = (Posts)createdPost; //Converts post from IPost (Interface of a model) to a Post (Post model)

                            var previousPage = Navigation.NavigationStack.LastOrDefault();
                            await Navigation.PushAsync(new ViewPostPage(post)); //ViewPostPage is pushed onto the stack, takes in the post info
                            Navigation.RemovePage(previousPage);
                        }
                        else await DisplayAlert("Invalid or Empty Field(s)", $"{validation}", "Ok");
                    }
                    else await DisplayAlert("No Internet Access", "Connection to network not found, please try again", "Ok");
                }
            }
            catch { await DisplayAlert("Error", "Something went wrong, unable to create post, please try again", "Ok"); }
        }

        //Displays a list of topics, sets the itemsource of Picker to the list of topics
        private void DisplayList()
        {
            var topicList = new List<string>();
            {
                topicList.Add("Choose Topic *");
                topicList.Add("Agriculture");
                topicList.Add("Animal Care");
                topicList.Add("Architecture");
                topicList.Add("Art");
                topicList.Add("Automotive");
                topicList.Add("Beauty Therapy");
                topicList.Add("Beekeeping");
                topicList.Add("Bridging & Foundation Skills");
                topicList.Add("Business Administration & Technology");
                topicList.Add("Business Management");
                topicList.Add("Carpentry & Construction");
                topicList.Add("Computing & IT");
                topicList.Add("Creative");
                topicList.Add("Culinary Arts, Baking & Hospitality");
                topicList.Add("Educational Pathways");
                topicList.Add("Electrotechnology & Electrical");
                topicList.Add("Engineering & Welding");
                topicList.Add("English Language");
                topicList.Add("Environment Management");
                topicList.Add("Fashion");
                topicList.Add("Forestry");
                topicList.Add("Graphic Design");
                topicList.Add("Hairdressing & Barbering");
                topicList.Add("Health Care");
                topicList.Add("Horticulture");
                topicList.Add("Interior Design");
                topicList.Add("Kaupapa Māori");
                topicList.Add("Legal Studies");
                topicList.Add("Marine Studies");
                topicList.Add("Maritime");
                topicList.Add("Massage Therapy");
                topicList.Add("Media Studies");
                topicList.Add("Music & Performance");
                topicList.Add("Nursing");
                topicList.Add("Occupational Health & Safety");
                topicList.Add("Organics");
                topicList.Add("Policing & Defence Forces");
                topicList.Add("Professional Mentoring");
                topicList.Add("Real Estate");
                topicList.Add("Retail");
                topicList.Add("Road Transport, Warehousing & Logistics");
                topicList.Add("Social Services");
                topicList.Add("Sport & Recreation");
                topicList.Add("Surveying");
                topicList.Add("Teaching");
                topicList.Add("Tourism & Travel");
                topicList.Add("Wood Manufacturing");
                topicList.Add("Youth Guarantee");
            }
            
            txtTopic.ItemsSource = topicList;
            txtTopic.SelectedItem = topicList.FirstOrDefault(); //Selects the first element in the list, displays it as default
        }
    }
}