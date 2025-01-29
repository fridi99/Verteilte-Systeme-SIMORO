using System;
using System.Collections.Generic;

namespace Simulation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SolarProducer solar1 = new SolarProducer(30);
            WindProducer wind1 = new WindProducer(20);
            StreetLight streetLight1 = new StreetLight(15);
            CommercialBuilding comBuilding1 = new CommercialBuilding(10);
            GeneralConsumer genConsumer1 =
                new GeneralConsumer([new DateTime(1, 1, 1, 12, 0, 0), new DateTime(1, 1, 1, 20, 0, 0)], 10);
            List<SmartMeter> testList = [new SmartMeter(solar1), 
                                         new SmartMeter(wind1), 
                                         new SmartMeter(streetLight1), 
                                         new SmartMeter(comBuilding1), 
                                         new SmartMeter(genConsumer1)];
            
            var manager = new SimulationManager(args, testList, 180);
        }
    }
}
