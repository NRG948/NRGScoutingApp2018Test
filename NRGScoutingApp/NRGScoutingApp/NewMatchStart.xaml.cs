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

    /* NOTICE: REFERENCE 
     * resetTimerIOS and resetTimerAndroid when changing reset Timer text
     * startTimerIOS and startTimerAndroid when changing start timer text
     * climbStartIOS and climbStartAndroid when changing climb timer text
     * cubePickedIOS and cubePickedAndroid when changing cube picked/dropped text
     */
    public partial class NewMatchStart : ContentPage
    {

        //INTERNAL VARIABLES FOR SETTING IMPORTANT TIMER AND BUTTON VALUES (DO NOT CHANGE THIS)
        public static readonly double matchSpanMs = 150000;
        public static readonly double minMs = 60000;
        public static readonly double secMs = 1000;
        public static readonly String cubePickedText = "Cube Picked";
        public static readonly String cubeDroppedText = "Cube Dropped";
        public static readonly String timerStart = "Start Timer";
        public static readonly String timerPause = "Pause Timer";

        public NewMatchStart()
        {
            BindingContext = this;
            InitializeComponent();
            App.Current.Properties["appState"] = "1";
            App.Current.SavePropertiesAsync();
            NavigationPage.SetHasBackButton(this, false);
            timerValueSetter();
        }

        public string cubeDropValue = "Cube Picked";
        private static int min = 0, sec = 0, ms = 0; //Values for Timer
        private int minutes, seconds, milliseconds;
        public static double timerrValue;
        private Boolean firstTimerStart = true;
        public static double pickedTime = 0;
        public static double droppedTime = 0;
        public static int pickNum = 0, dropNum = 0;
        int climbTime = 0;

        void resetClicked(object sender, System.EventArgs e)
        {
            if (timerValue.Text == "0:00.00" || (startTimerAndroid.Text == timerPause || startTimerIOS.Text == timerPause)) {}
            else if (startTimerAndroid.Text == timerStart || startTimerIOS.Text == timerStart)
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
            if (startTimerAndroid.Text == timerStart || startTimerIOS.Text == timerStart) 
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
                startTimerIOS.Text = timerPause;
                startTimerAndroid.Text = timerPause;
                if (Device.RuntimePlatform == "iOS"){
                    Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
                    {
                        if ((timerrValue >= matchSpanMs) || (startTimerAndroid.Text == timerStart || startTimerIOS.Text == timerStart))
                        {
                            startTimerIOS.Text = timerStart;
                            startTimerAndroid.Text = timerStart;
                            return false;
                        }
                        Timer_Elapsed(); return true;
                    });
                }
                else if (Device.RuntimePlatform == "Android"){
                    Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                    {
                        if ((timerrValue >= matchSpanMs) || (startTimerAndroid.Text == timerStart || startTimerIOS.Text == timerStart))
                        {
                            startTimerIOS.Text = timerStart;
                            startTimerAndroid.Text = timerStart;
                            return false;
                        }
                        Timer_Elapsed(); return true;
                    });
                }

            }
            else if (startTimerAndroid.Text == timerPause || startTimerIOS.Text == timerPause) //
            {
                startTimerIOS.Text = timerStart;
                startTimerAndroid.Text = timerStart;
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
            timerValue.Text = min + ":" +  sec.ToString("D2") + "." + (ms/10).ToString("D2");
        }

        void timerValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if (startTimerAndroid.Text == timerPause || startTimerIOS.Text == timerPause) 
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
                double sliderTimerValue = mins * minMs + secs * secMs + mss;
                timeSlider.Value = timerrValue;

            }
            else if (startTimerAndroid.Text == timerStart || startTimerIOS.Text == timerStart)
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
                timerValue.Text = timeToString((int)value);
                timerrValue = (double)(timeSlider.Value);

            }
        }


        void climbClicked(object sender, System.EventArgs e)
        {
            if (startTimerAndroid.Text == timerStart || startTimerIOS.Text == timerStart) //
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
            if (startTimerAndroid.Text == timerStart || startTimerIOS.Text == timerStart)  //
            {
                DisplayAlert("Error", "Timer not Started", "OK");
            }

            else if (cubePickedAndroid.Text == cubePickedText || cubePickedIOS.Text == cubePickedText)
            {
                //Performs actions to open popup for adding cube dropped, etc
                pickedTime = (int)timerrValue;
                App.Current.Properties["lastCubePicked"] = (int)pickedTime;
                App.Current.SavePropertiesAsync();
                testButton.Text = "Picked " + pickedTime;  //DEBUG for Checking
                App.matchEvents += "cubePicked" + pickNum + ":" + pickedTime +"|";
                pickNum++;
                cubeDropValue = cubeDroppedText;
                cubePickedAndroid.Text = cubeDroppedText;
                cubePickedIOS.Text = cubeDroppedText;

            }
            else if (cubePickedAndroid.Text == cubeDroppedText || cubePickedIOS.Text == cubeDroppedText)
            {
                //Performs action/s to open popup for adding cube dropped, etc
                droppedTime =(int)timerrValue;
                App.Current.Properties["lastCubeDropped"] = (int)droppedTime;
                App.Current.SavePropertiesAsync();
                testButton.Text = "Dropped " + droppedTime; //DEBUG for Checking
                PopupNavigation.Instance.PushAsync(new CubeDroppedDialog());
                cubeDropValue = cubePickedText;
                cubePickedAndroid.Text = cubePickedText;
                cubePickedIOS.Text = cubePickedText;
            }
        }



        //TODO: Call Parse Method for match number called and figure out the pickNum and dropNum values and set them
        private void timerValueSetter()
        {
            if(!App.Current.Properties.ContainsKey("lastCubePicked")){
                App.Current.Properties["lastCubePicked"] = 0;
                App.Current.Properties["lastCubeDropped"] = 0;
                App.Current.SavePropertiesAsync();
            }
            else if(Convert.ToInt32(App.Current.Properties["lastCubePicked"]) == 0 || Convert.ToInt32(App.Current.Properties["lastCubeDropped"]) == 0){}
            else if(Convert.ToInt32(App.Current.Properties["lastCubePicked"]) > Convert.ToInt32(App.Current.Properties["lastCubeDropped"])){
                cubePickedAndroid.Text = cubeDroppedText;
                cubePickedIOS.Text = cubeDroppedText;
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
                timerValue.Text = timeToString((int)timerrValue);
                firstTimerStart = false;
            }

        }

        public static void cubeDropSet()
        {
            var pickSet = new NewMatchStart();
            pickSet.cubePickedAndroid.Text = cubeDroppedText;
            pickSet.cubePickedIOS.Text = cubeDroppedText;
            Console.WriteLine ("Passed CubeDropset");
        }

        public static string timeToString(int timeValue){
            int minutes = 0;
            int seconds = 0;
            int milliseconds = 0;
            minutes = timeValue / (int)minMs;
            timeValue %= (int)minMs;
            seconds = timeValue / (int)secMs;
            timeValue %= (int)secMs;
            milliseconds = timeValue;
            return minutes + ":" + seconds.ToString("D2") + "." + (milliseconds / 10).ToString("D2");
        }

    }

}


