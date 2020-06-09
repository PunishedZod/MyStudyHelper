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
            TopicList(); //Populates the listview with topics when page is initialized
        }

        //Calls the method below on button click, begins the process of creating a post
        private void btnPost_Clicked(object sender, EventArgs e)
        {
            BeginCreatePost();
        }

        //Creates post and sends it to the database, then returns it back and sends it to the view post page
        private async void BeginCreatePost() 
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ICreatePostBackend>();
                    var validation = app.CheckInfo(txtTopic.SelectedItem.ToString(), txtTitle.Text, txtMessage.Text);

                    if (validation == null)
                    {
                        btnPost.IsEnabled = false;
                        var createdPost = await app.CreatePost(txtTopic.SelectedItem.ToString(), txtTitle.Text, txtMessage.Text);
                        var post = (Posts)createdPost;

                        var previousPage = Navigation.NavigationStack.LastOrDefault();
                        await Navigation.PushAsync(new ViewPostPage(post));
                        Navigation.RemovePage(previousPage);
                    }
                    else
                        await DisplayAlert("Invalid or Empty Field(s)", $"{validation}", "Ok");
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, please try again", "Ok");
            }
        }

        //List of topics to use as itemsource for the topic picker
        private void TopicList()
        {
            var topicList = new List<string>();
            {
                topicList.Add("Choose Topic *"); //Selects this as default topic
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
            txtTopic.SelectedItem = topicList.FirstOrDefault();
        }
    }
}