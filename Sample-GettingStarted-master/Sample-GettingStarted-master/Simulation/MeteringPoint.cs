using System;
using System.Text.Json;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Simulation
{
    public class MeteringPoint (string meteringPointNumber, double meterReading, SmartMeterStatus status, DateTime timestamp)
    {
        public string MeteringPointNumber { get; } = meteringPointNumber;
        public double MeterReading { get; } = meterReading;
        public SmartMeterStatus Status { get; } =  status;
        public DateTime Timestamp { get; } = timestamp;
    }
}