using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Simulation;

public class SolarProducer : PowerSource
{
    private int _size;
    private Dictionary<string, float> _simdata =  new Dictionary<string, float>();
    public SolarProducer(int size = 1)
    {
        _size = size;
        using (var reader = new StreamReader(@"Solar_Plant_data.csv"))
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

    public override float get_power(System.DateTime time, float weatherfactor = 100)
    {
        string curtime = roundtime(time);
        return _simdata[curtime]/10_546 * weatherfactor/100 * _size;
    }
}