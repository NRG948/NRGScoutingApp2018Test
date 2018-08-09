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

        private int min = 0, sec = 0, ms = 0;
        private int minutes, seconds, milliseconds;
        private double timerrValue;

        void resetClicked(object sender, System.EventArgs e)
        {
            if (timerValue.Text == "0:00.00" || startTimer.Text == "Pause Timer") //
            {
                //DisplayAlert("Alert", "Already 0!", "OK");
            }
            else if (startTimer.Text =="Start Timer")
            {
                timeSlider.Value = 0;
                min = 0; sec = 0; ms = 0;
                timerrValue = 0;
                timerValue.Text = "0:00.00";
            }

        }
       
        void startClicked(object sender, System.EventArgs e)
        {
            
            if (startTimer.Text == "Start Timer") //
            {
                
                startTimer.Text = "Pause Timer";

                //timer = new Timer(150001);
                //timer.Interval = 1000; // 1 ms
                //timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed); //Elapsed
                //timer.Enabled = true;
                //timer.Start();
                Device.StartTimer(TimeSpan.FromMilliseconds(10), () => {
                    if ((timerrValue >= 150000)||startTimer.Text == "Start Timer")
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

                //timer.Stop();

                //timer = null;
            } 
        }
        private void Timer_Elapsed() //sender  System.Timers.ElapsedEventArgs
        {
            ms += 10;
            timerrValue += 10;
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
            timeSlider.Value = 0;
            timerValue.Text = min + ":" + sec + "." + (ms/10);// String.Format("{0}:{00}.{00}", min, sec, ms / 10);
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
                double mss =0;
                if (double.Parse(split2[1])<10){
                    mss = double.Parse(split2[1])*100;
                }
                else if (double.Parse(split2[1]) > 10)
                {
                    mss = double.Parse(split2[1]) * 10;
                }
                //double mss = double.Parse(split2[1]); ///100
                double sliderTimerValue = mins*60000 + secs*1000 + mss;
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
                    seconds = (int)(Math.Floor(value - (60000 * minutes))/1000);
                    sec = (int)(Math.Floor(value - (60000 * minutes))/1000);
                    milliseconds = (int)(((value - ((minutes * 60000) + (seconds*1000))))/10);
                    ms = (int)((value - ((minutes * 60000) + (seconds * 1000))));  //((100 * (value - ((minutes * 60) + seconds))))
                    timerrValue = (int)(value); //*1000
                    timerValue.Text = minutes + ":" + seconds + "." + milliseconds;  //":" + seconds + "." + milliseconds;, minutes, seconds, milliseconds;
                }
                if (value <= 60000)
                {
                    seconds = (int)(Math.Floor(value/1000));
                    sec = (int)(Math.Floor(value/1000));
                    milliseconds = (int)(((value - (seconds*1000))) / 10); //((100 * (value - seconds))/10)
                    ms = (int)((value - (seconds * 1000))); //((100 * (value - seconds)))
                    min = 0;
                    timerrValue = (int)(value); //*1000
                    timerValue.Text = "0:" + seconds + "." + milliseconds;//(String.Format("0:{00}.{00}", seconds, milliseconds));
                }
                if (value <= 1000)
                {
                    milliseconds = (int)((value)/10);
                    min = 0;
                    sec = 0;
                    ms = (int)((value)/10); //((100 * (value)))
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
            else
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
                double timerrValue = mins + secs + mss;
            }


        }
    }
}
