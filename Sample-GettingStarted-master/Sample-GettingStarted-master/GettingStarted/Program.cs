using System.Collections.Generic;

namespace GettingStarted
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<SmartMeter> testList = [new SmartMeter()];

            var manager = new SimulationManager(args, testList, 900);
        }
    }
}
