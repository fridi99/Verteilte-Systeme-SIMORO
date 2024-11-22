using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;


namespace GettingStarted;

/// <summary>
/// Manages Smartmeter classes
/// </summary>
public class SimulationManager
{
    private static List<SmartMeter> _smartMeters = [];
    private static int _simTimeAcceleration = 1;
        
    public SimulationManager(string[] args, List<SmartMeter> smartMeters,  int simTimeAcceleration)
    {
        CreateHostBuilder(args).Build().Run();
        _smartMeters = smartMeters;
        _simTimeAcceleration = simTimeAcceleration;
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<MessageConsumer>();

                    x.UsingRabbitMq((context,cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                });

                services.AddHostedService<Publisher>();
            });

    private class Publisher(IBus bus) : BackgroundService
    {
        private readonly IBus _bus = bus;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var smartMeter in _smartMeters)
                {
                    // TODO: Implement Simulation Time update
                    // TODO: Update CurrentValue with Simulation class
                
                    await _bus.Publish(new Message(smartMeter.Id, smartMeter.CurrentValue, smartMeter.Status).Text, stoppingToken);
                }
                
                await Task.Delay(900000 / ((_simTimeAcceleration != 0) ? _simTimeAcceleration : 1), stoppingToken);
            }
        }  
    }
}