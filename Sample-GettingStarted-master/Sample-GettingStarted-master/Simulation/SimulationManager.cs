using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;


namespace Simulation;

/// <summary>
/// Manages Smartmeter classes
/// </summary>
public class SimulationManager
{
    private static List<SmartMeter> _smartMeters = [];
    private static int _simTimeAcceleration = 1;
    private static int _samprate = 5; 
        
    public SimulationManager(string[] args, List<SmartMeter> smartMeters,  int simTimeAcceleration)
    {
        _smartMeters = smartMeters;
        _simTimeAcceleration = simTimeAcceleration;
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMassTransit(x =>
                {
                    x.UsingRabbitMq((context,cfg) =>
                    {
                        cfg.Host("localhost", h =>
                        {
                            h.ConfigureBatchPublish(bp =>
                            {
                                bp.Enabled = false;
                            });
                        });
                    });
                });

                services.AddSingleton<IHostedService, Publisher>();
            });

    private class Publisher(IBus bus) : BackgroundService
    {
        private readonly IBus _bus = bus;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:SmartMeter"));
            
            var i = 1;
            DateTime simtime = new DateTime(2025,1,1, 9, 0, 0);
            while (!stoppingToken.IsCancellationRequested)
            {
                System.Console.Out.WriteLine(i++);
                foreach (var smartMeter in _smartMeters)
                {
                    // TODO: Implement Simulation Time update
                    // TODO: Update CurrentValue with Simulation class
                    smartMeter.update(simtime);
                    var data = new MeteringPoint(smartMeter.Id.ToString(), smartMeter.CurrentValue, smartMeter.Status, simtime);
                    Console.WriteLine($"[{simtime}] {smartMeter.CurrentValue} : {smartMeter.Status}");
                    await endpoint.Send(data, stoppingToken);
                }
                simtime = simtime.AddSeconds(_simTimeAcceleration*_samprate);                
                await Task.Delay(_samprate*1000, stoppingToken);
            }
        }  
    }
}