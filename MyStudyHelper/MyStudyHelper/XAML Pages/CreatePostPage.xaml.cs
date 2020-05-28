using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void btnPost_Clicked(object sender, EventArgs e)
        {
            BeginCreatePost();
        }

        //Creates post and sends it to the database, then returns it back and sends it to the view post page
        public async void BeginCreatePost() 
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<ICreatePostBackend>();
                    var validation = app.CheckInfo(txtTopic.ToString(), txtTitle.Text, txtMessage.Text);

                    if (validation == null)
                    {
                        var post = await app.CreatePost(txtTopic.ToString(), txtTitle.Text, txtMessage.Text); //SHOULD BE WORKING ONCE API HAS BEEN HOSTED
                        if (post != null)
                        {
                            await Navigation.PushAsync(new ViewPostPage(post));
                        }
                        else
                            await DisplayAlert("Invalid or Empty Field(s)", "Registration unsuccessful, please try again", "Ok");
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
    }
}