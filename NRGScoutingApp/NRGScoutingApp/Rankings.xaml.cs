using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NRGScoutingApp
{
    public partial class Rankings : ContentPage
    {
        public Rankings()
        {
            listView.ItemsSource = Ranker.mainRanker(App.Current.Properties["matchEventsString"].ToString());
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
           listView.ItemsSource = Ranker.mainRanker(App.Current.Properties["matchEventsString"].ToString());
        }

            // FOLLOWING BUTTON TO BE REMOVED IN PRODUCTION (DEBUG PURPOSES ONLY)
            async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var del = await DisplayAlert("notice", "clear ALL Entires??", "yes", "no");
            if (del)
            {
                App.Current.Properties.Clear();
                App.Current.Properties["matchEventsString"] = "(";
                await App.Current.SavePropertiesAsync();
            }
        }
    }
}
