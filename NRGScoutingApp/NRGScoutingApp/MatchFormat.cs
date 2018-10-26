using System;
using Data = System.Collections.Generic.KeyValuePair<string, string>;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace NRGScoutingApp
{
    public class MatchFormat
    {
        public MatchFormat()
        {
        }
        public static String[,] mainStringToSplit(String matchData){
            if (!matchData.Contains("|") || !matchData.Contains("||"))
            {
                return null;
            }
            else{
                String[] matches = matchData.Split('&');
                String[,] splitData = new String[matches.Length, 2];
                for (int i = 0; i < matches.Length; i++){
                    if(String.IsNullOrWhiteSpace(matches[i])){}
                    else{
                        String[] temp = matches[i].Split('*');
                        splitData[i, 0] = temp[0]; // Match Parameters
                        splitData[i, 1] = temp[1]; // Match Events
                        Console.WriteLine(splitData[i, 0]);
                        Console.WriteLine(splitData[i, 1]);
                    }
                }
                return splitData;
            }
        }

        public class MatchData
        {
            public string teamName { get; set; }
            public string matchNum { get; set; }
            public string position { get; set; }
        }

        public static List<MatchData> matchesToSimpleData(String[,] inputString){
            List <MatchData>  data = new List<MatchData>();
            for (int i = 0; i < inputString.GetLength(0); i++)
            {
                if (String.IsNullOrWhiteSpace(inputString[i, 0]) || String.IsNullOrWhiteSpace(inputString[i, 1])) { }

                else
                {
                    ArrayList parameters = ParametersFormat.ParseMatchParam(inputString[i, 0]);
                    Console.WriteLine(inputString[i,0]);
                    data.Add(new MatchData { teamName = parameters[0].ToString(), 
                        matchNum = "Match " + parameters[1].ToString(), 
                        position = parameters[2].ToString() });
                    Console.WriteLine(parameters[0].ToString() + "Match " + parameters[1].ToString() + parameters[2].ToString());
                }
            }
                      
            return data;
        }
    }
}
