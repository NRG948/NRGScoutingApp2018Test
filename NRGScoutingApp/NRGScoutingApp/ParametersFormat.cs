using System.Collections;
namespace NRGScoutingApp
{
    public class ParametersFormat
    {
        public ParametersFormat()
        {
            
        }

        public string ConvertMatchParam(string teamName, string matchNum, string pickerS, bool crossedB, bool switchB, bool scaleB, bool fswitchB, bool fscaleB, bool deathB, bool soloB, bool assistedB, bool neededB, bool platformB,
            bool noclimbB, string fouls, bool recyellowB, bool recredB, string comments) {
            string param = "<teamName>" + teamName + "<matchNum>" + matchNum + "<side>" + pickerS + "<crossedBase>" + crossedB + "<switch>" + switchB + "<scale>" + scaleB
            + "<farSwitch>" + fswitchB + "<farScale>" + fscaleB + "<death>" + deathB + "<solo>" + soloB + "<assisted>" + assistedB + "<needed>" + neededB + "<platform>" + platformB
                + "<noClimb>" + noclimbB + "<fouls>" + fouls + "<yellowC>" + recyellowB + "<redC>" + recredB + "<comments>" + comments;
            return param;
        }

        public ArrayList ParseMatchParam(string param) {
            char[] sep = { '<', '>' };
            ArrayList list = new ArrayList();
            string[] par = param.Split(sep);
            for (int i = 0; i < par.Length; i++) {
                if(i % 2 != 0) {
                    list.Add(par[i]);
                }
            }
            return list;
        }
    }
}
