namespace Simulation;

public abstract class PowerSource : IPowerSource
/// <summary>
/// This class has is an abstract parent class for all power sources created
/// </summary>
{
	private int id;
	//private simhost = SimulationHost.getinstance();
	private bool isoperating = true;
	public abstract float get_power(System.DateTime time, float weatherfactor = 100);
}