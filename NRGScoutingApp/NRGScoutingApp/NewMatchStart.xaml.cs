using System;
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
using System.Timers;
using Rg.Plugins.Popup.Services;
using System.Linq.Expressions;
using Security;


namespace NRGScoutingApp
{
    public partial class NewMatchStart : ContentPage
    {

        public NewMatchStart()
        {
            InitializeComponent();
            App.Current.Properties["appState"] = "1";
            App.Current.SavePropertiesAsync();
            timerConfirm();

        }
        private int min = 0, sec = 0, ms = 0;
        private int minutes, seconds, milliseconds;


        void resetClicked(object sender, System.EventArgs e)
        {
            if (timerValue.Text == "0:00.00" || startTimer.Text == "Pause Timer") //
            {
                //DisplayAlert("Alert", "Already 0!", "OK");
            }
            else if (startTimer.Text == "Start Timer")
            {
                timeSlider.Value = 0;
                min = 0; sec = 0; ms = 0;
                timerrValue = 0;
                App.Current.Properties["timerValue"] = 0;
                timerValue.Text = "0:00.00";
            }

        }

        void startClicked(object sender, System.EventArgs e)
        {

            if (startTimer.Text == "Start Timer") //
            {
                startTimer.Text = "Pause Timer";
                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                {
                    if ((timerrValue >= 150000) || startTimer.Text == "Start Timer")
                    {
                        startTimer.Text = "Start Timer";
                        return false;
                    }
                    Timer_Elapsed(); return true;
                });

            }
            else if (startTimer.Text == "Pause Timer") //
            {
              startTimer.Text = "Start Timer";
            }
        }
        private void Timer_Elapsed() //sender  System.Timers.ElapsedEventArgs
        {
            ms += 100;
            timerrValue += 100;
            if (ms >= 1000)
            {
                sec++;
                ms = 0;
            }
            if (sec == 60)
            {
                min++;
                sec = 0;
            }
            // if (ms >= 100)
            // {
            //     mst = (int)(ms / 100);
            // }
            // else if (ms <= 100)
            // {
            //     mst = (int)(ms / 10);
            //}
            timeSlider.Value = 70;
            App.Current.Properties["timerValue"] = timerrValue;
            App.Current.SavePropertiesAsync();
            timerValue.Text = min + ":" + sec + "." + (ms / 10);// String.Format("{0}:{00}.{00}", min, sec, ms / 10);
        }

        void timerValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if (startTimer.Text == "Pause Timer") //
            {
                string TimerValue = timerValue.Text;
                char[] seperator = new char[] { ':' };
                string[] split1 = TimerValue.Split(seperator, StringSplitOptions.None);
                char[] seperator2 = new char[] { '.' };
                string splitStrArr = (string)split1[1];
                string[] split2 = splitStrArr.Split(seperator2, StringSplitOptions.None);
                double mins = double.Parse(split1[0]);
                double secs = double.Parse(split2[0]);
                double mss = 8;
                if (double.Parse(split2[1]) < 10)
                {
                    mss = double.Parse(split2[1]) * 100;
                }
                else if (double.Parse(split2[1]) > 10)
                {
                    mss = double.Parse(split2[1]) * 10;
                }
                //double mss = double.Parse(split2[1]); ///100
                double sliderTimerValue = mins * 60000 + secs * 1000 + mss;
                //timerrValue = (int)(sliderTimerValue * 100);
                timeSlider.Value = sliderTimerValue;

            }
            else
            {
                double value = e.NewValue;
                if (value >= 60000)
                {
                    minutes = (int)(Math.Floor(value / 60000));
                    min = (int)(Math.Floor(value / 60000));
                    seconds = (int)(Math.Floor(value - (60000 * minutes)) / 1000);
                    sec = (int)(Math.Floor(value - (60000 * minutes)) / 1000);
                    milliseconds = (int)(((value - ((minutes * 60000) + (seconds * 1000)))) / 10);
                    ms = (int)((value - ((minutes * 60000) + (seconds * 1000))));  //((100 * (value - ((minutes * 60) + seconds))))
                    timerrValue = (int)(value); //*1000
                    timerValue.Text = minutes + ":" + seconds + "." + milliseconds;  //":" + seconds + "." + milliseconds;, minutes, seconds, milliseconds;
                }
                if (value <= 60000)
                {
                    seconds = (int)(Math.Floor(value / 1000));
                    sec = (int)(Math.Floor(value / 1000));
                    milliseconds = (int)(((value - (seconds * 1000))) / 10); //((100 * (value - seconds))/10)
                    ms = (int)((value - (seconds * 1000))); //((100 * (value - seconds)))
                    min = 0;
                    timerrValue = (int)(value); //*1000
                    timerValue.Text = "0:" + seconds + "." + milliseconds;//(String.Format("0:{00}.{00}", seconds, milliseconds));
                }
                if (value <= 1000)
                {
                    milliseconds = (int)((value) / 10);
                    min = 0;
                    sec = 0;
                    ms = (int)((value) / 10); //((100 * (value)))
                    timerrValue = (int)(value); //*1000
                    timerValue.Text = "0:00." + milliseconds;
                }

            }
        }


        void climbClicked(object sender, System.EventArgs e)
        {
            if (startTimer.Text == "Start Timer") //
            {
                DisplayAlert("Error", "Timer not Started", "OK");
                string appvalue = (App.Current.Properties["appState"].ToString());
                DisplayAlert("Page Value", appvalue, "OK");
            }
            else
            {
                //Adds info to to JSON about climb
                string TimerValue = timerValue.Text;
                char[] seperator = new char[] { ':' };
                string[] split1 = TimerValue.Split(seperator, StringSplitOptions.None);
                char[] seperator2 = new char[] { '.' };
                string splitStrArr = (string)split1[1];
                string[] split2 = splitStrArr.Split(seperator2, StringSplitOptions.None);
                double mins = double.Parse(split1[0]);
                double secs = double.Parse(split2[0]);
                double mss = double.Parse(split2[1]) / 100;
                timerrValue = mins + secs + mss;
            }
        }
        void cubeClicked(object sender, System.EventArgs e)
        {
            if (startTimer.Text == "Start Timer")  //
            {
                DisplayAlert("Error", "Timer not Started", "OK");
            }

            else if (cubePicked.Text == "Cube Picked")
            {
                //Performs action/s to open popup for adding cube dropped, etc
                string TimerValue = timerValue.Text;
                char[] seperator = new char[] { ':' };
                string[] split1 = TimerValue.Split(seperator, StringSplitOptions.None);
                char[] seperator2 = new char[] { '.' };
                string splitStrArr = (string)split1[1];
                string[] split2 = splitStrArr.Split(seperator2, StringSplitOptions.None);
                double mins = double.Parse(split1[0]);
                double secs = double.Parse(split2[0]);
                double mss = double.Parse(split2[1]) / 100;
                double pickedTime = mins + secs + mss;
                cubePicked.Text = "Cube Dropped";
            }
            else if (cubePicked.Text == "Cube Dropped")
            {
                //Performs action/s to open popup for adding cube dropped, etc
                string TimerValue = timerValue.Text;
                char[] seperator = new char[] { ':' };
                string[] split1 = TimerValue.Split(seperator, StringSplitOptions.None);
                char[] seperator2 = new char[] { '.' };
                string splitStrArr = (string)split1[1];
                string[] split2 = splitStrArr.Split(seperator2, StringSplitOptions.None);
                double mins = double.Parse(split1[0]);
                double secs = double.Parse(split2[0]);
                double mss = double.Parse(split2[1]) / 100;
                double droppedTime = mins + secs + mss;
                PopupNavigation.Instance.PushAsync(new CubeDroppedDialog());
            }



        }
        public void cubeEventStatus()
        {
            var cubeText = new NewMatchStart();
            cubeText.DisplayAlert("hi", "hello", "OK");
            cubeText.cubePicked.Text = "Cube Picked";

            cubeText.DisplayAlert("hujki", "hello", "OK");

            cubeText.DisplayAlert("why", "y", "ok");
        }
        void timerConfirm()
        {
            if ((int)App.Current.Properties["timerValue"] >= 0)
            {

                Navigation.PushAsync(new MatchEntryEditTab());
            }
            //else{
            //    var confirm = await DisplayAlert("Confirm", "Do you want to discard the match?", "Yes", "No");
            //    if (confirm){
            //        App.Current.Properties["appState"] = "0";
            //        App.Current.Properties["teamStart"] = "";
            //        await App.Current.SavePropertiesAsync();
            //    }
            //    else{
            //        await Navigation.PushAsync(new MatchEntryEditTab());
            //    }
            //}
            else
            {
                App.Current.Properties["appState"] = 0;
                App.Current.Properties["timerValue"] = 0;
                App.Current.SavePropertiesAsync();
                DisplayAlert("Alert", App.Current.Properties["appState"].ToString(), "OK");
            }
        }


    }
}


