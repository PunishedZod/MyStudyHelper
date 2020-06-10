﻿using System;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecentPostsPage : ContentPage
    {
        private IContainer container;

        public RecentPostsPage()
        {
            InitializeComponent();
            DisplayList();
        }

        //On page appearing, do the following code below
        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<Object>(this, "click_first_tab", (obj) => DisplayList()); //When page is clicked, call DisplayList (Allows refreshing of data)
            base.OnAppearing();
        }

        //On page dissapearing, do the following code below
        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<Object>(this, "click_first_tab");
            base.OnDisappearing();
        }

        //When button is clicked, navigate to AccountPage
        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }

        //When item in ListView is selected, navigate to ViewPostPage
        private async void lstRecentPosts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //These three lines of code are for unselecting an item after no longer needing it
            if (e.SelectedItem == null) return;
            var itemSelected = (Posts)e.SelectedItem;
            ((ListView)sender).SelectedItem = null;

            await Navigation.PushAsync(new ViewPostPage(itemSelected)); //ViewPostPage is pushed onto the stack, takes in the post info
        }

        //When ListView is refreshing, call the DisplayList method
        private void lstRecentPosts_Refreshing(object sender, EventArgs e)
        {
            DisplayList();
            lstRecentPosts.IsRefreshing = false;
        }

        //Gets recent posts via backend methods and displays them in a ListView
        //it is setup in a way so that if there is no changes, it won't bother with an API call
        private async void DisplayList()
        {
            try
            {
                container = DependancyInjection.Configure();
                using (var scope = container.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IRecentPostsBackend>();

                    if (lstRecentPosts.ItemsSource != null)
                    {
                        var temp = lstRecentPosts.ItemsSource as IList; //Converts ListView into a list

                        if (temp.Count != app.PostsMod.Count) //Compares the count of the list and collection, if not equal, set itemsource to collection 
                        {
                            lstRecentPosts.ItemsSource = app.PostsMod; //ListView itemsource set to collection from backend
                        }
                        else return;
                    }
                    else lstRecentPosts.ItemsSource = app.PostsMod;
                }
            }
            catch
            {
                await DisplayAlert("Error", "Something went wrong, unable to display recent posts", "Ok");
            }
        }
    }
}