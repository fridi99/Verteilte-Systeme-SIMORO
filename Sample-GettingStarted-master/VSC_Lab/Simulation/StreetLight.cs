using System;

namespace Simulation;

public class StreetLight : PowerDrain
// this class will only drain during the day time
{
    private float _size = 1;

    public StreetLight(float size = 1)
    {
        _size = size;
    }

    public override float get_power(DateTime time, float weatherfactor = 100)
    {
        if (time.Hour <= 6 || time.Hour >= 20)
        {
            return -_size;
        }
        else
        {
            return 0;
        }
    }
}
