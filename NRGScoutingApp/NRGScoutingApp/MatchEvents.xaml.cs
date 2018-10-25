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
        Data itemprev;
        public MatchEvents()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        List<Data> entries = new List<Data>();

        protected override void OnAppearing()
        {
            listView.ItemsSource = MatchEventsFormat.ParseUserEvents(App.matchEvents);
        }

        void Handle_Clicked(object sender, System.EventArgs e, List<Data> data)
        {
            listView.ItemsSource = data;
            //MatchEventsFormat.ParseUserEvents(App.matchEvents);
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            entries = MatchEventsFormat.ParseUserEvents(App.matchEvents);
            var entry = sender as ContentView;
            Label value = entry.FindByName("value") as Label;
            String val = value.Text;
            Label key = entry.FindByName("key") as Label;
            String ke = key.Text;
            Data item = new Data(ke, val);
            Console.WriteLine(item);
            if (!itemprev.Equals(null))
            {
                int indexof = entries.IndexOf(itemprev);
                if (itemprev.Key.Equals("Cube Picked") && !entries[indexof + 1].Key.Contains("Cube Picked"))
                {
                    entries.Remove(itemprev);
                    entries.RemoveAt(indexof);
                }
                else
                {
                    entries.Remove(itemprev);
                }
                //else if (itemprev.Key.Contains("Dropped") && indexof - 1 >= 0)
                //{
                //    entries.Remove(itemprev);
                //    entries.RemoveAt(indexof - 1);
                //}
            }
            int index = entries.IndexOf(item);
            if (ke.Equals("Cube Picked") && !entries[index + 1].Key.Contains("Cube Picked"))
            {
                entries.Remove(item);
                entries.RemoveAt(index);
                App.matchEvents = MatchEventsFormat.returnUserEventsAsString(entries);
            }
            else if (ke.Contains("Dropped") && index-1 >= 0)
            {
                entries.Remove(item);
                entries.RemoveAt(index - 1);
                App.matchEvents = MatchEventsFormat.returnUserEventsAsString(entries);
            } 
            else {
                entries.Remove(item);
                App.matchEvents = MatchEventsFormat.returnUserEventsAsString(entries);
            }
            Console.WriteLine(entries);
            Handle_Clicked(sender, e, entries);
<<<<<<< HEAD
            //entries.Clear();
            //item.Deconstruct(out ke, out val);
            itemprev = item;
            DisplayAlert("Alert", "Not Implemented yet! get to work on this!", "OK"); //Hidden Gem (TODO: Remove in Prod. version)
=======
>>>>>>> origin/master
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