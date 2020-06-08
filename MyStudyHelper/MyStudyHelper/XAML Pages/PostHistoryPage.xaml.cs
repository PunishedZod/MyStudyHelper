using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostHistoryPage : CarouselPage
    {
        private IContainer container;

        public PostHistoryPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<Object>(this, "click_third_tab", (obj) =>
            {
                DisplayList(); //Populates the listview with all the logged in users posts when page is initialized
            });
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        private async void lstPostHistory_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var itemSelected = (Posts)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new ViewPostPage(itemSelected));
        }

        private void lstPostHistory_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstPostHistory.IsRefreshing = false;
        }

        public async void DisplayList()
        {
            try
            {
                container = DependancyInjection.Configure();

                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IPostHistoryBackend>();
                    lstPostHistory.ItemsSource = app.PostsMod;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to display post history", "Ok");
            }
        }
    }
}