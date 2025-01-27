namespace DefaultNamespace;

public class SimulationHost
{
	private static SimulationHost _instance;
    private int _simulation_speed = 100;
	private int time { get; set; } //seconds
	private SimulationHost(){ }
	public static SimulationHost getinstance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new SimulationHost();
			}
			return _instance;
		}
	}
	public void update_time(){
		time += 60 * (_simulation_speed/100);
	}
}