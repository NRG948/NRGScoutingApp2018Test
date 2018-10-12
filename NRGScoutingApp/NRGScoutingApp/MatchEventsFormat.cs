using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace NRGScoutingApp
{

    // FORMAT OF EVENTS: (cubePicked:1000|cubeDroppedLocation0:5000||cubePicked:5500|cubeDropped:back||climbStart:139000||)
    public class MatchEventsFormat
    {
        public MatchEventsFormat(){}
        public static Array ParseMatchEvents(string events)
        {
            events = events.Remove(0, 1);
            events = events.Remove(events.Length - 1, 1);
            String[] sep = new String[] { "||" };
            String[] mainSplit = events.Split(sep, StringSplitOptions.None);
            String[,] eventsSplit = new String[mainSplit.Length,2];
            for (int i = 0; i < mainSplit.Length; i++)
            {
                if(!mainSplit[i].Contains("|")){
                    eventsSplit[i, 0] =  mainSplit[i];
                    Console.WriteLine(eventsSplit[i, 0]); //DEBUG
                }
                else{
                    String[] temp = mainSplit[i].Split('|');
                    eventsSplit[i,0] = temp[0];
                    eventsSplit[i,1] = temp[1];
                    Console.WriteLine(eventsSplit[i, 0]); //DEBUG
                    Console.WriteLine(eventsSplit[i, 1]); //DEBUG

                }
            }
            Dictionary<string, int> eventDict = new Dictionary<string, int>();
            for (int i = 0; i < eventsSplit.GetLength(0); i++){
                if(eventsSplit[i, 1] == ""){
                    String[] temp = eventsSplit[i,0].Split(':');
                    Console.WriteLine(temp[0]); //DEBUG
                    Console.WriteLine(temp[1]); //DEBUG
                    eventDict.Add(temp[0],Convert.ToInt32(temp[1]));
                }
                else{
                    String[] temp = eventsSplit[i,0].Split(':');
                    eventDict.Add(temp[0],Convert.ToInt32(temp[1]));
                    Console.WriteLine(temp[0]); //DEBUG
                    Console.WriteLine(temp[1]); //DEBUG
                    temp = eventsSplit[i,1].Split(':');
                    Console.WriteLine(temp[0]); //DEBUG
                    Console.WriteLine(temp[1]); //DEBUG
                    eventDict.Add(temp[0],Convert.ToInt32(temp[1]));

                }
            }
            return null; //return parsed dict: eventsSplit
        }

    }
}

// abstract[0][1][0] = cubePicked;
// abstract[0][1][1] = 5000;
// abstract[0][2][0] = cubeDroppedLocation;
// abstract[0][2][1] = 5100;

// hello x = Events["cubedropped"+1]

