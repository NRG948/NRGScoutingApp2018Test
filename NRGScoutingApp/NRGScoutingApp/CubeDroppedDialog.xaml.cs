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
            InitializeComponent();
        }
        public static Boolean EventOccured;
        void scaleClicked(object sender, System.EventArgs e)
        {
                    
            NewMatchStart.cubeEventStatus();
            PopupNavigation.Instance.PopAsync(true);
            }
        void allySwitchClicked(object sender, System.EventArgs e)
        {
            
            NewMatchStart.cubeEventStatus();
            PopupNavigation.Instance.PopAsync(true);
        }
        void noneClicked(object sender, System.EventArgs e)
        {
            
            NewMatchStart.cubeEventStatus();
            PopupNavigation.Instance.PopAsync(true);
        }
        void oppSwitchClicked(object sender, System.EventArgs e)
        {
           
            NewMatchStart.cubeEventStatus();
            PopupNavigation.Instance.PopAsync(true);
        }
        void exchangeClicked(object sender, System.EventArgs e)
        {
          
            NewMatchStart.cubeEventStatus();
            PopupNavigation.Instance.PopAsync(true);
        }
        void backClicked(object sender, System.EventArgs e)
        {
            //EventOccured = false;
            //NewMatchStart.cubeEventStatus(false);
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


