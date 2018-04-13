using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms.Platform;
using NRGScoutingApp;


namespace NRGScoutingApp
{
    public partial class WelcomePage : ContentPage
    {
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new MatchEntryStart());
        }

       



        public WelcomePage()
        {
            InitializeComponent();


            if (Device.RuntimePlatform == Device.iOS)
            {
                textColor = "#000000";

            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                textColor = "#fbfdf8";
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                bgColor = "#ffffff";
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                bgColor = "#d7401a";
            }






        }
        public string textColor;
        public string bgColor;

    }
}
