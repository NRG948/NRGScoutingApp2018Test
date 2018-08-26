using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.ComponentModel;

namespace NRGScoutingApp
{
    public partial class MatchEvents : ContentPage
    {
        StringFormat paramFormat = new StringFormat();
        public MatchEvents()
        {
            InitializeComponent();
        }

         async void backClicked (object sender, System.EventArgs e)
        {
            var text = await DisplayAlert("Alert", "Do you want to discard progress?", "Yes", "No");
            //Boolean nav = (Boolean) text;
            var appbool = new Matches();
            if (text){
                Application.Current.Properties["teamStart"] = "";
                Application.Current.Properties["appState"] = 0;
                App.Current.Properties["timerValue"] = (int)0;
                if (appbool.appRestore == false){
                    await Navigation.PopToRootAsync(true);
                }
                else if (appbool.appRestore == true){
                    await Navigation.PopAsync(true);
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
                Navigation.PopToRootAsync();
            }
           else if (appbool.appRestore == true)
            {
                appbool.appRestore = false;
                Navigation.PopAsync();
            }
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