using System;
using System.Collections.Generic;
using NRGScoutingApp;
using Xamarin.Forms;
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

        // INTERNAL VARIABLES FOR SETTING KEY TIMER AND BUTTON VALUES (DO NOT CHANGE THIS)
        public static readonly double matchSpanMs = 150000;
        public static readonly double minMs = 60000;
        public static readonly double secMs = 1000;
        public static readonly String cubePickedText = "Cube Picked";
        public static readonly String cubeDroppedText = "Cube Dropped";

        public NewMatchStart()
        {
            BindingContext = this;
            InitializeComponent();
            //cubeDropValue = "Cube Picked";
            App.Current.Properties["appState"] = "1";
            App.Current.SavePropertiesAsync();
            NavigationPage.SetHasBackButton(this, false);
            timerValueSetter();
        }

        public string cubeDropValue = "Cube Picked";
        private int min = 0, sec = 0, ms = 0;
        private int minutes, seconds, milliseconds;
        public double timerrValue;
        private Boolean firstTimerStart = true;
        public static double pickedTime = 0;
        public static double droppedTime = 0;
        int climbTime = 0;

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
                    if (timerrValue >= minMs)
                    {
                        minutes = (int)(Math.Floor(timerrValue / minMs));
                        min = (int)(Math.Floor(timerrValue / minMs));
                        seconds = (int)(Math.Floor(timerrValue - (minMs * minutes)) / secMs);
                        sec = (int)(Math.Floor(timerrValue - (minMs * minutes)) / secMs);
                        milliseconds = (int)(((timerrValue - ((minutes * minMs) + (seconds * secMs)))) / 10);
                        ms = (int)((timerrValue - ((minutes * minMs) + (seconds * secMs))));
                        timerValue.Text = String.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds); //minutes + ":" + seconds + "." + milliseconds;
                    }
                    if (timerrValue <= minMs)
                    {
                        seconds = (int)(Math.Floor(timerrValue / secMs));
                        sec = (int)(Math.Floor(timerrValue / secMs));
                        milliseconds = (int)(((timerrValue - (seconds * secMs))) / 10);
                        ms = (int)((timerrValue - (seconds * secMs)));
                        min = 0;
                        timerValue.Text = String.Format("0:{0:00}.{1:00}", seconds, milliseconds);
                    }
                    if (timerrValue <= secMs)
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
                if (Device.RuntimePlatform == "iOS"){
                    Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
                    {
                        if ((timerrValue >= matchSpanMs) || startTimer.Text == "Start Timer")
                        {
                            startTimer.Text = "Start Timer";
                            return false;
                        }
                        Timer_Elapsed(); return true;
                    });
                }
                else if (Device.RuntimePlatform == "Android"){
                    Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                    {
                        if ((timerrValue >= matchSpanMs) || startTimer.Text == "Start Timer")
                        {
                            startTimer.Text = "Start Timer";
                            return false;
                        }
                        Timer_Elapsed(); return true;
                    });
                }

            }
            else if (startTimer.Text == "Pause Timer") //
            {
              startTimer.Text = "Start Timer";
            }
        }
        private void Timer_Elapsed()
        {
            if (Device.RuntimePlatform=="iOS"){
                ms += 1;
                timerrValue += 1;
            }
            else if (Device.RuntimePlatform == "Android")
            {
                ms += 100;
                timerrValue += 100;
            }
            if (ms >= secMs)
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
                double sliderTimerValue = mins * minMs + secs * secMs
                 + mss;
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
                if (value >= minMs)
                {
                    minutes = (int)(Math.Floor(value / minMs));
                    min = (int)(Math.Floor(value / minMs));
                    seconds = (int)(Math.Floor(value - (minMs * minutes)) / secMs);
                    sec = (int)(Math.Floor(value - (minMs * minutes)) / secMs);
                    milliseconds = (int)(((value - ((minutes * minMs) + (seconds * secMs)))) / 10);
                    ms = (int)((value - ((minutes * minMs) + (seconds * secMs)))); 
                    timerrValue = (double)(timeSlider.Value);
                    timerValue.Text = String.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds); //minutes + ":" + seconds + "." + milliseconds;
                }
                if (value <= minMs)
                {
                    seconds = (int)(Math.Floor(value / secMs));
                    sec = (int)(Math.Floor(value / secMs));
                    milliseconds = (int)(((value - (seconds * secMs))) / 10); 
                    ms = (int)((value - (seconds * secMs))); 
                    min = 0;
                    timerrValue = (double)(timeSlider.Value); 
                    timerValue.Text = String.Format("0:{0:00}.{1:00}", seconds, milliseconds);
                }
                if (value <= secMs)
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
            else if (climbTime == 0)
            {
                //Adds info to to JSON about climb
                climbTime = (int)timerrValue;
                App.matchEvents +=  "climbStart:" + climbTime +"||";
            }
        }
        void cubeClicked(object sender, System.EventArgs e)
        {
            if (startTimer.Text == "Start Timer")  //
            {
                DisplayAlert("Error", "Timer not Started", "OK");
            }

            else if (cubePicked.Text == cubePickedText)
            {
                //Performs actions to open popup for adding cube dropped, etc
                pickedTime = (int)timerrValue;
                App.Current.Properties["lastCubePicked"] = (int)pickedTime;
                App.Current.SavePropertiesAsync();
                testButton.Text = "Picked " + pickedTime;
                App.matchEvents += "cubePicked:" + pickedTime +"|";
                cubeDropValue = cubeDroppedText;
                cubePicked.Text = cubeDroppedText;

            }
            else if (cubePicked.Text == cubeDroppedText)
            {
                //Performs action/s to open popup for adding cube dropped, etc
                droppedTime =(int)timerrValue;
                App.Current.Properties["lastCubeDropped"] = (int)droppedTime;
                App.Current.SavePropertiesAsync();
                testButton.Text = "Dropped " + droppedTime;
                PopupNavigation.Instance.PushAsync(new CubeDroppedDialog());
                cubeDropValue = cubePickedText;
                cubePicked.Text = cubePickedText;
            }
        }

        public void cubeEventStatus()
        {
            var cubeText = new NewMatchStart();
            cubeText.cubePicked.Text = cubePickedText;
        }
        private void timerValueSetter()
        {
            if(!App.Current.Properties.ContainsKey("lastCubePicked")){
                App.Current.Properties["lastCubePicked"] = 0;
                App.Current.Properties["lastCubeDropped"] = 0;
                App.Current.SavePropertiesAsync();
            }
            else if(Convert.ToInt32(App.Current.Properties["lastCubePicked"]) == 0 || Convert.ToInt32(App.Current.Properties["lastCubeDropped"]) == 0){

            }
            else if(Convert.ToInt32(App.Current.Properties["lastCubePicked"]) > Convert.ToInt32(App.Current.Properties["lastCubeDropped"])){
                cubePicked.Text = cubeDroppedText;
            }

            if (!App.Current.Properties.ContainsKey("timerValue"))
            {
                App.Current.Properties["timerValue"] = (int)0;
                App.Current.SavePropertiesAsync();
            }
            else if (App.Current.Properties.ContainsKey("timerValue") && firstTimerStart == true)
            {
                timerrValue = Convert.ToDouble(App.Current.Properties["timerValue"]);
                timeSlider.Value = timerrValue;
                if (timerrValue >= minMs)
                {
                    minutes = (int)(Math.Floor(timerrValue / minMs));
                    min = (int)(Math.Floor(timerrValue / minMs));
                    seconds = (int)(Math.Floor(timerrValue - (minMs * minutes)) / secMs);
                    sec = (int)(Math.Floor(timerrValue - (minMs * minutes)) / secMs);
                    milliseconds = (int)(((timerrValue - ((minutes * minMs) + (seconds * secMs)))) / 10);
                    ms = (int)((timerrValue - ((minutes * minMs) + (seconds * secMs))));
                    timerValue.Text = String.Format("{0}:{1:00}.{2:00}", minutes, seconds, milliseconds); //minutes + ":" + seconds + "." + milliseconds;
                }
                if (timerrValue <= minMs)
                {
                    seconds = (int)(Math.Floor(timerrValue / secMs));
                    sec = (int)(Math.Floor(timerrValue / secMs));
                    milliseconds = (int)(((timerrValue - (seconds * secMs))) / 10);
                    ms = (int)((timerrValue - (seconds * secMs)));
                    min = 0;
                    timerValue.Text = String.Format("0:{0:00}.{1:00}", seconds, milliseconds);
                }
                if (timerrValue <= secMs)
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
        public static void cubeDropSet()
        {
            var pickSet = new NewMatchStart();
            pickSet.cubePicked.Text = "Cube Dropped";
            Console.WriteLine ("Passed CubeDropset");
        }

    }

}


