using System;
using System.Diagnostics.Tracing;
namespace NRGScoutingApp
{

    // FORMAT OF EVENTS: (cubePicked:1000|cubeDroppedLocation0:5000||cubePicked:5500|cubeDropped:back||climbStart:139000||)
    public class MatchEventsFormat
    {
        public MatchEventsFormat()
        {
        }
        public static Array ParseMatchEvents(string events)
        {
            events.Remove(0, 1);
            events.Remove(events.Length - 1, 1);
            char sep = '|' ; //fix sep with tutorial online "||","|"
            Array[][][] x = null;
            string[] eventsSplit = events.Split(sep);
            for (int i = 0; i < eventsSplit.Length; i++)
            {
                Console.WriteLine(eventsSplit[0]);
                Console.WriteLine(eventsSplit[1]);

            }
            return null; //return parsed dict

        }

    }
}

// abstract[0][1][0] = cubePicked;
// abstract[0][1][1] = 5000;
// abstract[0][2][0] = cubeDroppedLocation;
// abstract[0][2][1] = 5100;

// hello x = Events["cubedropped"+1]

