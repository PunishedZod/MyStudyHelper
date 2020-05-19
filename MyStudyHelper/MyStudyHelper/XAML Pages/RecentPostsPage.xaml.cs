﻿using MyStudyHelper.Classes.API.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyStudyHelper.XAML_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecentPostsPage : ContentPage
    {
        public ObservableCollection<PostsModel> PostMod = new ObservableCollection<PostsModel>
        {
            new PostsModel {Title="Help! Please!", Content="Quisque a nisl fermentum, fringilla ligula a, lobortis risus."},
            new PostsModel {Title="Need Assistance", Content="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam malesuada erat ac sapien porta aliquet. Nam finibus eros eu nisi consequat, et accumsan dui porttitor."},
            new PostsModel {Title="A Question About Studies", Content="Cras nibh arcu, sagittis ac sollicitudin et, porttitor a velit. Suspendisse dignissim eu turpis vel porta. Vivamus tincidunt eleifend augue non egestas. Morbi accumsan nisl ut risus pretium imperdiet. Cras posuere nisl auctor dolor convallis, non condimentum purus pellentesque. Praesent vel viverra urna. Pellentesque ac risus mauris."},
            new PostsModel {Title="A Question About Studies", Content="Cras nibh arcu, sagittis ac sollicitudin et, porttitor a velit. Suspendisse." },
        };

        public RecentPostsPage()
        {
            InitializeComponent();
            BindingContext = this;
            Test();
        }
        public void Test()
        {
            lstRecentPosts.ItemsSource = PostMod;
        }

        private async void btnAccount_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountPage());
        }
    }
}