using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Android.Provider;

namespace NRGScoutingApp
{
    public partial class BlueAllianceMatches : ContentPage
    {


        public BlueAllianceMatches()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            Browser.Source = "https://www.thebluealliance.com/";
        }
        private void backClicked(object sender, EventArgs e)
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
            }

        private void forwardClicked(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
            {
                Browser.GoForward();
            }
        }
    }
}
