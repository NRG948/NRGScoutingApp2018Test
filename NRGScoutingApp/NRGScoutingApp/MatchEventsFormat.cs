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
                    if (string.IsNullOrWhiteSpace(eventsSplit[i, 1]))
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

        public static String returnUserEventsAsString(List<Data> newMatchData)
        {
            String newEvents;
            newEvents = "(";
            int pickNum = 0, dropNum = 0;
            for (int i = 0; i < newMatchData.Count; i++)
            {
                if (newMatchData[i].Key.Contains("Picked"))
                {
                    newEvents += "cubePicked" + pickNum + ":" + TimeToInt(newMatchData[i].Value);
                    Console.WriteLine ("cubePicked" + pickNum + ":" + TimeToInt(newMatchData[i].Value));
                    Console.WriteLine(pickNum);
                    pickNum++;
                }
                else if (newMatchData[i].Key.Contains("Scale"))
                {
                    newEvents += "cubeDroppedScale" + dropNum + ":" + TimeToInt(newMatchData[i].Value) + "|";
                    dropNum++;
                }
                else if (newMatchData[i].Key.Contains("Ally Switch"))
                {
                    newEvents += "cubeDroppedAllySwitch" + dropNum + ":" + TimeToInt(newMatchData[i].Value) + "|";
                    dropNum++;
                }
                else if (newMatchData[i].Key.Contains("None"))
                {
                    newEvents += "cubeDroppedNone" + dropNum  + ":" + TimeToInt(newMatchData[i].Value) + "|";
                    dropNum++;
                }
                else if (newMatchData[i].Key.Contains("Opposite Switch"))
                {
                    newEvents += "cubeDroppedOppSwitch" + dropNum + ":" + TimeToInt(newMatchData[i].Value) + "|";
                    dropNum++;
                }
                else if (newMatchData[i].Key.Contains("Exchange"))
                {
                    newEvents += "cubeDroppedExchange" + dropNum + ":" + TimeToInt(newMatchData[i].Value) + "|";
                    dropNum++;
                }
                else if (newMatchData[i].Key.Contains("Climb"))
                {
                    newEvents += "climbStart:" + TimeToInt(newMatchData[i].Value) + "|";

                }
                newEvents += "|";
            }
            return newEvents;
        }

        public static List<Data> returnSortedUserEvents(List<Data> newMatchData){
            //try{
                String[,] matchDataInString = new String[newMatchData.Count, 2];
                for (int i = 0; i < newMatchData.Count; i++)
                {
                    Console.WriteLine(i);
                    matchDataInString[i, 0] = newMatchData[i].Key;
                    matchDataInString[i, 1] = TimeToInt(newMatchData[i].Value).ToString();
                }

            for (int i = 1; i <= matchDataInString.GetLength(0); i++)
                {
                for (int j = 1; j <= matchDataInString.GetLength(0) - i; j++)
                    {
                        // Use ">" for ascending and "<" for descending 
                        if (Convert.ToInt32(matchDataInString[i - 1, 1]) > Convert.ToInt32(matchDataInString[j + i - 1, 1]))
                        {
                            Console.WriteLine("I" + i + "J" + j);
                            String c1 = matchDataInString[i - 1, 1];
                            String c0 = matchDataInString[i - 1, 0];
                            String d1 = matchDataInString[j + i - 1, 1];
                            String d0 = matchDataInString[j + i - 1, 0];
                            matchDataInString[i - 1, 1] = d1;
                            matchDataInString[i - 1, 0] = d0;
                            matchDataInString[j + i - 1, 1] = c1;
                            matchDataInString[j + i - 1, 0] = c0;
                        }
                    }
                }
                List<Data> returnList = new List<Data>();
                for (int i = 0; i < matchDataInString.Length; i++)
                {
                    returnList.Add(new Data(matchDataInString[i, 0], matchDataInString[i, 1]));
                }
                return returnList;
            //}
            //catch(IndexOutOfRangeException){
            //    return null;
            //}

        }

        public static int TimeToInt(string time){
            string TimerValue = time;
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
            Console.WriteLine(mins * NewMatchStart.minMs + secs * NewMatchStart.secMs + mss);
            return (int)(mins * NewMatchStart.minMs + secs * NewMatchStart.secMs + mss);
        }

        public static String ToTimeFormat(int time)
        {
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
            else
            {
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

