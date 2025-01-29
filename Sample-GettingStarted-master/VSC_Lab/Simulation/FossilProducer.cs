using System;

namespace Simulation;

public class FossilProducer : PowerSource
{
    private float _size = 1;
    public FossilProducer(float size = 1)
    {
        _size = size;
    }
    public override float get_power(DateTime time, float weatherfactor = 100)
    {
        return _size;
    }
}