using System;

using Xamarin.Forms;

namespace NRGScoutingApp
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";


        public App()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS){
                MainPage = new NavigationPage(new NavTab());
                //new NavigationPage(
            }
            else{
                MainPage = new NavigationPage(new NavTab());
            }

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

           
          
        }
      public class NavTab : TabbedPage
        {
            public NavTab()
            {
                Children.Add(new WelcomePage());
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
