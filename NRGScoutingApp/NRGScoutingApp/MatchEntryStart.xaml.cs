﻿using System;
using System.Collections.Generic;
using NRGScoutingApp;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms.Platform;
using Newtonsoft.Json;
using Xamarin.Forms.Xaml;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace NRGScoutingApp
{
    public partial class MatchEntryStart : ContentPage
    {

        public MatchEntryStart()
        {
            MatchesList.ItemsSource = teams;
            InitializeComponent();
        }
        public Boolean goBack = false;
        public string teamName;
            
        protected override void OnAppearing()
        {
            if (goBack == true){
                Navigation.PopAsync();
                goBack = false;
            }
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            teamName = e.Item.ToString();
            App.Current.Properties["teamStart"] = teamName;
            App.Current.SavePropertiesAsync();
            Navigation.PushAsync(new MatchEntryEditTab());
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // MatchesList.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                MatchesList.ItemsSource = teams;
            else
                MatchesList.ItemsSource = teams.Where(teams => teams.Contains(e.NewTextValue));

            //MatchesList.EndRefresh();
        }

        List<String> teams = TeamsNames.teams;

    }

      


}
