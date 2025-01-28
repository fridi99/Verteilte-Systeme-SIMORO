using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Simulation
{
    /// TODO: Connect with Simulation class
    public class SmartMeter
    {
        private PowerDrain _drain;
        private PowerSource _source;
        private bool _issource;
        public SmartMeter(PowerDrain drain)
        {
            _drain = drain;
            _issource = false;
        }
        public SmartMeter(PowerSource source)
        {
            _source = source;
            _issource = true;
        }

        public double get_power(DateTime time)
        {
            if(_issource)
            {
                CurrentValue = _source.get_power(time); 
                return CurrentValue;                
            }
            else
            {
                CurrentValue = _drain.get_power(time);
                return CurrentValue;
            }
        }

        public void update(DateTime time)
        {
            get_power(time);
        }
        public Guid Id { get; } = Guid.NewGuid();
        public double CurrentValue { get; set; } = 0.0; // TODO: Get starting value from Simulation class
        public SmartMeterStatus Status { get; set; } = SmartMeterStatus.Ok; // TODO: Do anything sensible with this
    }
}