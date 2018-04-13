using System;
using System.Collections.Generic;
using NRGScoutingApp;
using Xamarin.Forms;

namespace NRGScoutingApp
{
    public partial class MatchEntryStart : ContentPage
    {
        public MatchEntryStart()
        {
            InitializeComponent();


        }
        protected override bool OnBackButtonPressed()
        {
            // If you want to continue going back
            base.OnBackButtonPressed();
            DisplayAlert("Warning", "You will lose all unsaved data", "Cancel", "Ok");
            return false;

        }
      

    }
}
