using System;
namespace DefaultNamespace;

public interface IPowerSource
{
    public abstract float get_power(DateTime time, float weatherfactor = 100);
}