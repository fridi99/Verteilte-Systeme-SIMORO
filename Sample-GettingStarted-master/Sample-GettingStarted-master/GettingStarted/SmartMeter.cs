using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace GettingStarted
{
    /// TODO: Connect with Simulation class
    public class SmartMeter
    {
        public Guid Id { get; } = Guid.NewGuid();
        public double CurrentValue { get; set; } = 0.0; // TODO: Get starting value from Simulation class
        public SmartMeterStatus Status { get; set; } = SmartMeterStatus.Ok; // TODO: Do anything sensible with this
    }
}