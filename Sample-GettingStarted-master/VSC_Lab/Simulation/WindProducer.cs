using System;

namespace Simulation;

public class WindProducer : PowerSource
{
    private float _size = 1;

    public WindProducer(float size = 1)
    {
        _size = size;
    }

    public override float get_power(DateTime time, float weatherfactor = 100)
    {
        return _size * weatherfactor/100;
    }
}