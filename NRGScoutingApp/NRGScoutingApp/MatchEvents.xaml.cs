using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.ComponentModel;

namespace NRGScoutingApp
{
    public partial class MatchEvents : ContentPage
    {
        public MatchEvents()
        {
            InitializeComponent();
        }

         async void backClicked (object sender, System.EventArgs e)
        {
            var text = await DisplayAlert("Alert", "Do you want to discard progress?", "Yes", "No");
            //Boolean nav = (Boolean) text;
            if (text){
                await Navigation.PopAsync();
            }
            else{
                
            }
        }
        void saveClicked(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

      }
}
