namespace DefaultNamespace;

public abstract class PowerDrain
{

    private int id;
    //private simhost = SimulationHost.getinstance();
    private bool isoperating = true;
    public abstract float get_power(DateTime time, float weatherfactor = 100);
    
}