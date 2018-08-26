using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.ComponentModel;


namespace NRGScoutingApp
{
    public partial class MatchParameters : ContentPage
    {
        public static String pickerS;
        public static bool crossedB, switchB, scaleB, fswitchB, fscaleB, deathB, soloB, assistedB, neededB, platformB, 
        noclimbB, recyellowB, recredB;
        StringFormat paramFormat = new StringFormat();

        public MatchParameters()
        {
            InitializeComponent();
        }

        async void backClicked(object sender, System.EventArgs e)
        {
            var text = await DisplayAlert("Alert", "Do you want to discard progress?", "Yes", "No");
            //Boolean nav = (Boolean) text;
            var appbool = new Matches();
            if (text)
            {
                Application.Current.Properties["teamStart"] = "";
                Application.Current.Properties["appState"] = 0;
                App.Current.Properties["timerValue"] = (int)0;
                if (appbool.appRestore == false)
                {
                    appbool.appRestore = false;
                    //await Navigation.PopToRootAsync(true);
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopToRootAsync());
                }
                else if (appbool.appRestore == true)
                {
                    appbool.appRestore = false;
                    //await Navigation.PopAsync(true);
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                }

            }
        }
        void saveClicked(object sender, System.EventArgs e)
        {
            var appbool = new Matches();
            string param = paramFormat.ConvertMatchParam("hi", MatchParameters.pickerS, MatchParameters.crossedB, MatchParameters.switchB, MatchParameters.scaleB,
                                                          MatchParameters.fswitchB, MatchParameters.fscaleB, MatchParameters.deathB, MatchParameters.soloB,
                                                          MatchParameters.assistedB, MatchParameters.neededB, MatchParameters.platformB,
                                                          MatchParameters.noclimbB, "5", MatchParameters.recyellowB, MatchParameters.recredB, "great");
            DisplayAlert(param, "generated", "OK");
            Application.Current.Properties["teamStart"] = "";
            Application.Current.Properties["appState"] = 0;
            App.Current.Properties["timerValue"] = 0;
            if (appbool.appRestore == false)
            {
                appbool.appRestore = false;
                Device.BeginInvokeOnMainThread(async () => await Navigation.PopToRootAsync());
                //Navigation.PopToRootAsync();
            }
            else if (appbool.appRestore == true)
            {
                appbool.appRestore = false;
                Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                //Navigation.PopAsync(true);
            }
        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var pos = PositionPicker.Items[PositionPicker.SelectedIndex];
            pickerS = pos;
            DisplayAlert(pickerS, "Position Selected", "OK");
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var crossed = crossbase.IsToggled;
            crossedB = crossed;
        }

        void Handle_Toggled_1(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = switchP.IsToggled;
            switchB = y;
        }

        void Handle_Toggled_2(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = scale.IsToggled;
            scaleB = y;
        }

        void Handle_Toggled_3(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = farswitch.IsToggled;
            fswitchB = y;
        }

        void Handle_Toggled_4(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = farscale.IsToggled;
            fscaleB = y;
        }

        void Handle_Toggled_5(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = death.IsToggled;
            deathB = y;
        }

        void Handle_Toggled_6(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = solo.IsToggled;
            soloB = y;
        }

        void Handle_Toggled_7(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = assisted.IsToggled;
            assistedB = y;
        }

        void Handle_Toggled_8(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = needed.IsToggled;
            neededB = y;
        }

        void Handle_Toggled_9(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = platform.IsToggled;
            platformB = y;
        }

        void Handle_Toggled_10(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = noclimb.IsToggled;
            noclimbB = y;
        }

        void Handle_Toggled_11(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = yellow.IsToggled;
            recyellowB = y;
        }

        void Handle_Toggled_12(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            var y = red.IsToggled;
            recredB = y;
        }
      }
}
                                  
