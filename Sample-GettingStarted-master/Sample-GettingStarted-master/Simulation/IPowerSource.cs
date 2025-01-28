using System;

namespace Simulation;

public interface IPowerSource
// Interface for all the Power Classes. Only enforces the single interface function
{
    public abstract float get_power(DateTime time, float weatherfactor = 100);
}