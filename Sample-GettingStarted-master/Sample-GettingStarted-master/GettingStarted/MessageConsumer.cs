using System;
using System.Text.Json;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GettingStarted
{
    public class MeteringPoint (Guid meteringPointNumber, double meterReading, SmartMeterStatus status)
    {
        public Guid MeteringPointNumber { get; } = meteringPointNumber;
        public double MeterReading { get; } = meterReading;
        public SmartMeterStatus Status { get; } =  status;
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
    }

    public class MessageConsumer :
        IConsumer<MeteringPoint>
    {
        readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MeteringPoint> context)
        {
            _logger.LogInformation("Received Text: {Text}", context.Message);

            return Task.CompletedTask;
        }
    }
}