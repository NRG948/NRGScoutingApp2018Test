using System;
using System.Collections.Generic;
using Data = System.Collections.Generic.KeyValuePair<string, string>;

using System.Collections;
using System.Runtime.InteropServices;

namespace NRGScoutingApp
{
    public class Ranker
    {
        public Ranker()
        {
        }

        static Dictionary <string, string> switchAvgValues = new Dictionary<string, string>();
        static Dictionary<string, string> scaleAvgValues = new Dictionary<string, string>();

        public static double switchAvg(List<Data> input)
        {
            int inputLen = input.Count;
            int numSwtiches = 0;
            int totalTime = 0;
            for (int i = 0; i < inputLen; i++)
            {
                if (input[i].Key.Contains("Switch"))
                {
                    numSwtiches++;
                    totalTime += MatchEventsFormat.TimeToInt(input[i].Value)-MatchEventsFormat.TimeToInt(input[i-1].Value);
                    Console.WriteLine(MatchEventsFormat.TimeToInt(input[i].Value) - MatchEventsFormat.TimeToInt(input[i - 1].Value) + "onetimeswitch");
                    Console.WriteLine(totalTime + "swotchavg");
                }
            }
            Console.WriteLine("switch return" + (double)totalTime / numSwtiches);
            return (double)totalTime / numSwtiches;
        }
        public static double scaleAvg(List<Data> input)
        {
            int inputLen = input.Count;
            int numScales = 0;
            int totalTime = 0;
            for (int i = 0; i < inputLen; i++) //-1
            {
                if (input[i].Key.Contains("Scale"))
                {
                    numScales++;
                    totalTime += MatchEventsFormat.TimeToInt(input[i].Value) - MatchEventsFormat.TimeToInt(input[i - 1].Value);
                    Console.WriteLine("old times ubtract " + MatchEventsFormat.TimeToInt(input[i - 1].Value));
                    Console.WriteLine(MatchEventsFormat.TimeToInt(input[i].Value) - MatchEventsFormat.TimeToInt(input[i - 1].Value) + "onetimescale");
                    Console.WriteLine(totalTime + "scaleavg");
                }
                else{
                    Console.WriteLine("noscalefound");
                }
            }
            Console.WriteLine("scale return" + (double)totalTime / numScales);
            return (double)totalTime / numScales;
        }
        public static Dictionary<string, string> mainRanker(String matchData)
        {
            Dictionary<string, string> switchData = new Dictionary<string, string>();
            Dictionary<string, string> scaleData = new Dictionary<string, string>();
            Dictionary<string, string> overAllAvg = new Dictionary<string, string>();

            if (!matchData.Contains("|") || !matchData.Contains("(") || !matchData.Contains(")") || !matchData.Contains("*"))
            {
                return switchAvgValues;
            }
            else
            {
                String[,] datas = MatchFormat.mainStringToSplit(matchData);
                if (datas != null)
                {
                    for (int i = 0; i < datas.GetLength(0); i++)
                    {
                        if (!String.IsNullOrWhiteSpace(datas[i, 0])) {
                            ArrayList parameters = ParametersFormat.ParseMatchParam(datas[i, 0]);
                            String teamName = parameters[1].ToString();
                            if (!switchData.ContainsKey(teamName))
                            {
                                switchData[teamName] = switchAvg(MatchEventsFormat.ParseUserEvents(datas[i, 1])) + ",";
                                Console.WriteLine("Switch value: " + switchData[teamName]);
                            }
                            else
                            {
                                switchData[teamName] += switchAvg(MatchEventsFormat.ParseUserEvents(datas[i, 1])) + ",";
                                Console.WriteLine("Switch value: " + switchData[teamName]);

                            }
                            if (!scaleData.ContainsKey(teamName))
                            {
                                scaleData[teamName] = scaleAvg(MatchEventsFormat.ParseUserEvents(datas[i, 1])) + ",";
                                Console.WriteLine("Scale value: " + scaleData[teamName]);
                            }
                            else
                            {
                                scaleData[teamName] += scaleAvg(MatchEventsFormat.ParseUserEvents(datas[i, 1])) + ",";
                                Console.WriteLine("Scale value: " + scaleData[teamName]);
                            }
                        }
                    }
                    Console.WriteLine("Before avg Teams");
                    avgTeams(switchData, scaleData);
                    Console.WriteLine("After avg Teams");
                    return switchAvgValues;
                }
                else{
                    Console.WriteLine("return empy switch avg");
                    return switchAvgValues;
                }
            }
        }

        public static void avgTeams(Dictionary<string, string> switchData, Dictionary<string, string> scaleData){

            foreach (String team in TeamsNames.teams){
                if(switchData.ContainsKey(team)){
                    String avgSwitch = switchData[team];
                    Console.WriteLine(avgSwitch);
                    String[] allSwitches = avgSwitch.Split(',');
                    int len = allSwitches.GetLength(0);
                    double totalAdd = 0;
                    for (int i = 0; i < allSwitches.GetLength(0); i++){
                        if (String.IsNullOrWhiteSpace(allSwitches[i])) {
                            len--;
                        }
                        else
                        {
                            try
                            {
                                totalAdd += Convert.ToInt32(allSwitches[i]);
                            }
                            catch (System.FormatException){
                                Console.WriteLine("catchswitch");
                            }

                        }
                    }
                    totalAdd /= len;
                    switchAvgValues[team] = ((int)totalAdd).ToString();
                    Console.WriteLine(team + totalAdd);
                }
                if(scaleData.ContainsKey(team))
                {
                    String avgScale = scaleData[team].ToString();
                    Console.WriteLine(avgScale);
                    String[] allScales = avgScale.Split(',');
                    double totalAdd = 0;
                    int len = allScales.GetLength(0);
                    for (int i = 0; i < allScales.GetLength(0); i++)
                    {
                        if (String.IsNullOrWhiteSpace(allScales[i]))
                        {
                            len--;
                        }
                        else
                        {
                            try{
                                totalAdd += Convert.ToInt32(allScales[i]);

                            }
                            catch(System.FormatException){
                                Console.WriteLine("catchscale");
                            }
                           
                        }
                    }
                    totalAdd /= len;
                    scaleAvgValues[team] = ((int)totalAdd).ToString();
                    Console.WriteLine(team + totalAdd);
                }
            }
        }
    }
}
