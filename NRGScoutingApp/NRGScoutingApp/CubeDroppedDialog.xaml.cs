using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using System.Security.Cryptography.X509Certificates;
//using Security;
using System.Globalization;

namespace NRGScoutingApp
{
    public partial class CubeDroppedDialog
    {
        public CubeDroppedDialog()
        {
            BindingContext = this; 
            InitializeComponent();
        }
        void scaleClicked(object sender, System.EventArgs e)
        {
            App.matchEvents += "cubeDroppedScale:" + NewMatchStart.droppedTime + "||";
            PopupNavigation.Instance.PopAsync(true);
        }
        void allySwitchClicked(object sender, System.EventArgs e)
        {
            App.matchEvents += "cubeDroppedAllySwitch:" + NewMatchStart.droppedTime + "||";
            PopupNavigation.Instance.PopAsync(true);
        }
        void noneClicked(object sender, System.EventArgs e)
        {
            App.matchEvents += "cubeDroppedNone:" + NewMatchStart.droppedTime + "||";
            PopupNavigation.Instance.PopAsync(true);
        }
        void oppSwitchClicked(object sender, System.EventArgs e)
        {
            App.matchEvents += "cubeDroppedOppSwitch:" + NewMatchStart.droppedTime + "||";
            PopupNavigation.Instance.PopAsync(true);
        }
        void exchangeClicked(object sender, System.EventArgs e)
        {
            App.matchEvents += "cubeDroppedExchange:" + NewMatchStart.droppedTime + "||";
            PopupNavigation.Instance.PopAsync(true);
        }
        void backClicked(object sender, System.EventArgs e)
        {
            var MatchTimer = new NewMatchStart();
           // MatchTimer.cubePicked.Text = "Cube Dropped";
            Button cubePicked = (Button)sender;
            NewMatchStart.cubeDropSet();
            cubePicked.Text = "New Value";
            App.matchEvents += "cubeDropped:back||";
            PopupNavigation.Instance.PopAsync(true);
        }

    }
    //public class NewMatchStart : ContentPage
    //{
    //    public NewMatchStart()
    //    {
    //        cubePicked.Text = "Cube Picked";
    //    }
    //}
}


