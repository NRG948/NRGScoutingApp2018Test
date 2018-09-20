using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using System.Security.Cryptography.X509Certificates;
//using Security;
using System.Globalization;

namespace NRGScoutingApp
{
    public partial class ImportDialog
    {
        public ImportDialog()
        {
            BindingContext = this;
            InitializeComponent();
        }

        void cancelClicked(object sender, System.EventArgs e)
        {
                PopupNavigation.Instance.PopAsync(true);
        }
        void importClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Imported", importData.Text, "OK");
            PopupNavigation.Instance.PopAsync(true);
           
        }

    }
}


