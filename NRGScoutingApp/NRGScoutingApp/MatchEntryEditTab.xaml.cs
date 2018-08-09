using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NRGScoutingApp
{
    public partial class MatchEntryEditTab : TabbedPage
    {
        public MatchEntryEditTab()
        {
            Children.Add(new NewMatchStart());
            Children.Add(new MatchParameters());
            Children.Add(new MatchEvents());
            InitializeComponent();
        }
    }
}
