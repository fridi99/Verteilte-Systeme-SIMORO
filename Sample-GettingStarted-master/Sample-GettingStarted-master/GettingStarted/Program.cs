using System;
using System.Collections.Generic;
using DefaultNamespace;

namespace GettingStarted
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SolarProducer solar1 = new SolarProducer(5);
            List<SmartMeter> testList = [new SmartMeter(solar1)];
            Console.WriteLine(testList[0].get_power(DateTime.Now));
            var manager = new SimulationManager(args, testList, 900);
            
        }
    }
}
