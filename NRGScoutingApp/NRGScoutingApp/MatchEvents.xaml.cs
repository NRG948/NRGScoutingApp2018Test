using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.ComponentModel;

namespace NRGScoutingApp
{
    public partial class MatchEvents : ContentPage
    {
        //StringFormat paramFormat = new StringFormat();
        public MatchEvents()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            listView.ItemsSource = MatchEventsFormat.ParseUserEvents(App.matchEvents);
            //MatchEventsFormat.ParseUserEvents(App.matchEvents);
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            DisplayAlert("Alert", "Not Implemented yet! get to work on this!", "OK"); //Hidden Gem (TODO: Remove in Prod. version)
        }
      }
}

//void timerConfirm()
//{
    //if ((int)(App.Current.Properties["timerValue"]) >= 0)
    //{
    //        private double timerrValue = (int)(App.Current.Properties["timerValue"]);
    //}
    //        else{
    //            private double timerrValue;
    //        }
    //}