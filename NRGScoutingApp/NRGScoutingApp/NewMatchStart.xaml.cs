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

namespace NRGScoutingApp
{
    public partial class NewMatchStart : ContentPage
    {


        public NewMatchStart()
        {
            InitializeComponent();

        }

        private int min = 0, sec = 0, ms = 1;
        private int minutes, seconds, milliseconds;

        void resetClicked(object sender, System.EventArgs e)
        {
            if (timerValue.Text == "0:00.00" && startTimer.Text == "Start Timer")
            {
                DisplayAlert("Alert", "Already 0!", "OK");
            }
            else
            {
                timeSlider.Value = 0;
                min = 0; sec = 0; ms =0;
                timerValue.Text = "0:00.00";
            }

        }
        private Timer timer;
        void startClicked(object sender, System.EventArgs e)
        {
            
            if (startTimer.Text == "Start Timer")
            {
                
                startTimer.Text = "Pause Timer";
                timer = new Timer();
                timer.Interval = 1; // 1 ms
                timer.Elapsed += Timer_Elapsed;
                timer.Start();



            }
            else if (startTimer.Text == "Pause Timer")
            {
                startTimer.Text = "Start Timer";
                timer.Stop();
                timer = null;
            } 
        }
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            testLabel.Text = ms.ToString();
            ms++;
            //if(ms>=1000){
            //    sec++;
            //    ms = 0;
            //}222
            //if (sec==59){
            //    min++;
            //    sec = 0;
            //}
            testLabel.Text = ms.ToString(); //String.Format("{0}:{00}.{00}", min, sec, ms);   min + " " + sec + " " +
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
                double min = double.Parse(split1[0]);
                double sec = double.Parse(split2[0]);
                double ms = double.Parse(split2[1]) / 100;
                double timerrValue = min + sec + ms;
                timeSlider.Value = timerrValue;

            }
            else
            {
                double value = e.NewValue;
                if (value >= 60)
                {
                    minutes = (int)(Math.Floor(value / 60));
                    min = (int)(Math.Floor(value / 60));
                    seconds = (int)(Math.Floor(value - (60 * minutes)));
                    sec = (int)(Math.Floor(value - (60 * minutes)));
                    milliseconds = (int)(100 * (value - ((minutes * 60) + seconds)));
                    ms = (int)(100 * (value - ((minutes * 60) + seconds)));
                    timerValue.Text = minutes + ":" + seconds + "." + milliseconds;  //":" + seconds + "." + milliseconds;, minutes, seconds, milliseconds;
                }
                if (value <= 60)
                {
                    seconds = (int)(Math.Floor(value));
                    sec = (int)(Math.Floor(value));
                    milliseconds = (int)(100 * (value - seconds));
                    ms = (int)(100 * (value - seconds));
                    timerValue.Text = "0:" + seconds + "." + milliseconds;//(String.Format("0:{00}.{00}", seconds, milliseconds));
                }
                if (value <= 0)
                {
                    milliseconds = (int)(100 * (value));
                    ms = (int)(100 * (value));
                    timerValue.Text = "0:00." + milliseconds;
                }

            }
        }


        void climbClicked(object sender, System.EventArgs e)
        {
            if (startTimer.Text == "Start Timer")
            {
                DisplayAlert("Error", "Timer not Started", "OK");
            }
            else
            {
                //Adds info to to JSON about climb
            }
        }
        void cubeClicked(object sender, System.EventArgs e)
        {
            if (startTimer.Text == "Start Timer")
            {
                DisplayAlert("Error", "Timer not Started", "OK");
            }
            else
            {
                //Performs action/s to open popup for adding cube dropped, etc
            }


        }
    }
}
