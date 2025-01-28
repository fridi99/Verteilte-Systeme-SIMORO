using System;

namespace Simulation;

public class CommercialBuilding : PowerDrain
// the CommercialBuilding Class only drains during working hours
{
    private float _size = 1;

    public CommercialBuilding(float size = 1)
    {
        _size = size;
    }
    
    public override float get_power(DateTime time, float weatherfactor = 100)
    {
        if (time.Hour >= 9 && time.Hour <= 17)
        {
            return -_size;
        }
        else
        {
            return 0;
        }
    }
}