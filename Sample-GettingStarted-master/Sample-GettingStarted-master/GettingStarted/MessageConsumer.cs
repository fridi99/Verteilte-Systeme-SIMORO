using System;
using System.Text.Json;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GettingStarted
{
    public class Message (Guid meteringPointNumber, double meterReading, SmartMeterStatus status)
    {
        private class MeteringPoint (Guid _meteringPointNumber, double _meterReading, SmartMeterStatus _status)
        {
            public Guid meteringPointNumber { get; } = _meteringPointNumber;
            public double meterReading { get; } = _meterReading;
            public SmartMeterStatus status { get; } =  _status;
            public DateTimeOffset timestamp { get; } = DateTimeOffset.Now;
        }
        
        public string Text { get; } = JsonSerializer.Serialize(new MeteringPoint(meteringPointNumber, meterReading, status));
    }

    public class MessageConsumer :
        IConsumer<Message>
    {
        readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation("Received Text: {Text}", context.Message.Text);

            return Task.CompletedTask;
        }
    }
}