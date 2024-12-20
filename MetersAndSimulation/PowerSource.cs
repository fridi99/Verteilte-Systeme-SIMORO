namespace DefaultNamespace;

public abstract class PowerSource : IPowerSource
// the parent class that power sources inherit from
{
	private int id;
	//private simhost = SimulationHost.getinstance();
	private bool isoperating = true;
	public abstract float get_power(System.DateTime time, float weatherfactor = 100);
	
	
}