using System;
using System.Collections.Generic;

using Xamarin.Forms;


namespace NRGScoutingApp
{
    public partial class MatchParameters : ContentPage
    {
        public static String pickerS;
        public static bool crossedB, switchB, scaleB, fswitchB, fscaleB, deathB, soloB, assistedB, neededB, platformB, 
        noclimbB, recyellowB, recredB;

        public MatchParameters()
        {
            InitializeComponent();
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
                                  
