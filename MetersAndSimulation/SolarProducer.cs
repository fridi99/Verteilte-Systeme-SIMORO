namespace DefaultNamespace;

using System.Globalization;

public class SolarProducer : Power_source
{
    private int _size;
    private Dictionary<string, float> _simdata =  new Dictionary<string, float>();
    public SolarProducer(int size = 1)
    {
        _size = size;
        using (var reader = new StreamReader(@"MetersAndSimulation/Solar_Plant_data.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(";").ToList();
                _simdata.Add(values[1], float.Parse(values[4], CultureInfo.InvariantCulture.NumberFormat));
            }
        }
    }

    private string roundtime(DateTime time)
    {
        string result = time.ToString("HH");
        int fac = time.Minute / 15;
        result += ":" + (fac * 15).ToString("00");
        return result;
    }

    public float get_power(System.DateTime time)
    {
        string curtime = roundtime(time);
        return _simdata[curtime];
    }
}