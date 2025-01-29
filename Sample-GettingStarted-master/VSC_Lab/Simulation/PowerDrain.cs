using System;

namespace Simulation;

public abstract class PowerDrain : IPowerSource
{

    private int id;
    //private simhost = SimulationHost.getinstance();
    private bool isoperating = true;
    public abstract float get_power(DateTime time, float weatherfactor = 100);
    
}