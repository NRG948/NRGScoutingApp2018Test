using System;
using System.Collections.Generic;
using System.Collections;
using Xamarin.Forms;

namespace NRGScoutingApp
{
    public partial class MatchEntryEditTab : TabbedPage
    {
        public MatchEntryEditTab()
        {
            //TabbedPage.SetBinding(MatchEntryEditTab.TitleProperty = teamAssign.teamName);
            checkParse();
            Children.Add(new NewMatchStart());
            Children.Add(new MatchEvents());
            if(App.Current.Properties.ContainsKey(App.Current.Properties["teamStart"].ToString() + "converted")){
                Children.Add(new MatchParameters(vals));
            }
           else{
                Children.Add(new MatchParameters()); //vals
            }            
            BindingContext = this;
            App.Current.Properties["newAppear"] = 1;
            App.Current.SavePropertiesAsync();
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }
        ArrayList vals = new ArrayList();
        string titleName = (App.Current.Properties["teamStart"].ToString());
        public string teamName { get { return titleName; } }
        private void checkParse(){
            String teamParse = App.Current.Properties["teamStart"].ToString() + "converted";
            if (!App.Current.Properties.ContainsKey(App.Current.Properties["teamStart"].ToString() + "converted")){}
            else{
                ParametersFormat s = new ParametersFormat();
                vals = s.ParseMatchParam(App.Current.Properties[App.Current.Properties["teamStart"].ToString() + "converted"].ToString()); //"<" + App.Current.Properties["teamStart"].ToString()+">"
                DisplayAlert("Hello", "exists", "ok");
            }
        }
    }
}
