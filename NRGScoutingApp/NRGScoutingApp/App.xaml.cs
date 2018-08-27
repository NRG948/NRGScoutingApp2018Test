using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;


namespace NRGScoutingApp
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";


        public App()
        {
            InitializeComponent();
            Application.Current.MainPage = new NavigationPage(new NavTab());
            MainPage = new NavigationPage(new NavTab());

  
            //        if (Device.RuntimePlatform == Device.iOS)
            //{
            //    MainPage = new NavigationPage(new NavTab());
            //    //new NavigationPage(
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new NavTab());
            //}
            //switch(App.Current.Properties["appState"]) {
            //    case 0:
            //        break;
            //    case 1:
            //        new NavigationPage(new MatchEntryEditTab());
            //        break;
            //}


            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

           
          
        }
      public class NavTab : TabbedPage
        {
            public NavTab()
            {
                Children.Add(new Matches());
                Children.Add(new Rankings());

            }
        }

        //public class MatchEntryEditTab : TabbedPage
        //{
        //    public MatchEntryEditTab()
        //    {
        //        Children.Add(new WelcomePage());
        //        Children.Add(new Rankings());

        //    }
        //}

    }
}
