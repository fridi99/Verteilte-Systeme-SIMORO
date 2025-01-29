using System;
using System.Collections.Generic;

namespace Simulation;

public class GeneralConsumer : PowerDrain
// this class can have any on/off times, given by the list argument of the constructor
// first DateTime Object gives on time, second gives off time
{
    private float _size = 1;
    private List<DateTime> _operating_times;

    public GeneralConsumer(List<DateTime> list, float size = 1)
    {
        _size = size;
        _operating_times = list;
    }

    private bool comparator(DateTime a, DateTime b)
    {
        if(a.Hour > b.Hour) return true;
        if(a.Hour < b.Hour) return false;
        if(a.Minute > b.Minute) return true;
        else return false;
    }
    
    public override float get_power(DateTime time, float weatherfactor = 100)
    {
        DateTime t1 = _operating_times[0];
        DateTime t2 = _operating_times[1];
        if (comparator(t2, t1)) 
        {
            if (comparator(time, t1) && !comparator(time,t2))
            {
                return -_size;
            }

            return 0;
        }
        else
        {
            if ((comparator(time, t1)) || !comparator(time, t2))
            {
                return -_size;
            }

            return 0;
        }
        
    }
}