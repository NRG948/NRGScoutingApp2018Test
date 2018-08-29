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


namespace NRGScoutingApp
{
    public partial class NewMatchStart : ContentPage
    {
        public NewMatchStart()
        {
            BindingContext = this;
            InitializeComponent();
            //cubeDropValue = "Cube Picked";
            App.Current.Properties["appState"] = "1";
            App.Current.SavePropertiesAsync();
            timerValueSetter();
        }

        public string cubeDropValue = "Cube Picked";
        private int min = 0, sec = 0, ms = 0;
        private int minutes, seconds, milliseconds;
        public double timerrValue;
        private Boolean firstTimerStart = true;

        void resetClicked(object sender, System.EventArgs e)
        {
            if (timerValue.Text == "0:00.00" || startTimer.Text == "Pause Timer"){}
            else if (startTimer.Text == "Start Timer")
            {
                timeSlider.Value = 0;
                min = 0; sec = 0; ms = 0;
                timerrValue = 0;
                App.Current.Properties["timerValue"] = (int)0;
                App.Current.SavePropertiesAsync();
                timerValue.Text = "0:00.00";
            }
        }

        void startClicked(object sender, System.EventArgs e)
        {
            if (startTimer.Text == "Start Timer") 
            {
                if (!App.Current.Properties.ContainsKey("timerValue")) {
                    App.Current.Properties["timerValue"] = (int) 0;
                    App.Current.SavePropertiesAsync();
                }
                else if (App.Current.Properties.ContainsKey("timerValue") && firstTimerStart== true)
                {
                    timerrValue = Convert.ToDouble(App.Current.Properties["timerValue"]);
                    if (timerrValue >= 60000)
                    {
                        minutes = (int)(Math.Floor(timerrValue / 60000));
                        min = (int)(Math.Floor(timerrValue / 60000));
                        seconds = (int)(Math.Floor(timerrValue - (60000 * minutes)) / 1000);
                        sec = (int)(Math.Floor(timerrValue - (60000 * minutes)) / 1000);
                        milliseconds = (int)(((timerrValue - ((minutes * 60000) + (seconds * 1000)))) / 10);
                        ms = (int)((timerrValue - ((minutes * 60000) + (seconds * 1000))));
                        timerValue.Text = String.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds); //minutes + ":" + seconds + "." + milliseconds;
                    }
                    if (timerrValue <= 60000)
                    {
                        seconds = (int)(Math.Floor(timerrValue / 1000));
                        sec = (int)(Math.Floor(timerrValue / 1000));
                        milliseconds = (int)(((timerrValue - (seconds * 1000))) / 10);
                        ms = (int)((timerrValue - (seconds * 1000)));
                        min = 0;
                        timerValue.Text = String.Format("0:{0:00}.{1:00}", seconds, milliseconds);
                    }
                    if (timerrValue <= 1000)
                    {
                        milliseconds = (int)((timerrValue) / 10);
                        min = 0;
                        sec = 0;
                        ms = (int)((timerrValue) / 10);
                        timerValue.Text = String.Format("0:00.{0:00}", milliseconds);
                    }
                    firstTimerStart = false;
                }
                startTimer.Text = "Pause Timer";
                Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
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
        private void Timer_Elapsed()
        {
            ms += 1;
            timerrValue += 1;
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
            timeSlider.Value = timerrValue;
            App.Current.Properties["timerValue"] = (int)timerrValue;
            App.Current.SavePropertiesAsync();
            timerValue.Text = min + ":" + sec + "." + (ms/10);
        }

        void timerValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if (startTimer.Text == "Pause Timer") 
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
                double sliderTimerValue = mins * 60000 + secs * 1000 + mss;
                timeSlider.Value = timerrValue;

            }
            else if (startTimer.Text == "Start Timer")
            {
                if (!App.Current.Properties.ContainsKey("timerValue")){
                    App.Current.Properties["timerValue"] = (int) 0;
                    App.Current.SavePropertiesAsync();
                }
                else if(App.Current.Properties.ContainsKey("timerValue") && firstTimerStart == true)
                {
                    timerrValue = (int)(App.Current.Properties["timerValue"]);
                    firstTimerStart = false;
                    }
                else{
                    App.Current.Properties["timerValue"] = (int)timeSlider.Value;
                    App.Current.SavePropertiesAsync();
                }
                double value = e.NewValue;
                if (value >= 60000)
                {
                    minutes = (int)(Math.Floor(value / 60000));
                    min = (int)(Math.Floor(value / 60000));
                    seconds = (int)(Math.Floor(value - (60000 * minutes)) / 1000);
                    sec = (int)(Math.Floor(value - (60000 * minutes)) / 1000);
                    milliseconds = (int)(((value - ((minutes * 60000) + (seconds * 1000)))) / 10);
                    ms = (int)((value - ((minutes * 60000) + (seconds * 1000)))); 
                    timerrValue = (double)(timeSlider.Value);
                    timerValue.Text = String.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds); //minutes + ":" + seconds + "." + milliseconds;
                }
                if (value <= 60000)
                {
                    seconds = (int)(Math.Floor(value / 1000));
                    sec = (int)(Math.Floor(value / 1000));
                    milliseconds = (int)(((value - (seconds * 1000))) / 10); 
                    ms = (int)((value - (seconds * 1000))); 
                    min = 0;
                    timerrValue = (double)(timeSlider.Value); 
                    timerValue.Text = String.Format("0:{0:00}.{1:00}", seconds, milliseconds);
                }
                if (value <= 1000)
                {
                    milliseconds = (int)((value) / 10);
                    min = 0;
                    sec = 0;
                    ms = (int)((value) / 10); 
                    timerrValue = (int)(timeSlider.Value); 
                    timerValue.Text = String.Format("0:00.{0:00}", milliseconds);
                }
            }
        }


        void climbClicked(object sender, System.EventArgs e)
        {
            if (startTimer.Text == "Start Timer") //
            {
                DisplayAlert("Error", "Timer not Started", "OK");
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
                cubeDropValue = "Cube Dropped";
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
                cubeDropValue = "Cube Picked";
                cubePicked.Text = "Cube Picked";
            }
        }

        public void cubeEventStatus()
        {
            var cubeText = new NewMatchStart();
            cubeText.cubePicked.Text = "Cube Picked";
        }
        private void timerValueSetter()
        {
            if (!App.Current.Properties.ContainsKey("timerValue"))
            {
                App.Current.Properties["timerValue"] = (int)0;
                App.Current.SavePropertiesAsync();
            }
            else if (App.Current.Properties.ContainsKey("timerValue") && firstTimerStart == true)
            {
                timerrValue = Convert.ToDouble(App.Current.Properties["timerValue"]);
                timeSlider.Value = timerrValue;
                if (timerrValue >= 60000)
                {
                    minutes = (int)(Math.Floor(timerrValue / 60000));
                    min = (int)(Math.Floor(timerrValue / 60000));
                    seconds = (int)(Math.Floor(timerrValue - (60000 * minutes)) / 1000);
                    sec = (int)(Math.Floor(timerrValue - (60000 * minutes)) / 1000);
                    milliseconds = (int)(((timerrValue - ((minutes * 60000) + (seconds * 1000)))) / 10);
                    ms = (int)((timerrValue - ((minutes * 60000) + (seconds * 1000))));
                    timerValue.Text = String.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds); //minutes + ":" + seconds + "." + milliseconds;
                }
                if (timerrValue <= 60000)
                {
                    seconds = (int)(Math.Floor(timerrValue / 1000));
                    sec = (int)(Math.Floor(timerrValue / 1000));
                    milliseconds = (int)(((timerrValue - (seconds * 1000))) / 10);
                    ms = (int)((timerrValue - (seconds * 1000)));
                    min = 0;
                    timerValue.Text = String.Format("0:{0:00}.{1:00}", seconds, milliseconds);
                }
                if (timerrValue <= 1000)
                {
                    milliseconds = (int)((timerrValue) / 10);
                    min = 0;
                    sec = 0;
                    ms = (int)((timerrValue) / 10);
                    timerValue.Text = String.Format("0:00.{0:00}", milliseconds);
                }
                firstTimerStart = false;
            }

        }
    }
}


