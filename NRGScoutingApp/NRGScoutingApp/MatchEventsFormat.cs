using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Data = System.Collections.Generic.KeyValuePair<string, string>;

namespace NRGScoutingApp
{

    /* FORMAT OF EVENTS: (cubePicked:1000|cubeDroppedLocation0:5000||cubePicked:5500|cubeDropped:back||climbStart:139000||)
     * MUST RETURN Dictionary<string,int> or Dictionary<string,string>  
     *      PLEASE KEEP THIS IN MIND WHILE PASSING VALUES THROUGH THIS
    */
    public class MatchEventsFormat
    {
        public MatchEventsFormat(){}
        //THE ORIGINAL MATCH EVENTS STRING AND IF THE REQUESTED DATA IS FOR USER VIEWING OR BACKEND PROCESSING
        public static Dictionary<string, int> ParseMatchEvents(string events) 
        {
            if (!events.Contains("|") || !events.Contains("||"))
            {
                Console.WriteLine("empty input");
                return null;
            }
            else
            {
                events = events.Remove(0, 1);
                events = events.Remove(events.Length - 1, 1);
                String[] sep = new String[] { "||" };
                String[] mainSplit = events.Split(sep, StringSplitOptions.None);
                String[,] eventsSplit = new String[mainSplit.Length, 2];
                for (int i = 0; i < mainSplit.Length; i++)
                {
                    if (!mainSplit[i].Contains("|"))
                    {
                        eventsSplit[i, 0] = mainSplit[i];
                        Console.WriteLine(eventsSplit[i, 0]); //DEBUG
                    }
                    else
                    {
                        String[] temp = mainSplit[i].Split('|');
                        eventsSplit[i, 0] = temp[0];
                        eventsSplit[i, 1] = temp[1];
                        Console.WriteLine(eventsSplit[i, 0]); //DEBUG
                        Console.WriteLine(eventsSplit[i, 1]); //DEBUG

                    }
                }

                Dictionary<string, int> eventDict = new Dictionary<string, int>();
                for (int i = 0; i < eventsSplit.GetLength(0); i++)
                {
                    if (eventsSplit[i, 1] == "")
                    {
                        String[] temp = eventsSplit[i, 0].Split(':');
                        eventDict.Add(temp[0], Convert.ToInt32(temp[1]));
                        Console.WriteLine(temp[0]); //DEBUG
                        Console.WriteLine(temp[1]); //DEBUG
                    }
                    else
                    {
                        String[] temp = eventsSplit[i, 0].Split(':');
                        eventDict.Add(temp[0], Convert.ToInt32(temp[1]));
                        Console.WriteLine(temp[0]); //DEBUG
                        Console.WriteLine(temp[1]); //DEBUG
                        temp = eventsSplit[i, 1].Split(':');
                        eventDict.Add(temp[0], Convert.ToInt32(temp[1]));
                        Console.WriteLine(temp[0]); //DEBUG
                        Console.WriteLine(temp[1]); //DEBUG
                    }
                }
                return eventDict;
            }
        }
        public static List<Data> ParseUserEvents(string events)

        {
            if (!events.Contains("|") || !events.Contains("||"))
            {
                Console.WriteLine("empty input");
                return null;
            }
            else
            {
                List<Data> MatchEvents = new List<Data>();
                events = events.Remove(0, 1);
                events = events.Remove(events.Length - 1, 1);
                String[] sep = new String[] { "||" };
                String[] mainSplit = events.Split(sep, StringSplitOptions.None);
                String[,] eventsSplit = new String[mainSplit.Length, 2];
                for (int i = 0; i < mainSplit.Length; i++)
                {
                    if (!mainSplit[i].Contains("|"))
                    {
                        eventsSplit[i, 0] = mainSplit[i];
                        Console.WriteLine(eventsSplit[i, 0]); //DEBUG
                    }
                    else
                    {
                        String[] temp = mainSplit[i].Split('|');
                        eventsSplit[i, 0] = temp[0];
                        eventsSplit[i, 1] = temp[1];
                        Console.WriteLine(eventsSplit[i, 0]); //DEBUG
                        Console.WriteLine(eventsSplit[i, 1]); //DEBUG

                    }
                }

               for (int i = 0; i < eventsSplit.GetLength(0); i++)
                {
                    if (string.IsNullOrWhiteSpace(eventsSplit[i, 1]))
                    {
                        String[] temp = eventsSplit[i, 0].Split(':');
                        {
                            if (temp[0].Contains("Picked"))
                            {
                                temp[0] = "Cube Picked";
                            }
                            else if (temp[0].Contains("Scale"))
                            {
                                temp[0] = "Dropped Scale";
                            }
                            else if (temp[0].Contains("AllySwitch"))
                            {
                                temp[0] = "Dropped Ally Switch";
                            }
                            else if (temp[0].Contains("None"))
                            {
                                temp[0] = "Dropped None";
                            }
                            else if (temp[0].Contains("OppSwitch"))
                            {
                                temp[0] = "Dropped Opposite Switch";
                            }
                            else if (temp[0].Contains("Exchange"))
                            {
                                temp[0] = "Dropped Exchange";
                            }
                            else if (temp[0].Contains("climb"))
                            {
                                temp[0] = "Climb Start";
                            }
                        }
                        //userDict.Add(temp[0], temp[1]);  //TEMP
                        Console.WriteLine(temp[0]); //DEBUG
                        Console.WriteLine(temp[1]); //DEBUG
                        MatchEvents.Add(new Data(temp[0], ToTimeFormat(Convert.ToInt32(temp[1]))));
                    }
                    else
                    {
                        String[] temp = eventsSplit[i, 0].Split(':');
                        {
                            if (temp[0].Contains("Picked"))
                            {
                                temp[0] = "Cube Picked";
                            }
                            else if (temp[0].Contains("Scale"))
                            {
                                temp[0] = "Dropped Scale";
                            }
                            else if (temp[0].Contains("AllySwitch"))
                            {
                                temp[0] = "Dropped Ally Switch";
                            }
                            else if (temp[0].Contains("None"))
                            {
                                temp[0] = "Dropped None";
                            }
                            else if (temp[0].Contains("OppSwitch"))
                            {
                                temp[0] = "Dropped Opposite Switch";
                            }
                            else if (temp[0].Contains("Exchange"))
                            {
                                temp[0] = "Dropped Exchange";
                            }
                            else if (temp[0].Contains("climb"))
                            {
                                temp[0] = "Climb Start";
                            }
                        }
                        //userDict.Add(temp[0], temp[1]); //TEMP
                        Console.WriteLine(temp[0]); //DEBUG
                        Console.WriteLine(temp[1]); //DEBUG
                        MatchEvents.Add(new Data(temp[0], ToTimeFormat(Convert.ToInt32(temp[1]))));
                        temp = eventsSplit[i, 1].Split(':');
                        {
                            if (temp[0].Contains("Picked"))
                            {
                                temp[0] = "Cube Picked";
                            }
                            else if (temp[0].Contains("Scale"))
                            {
                                temp[0] = "Dropped Scale";
                            }
                            else if (temp[0].Contains("AllySwitch"))
                            {
                                temp[0] = "Dropped Ally Switch";
                            }
                            else if (temp[0].Contains("None"))
                            {
                                temp[0] = "Dropped None";
                            }
                            else if (temp[0].Contains("OppSwitch"))
                            {
                                temp[0] = "Dropped Opposite Switch";
                            }
                            else if (temp[0].Contains("Exchange"))
                            {
                                temp[0] = "Dropped Exchange";
                            }
                            else if (temp[0].Contains("climb"))
                            {
                                temp[0] = "Climb Start";
                            }
                        }
                        //userDict.Add(temp[0], temp[1]); //TEMP
                        Console.WriteLine(temp[0]); //DEBUG
                        Console.WriteLine(temp[1]); //DEBUG
                        MatchEvents.Add(new Data(temp[0], ToTimeFormat(Convert.ToInt32(temp[1]))));
                    }
                }
                //MatchEvents.Sort();
                return MatchEvents;               
            }
        }
        public static String ToTimeFormat(int time){
            int minutes, seconds, milliseconds;
            if (time >= NewMatchStart.minMs)
            {
                minutes = (int)(Math.Floor(time / NewMatchStart.minMs));
                seconds = (int)(Math.Floor(time - (NewMatchStart.minMs * minutes)) / NewMatchStart.secMs);
                milliseconds = (int)(((time - ((minutes * NewMatchStart.minMs) + (seconds * NewMatchStart.secMs)))) / 10);
                return minutes + ":" + seconds + '.' + milliseconds;
            }
            else if (time <= NewMatchStart.minMs)
            {
                seconds = (int)(Math.Floor(time / NewMatchStart.secMs));
                milliseconds = (int)(((time - (seconds * NewMatchStart.secMs))) / 10);
                return 0 + ":" + seconds + '.' + milliseconds;
            }
            else if (time <= NewMatchStart.secMs)
            {
                milliseconds = (int)((time) / 10);
                return 0 + ":00" + '.' + milliseconds;
            }
            else{
                return null;
            }
        }

    }
}

// abstract[0][1][0] = cubePicked;
// abstract[0][1][1] = 5000;
// abstract[0][2][0] = cubeDroppedLocation;
// abstract[0][2][1] = 5100;

// hello x = Events["cubedropped"+1]

