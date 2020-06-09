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
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false); //Disables swiping in the app for tabbed page
        }

        //This will load listview data upon a tabbed page being clicked, it's useful for refreshing the listview data when new posts are added
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            var index = Children.IndexOf(CurrentPage); //Gets the index of the current page selected

            if (index == 0) //If else statements to determine what data to load depending on what index is selected
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
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}