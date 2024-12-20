
using DefaultNamespace;


SolarProducer Solar1 = new SolarProducer(20); 
SolarProducer Solar2 = new SolarProducer(40);
WindProducer Wind1 = new WindProducer(5);
FossilProducer Fossil1 = new FossilProducer(200);
StreetLight SL1 = new StreetLight(120);
CommercialBuilding CB1 = new CommercialBuilding(25);
DateTime t1 = new DateTime(1, 1, 1, 5, 30, 0);
DateTime t2 = new DateTime(1, 1, 1, 17, 30, 0);
GeneralConsumer GC1 = new GeneralConsumer([t2, t1], 30);

Console.WriteLine(Solar1.get_power(DateTime.Now));
Console.WriteLine(Solar2.get_power(DateTime.Now));
Console.WriteLine(Wind1.get_power(DateTime.Now));
Console.WriteLine(Fossil1.get_power(DateTime.Now));
Console.WriteLine(SL1.get_power(DateTime.Now));
Console.WriteLine(CB1.get_power(DateTime.Now));
Console.WriteLine(GC1.get_power(DateTime.Now));