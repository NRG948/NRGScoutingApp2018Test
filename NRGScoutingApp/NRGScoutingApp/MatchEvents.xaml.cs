using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;
using System.Linq;
using Data = System.Collections.Generic.KeyValuePair<string, string>;

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

        void Handle_Clicked(object sender, System.EventArgs e, List<Data> data)
        {
            listView.ItemsSource = data;
            //MatchEventsFormat.ParseUserEvents(App.matchEvents);
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            List<Data> entries = new List<Data>();
            entries = MatchEventsFormat.ParseUserEvents(App.matchEvents);
            var entry = sender as ContentView;
            Label value = entry.FindByName("value") as Label;
            String val = value.Text;
            Label key = entry.FindByName("key") as Label;
            String ke = key.Text;
            Data item = new Data(ke, val);
            int index = entries.IndexOf(item);
            if (ke.Equals("Cube Picked") && !entries[index + 1].Key.Contains("Cube Picked"))
            {
                entries.Remove(item);
                entries.RemoveAt(index);
            }
            else if (ke.Contains("Dropped"))
            {
                entries.Remove(item);
                entries.RemoveAt(index - 1);
            } else {
                entries.Remove(item);
            }
            Console.WriteLine(entries);
            Handle_Clicked(sender, e, entries);
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