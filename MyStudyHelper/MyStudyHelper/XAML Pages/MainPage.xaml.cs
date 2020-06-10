using System;
using Xamarin.Forms;
using System.ComponentModel;

namespace MyStudyHelper
{
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false); //Disables swiping in the app for TabbedPage
        }

        //On current page changed, do the following code below (Used for refreshing the various ListViews within the app)
        protected override void OnCurrentPageChanged()
        {
            var index = Children.IndexOf(CurrentPage); //Gets the index of the CurrentPage

            //If Else statements which determine what data to load depending on the index of the page selected
            if (index == 0)
            {
                MessagingCenter.Send<Object>(this, "click_first_tab");
            }
            else if (index == 1)
            {
                MessagingCenter.Send<Object>(this, "click_second_tab");
            }
            else if (index == 2) 
            {
                MessagingCenter.Send<Object>(this, "click_third_tab");
            }
            base.OnCurrentPageChanged();
        }

        //protected override void OnDisappearing()
        //{
        //    MessagingCenter.Unsubscribe<Object>(this, "click_first_tab");
        //    base.OnDisappearing();
        //}

        //Disables physical backbutton on device when on the MainPage
        protected override bool OnBackButtonPressed() { return true; }
    }
}